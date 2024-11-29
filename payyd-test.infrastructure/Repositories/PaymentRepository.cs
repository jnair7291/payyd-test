using Microsoft.Extensions.Logging;
using payyd_test.application.Interfaces;
using Square.Authentication;
using Square;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using payyd_test.domain.Common;
using Square.Models;
using Microsoft.EntityFrameworkCore;
using Square.Exceptions;

namespace payyd_test.infrastructure.Repositories
{
    public class PaymentRepository : ITransactionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly PayydDbContext _dBContext;
        private readonly string LocationId;
        private static ISquareClient squareClient;
        private readonly ILogger<CustomerRepository> _logger;
        public PaymentRepository(IConfiguration configuration, PayydDbContext dBContext, ILogger<CustomerRepository> logger)
        {
            _configuration = configuration;
            _dBContext = dBContext;
            _logger = logger;
            var LocationId = $"{_configuration.GetSection("Square:LocationId").Value}";
            var accessToken = $"{_configuration.GetSection("Square:AccessToken").Value}";
            var environment = $"{_configuration.GetSection("Square:Environment").Value}";
            squareClient = new SquareClient.Builder().Environment(environment == "Production" ? Square.Environment.Production : Square.Environment.Sandbox)
                   .BearerAuthCredentials(new BearerAuthModel.Builder(accessToken).Build())
               .Build();
        }

        public async Task<ResultOrError<string, ErrorResponse>> MakePaymentAsync(string customerId)
        {
            _logger.Log(LogLevel.Information, "Starting MakePaymentAsync for customerId: {CustomerId}", customerId);

            ErrorResponse error = new ErrorResponse(Errors.SomethingWentWrong, null);
            if (string.IsNullOrWhiteSpace(customerId))
            {
                _logger.Log(LogLevel.Warning, "Invalid input: CustomerId is null or empty.");
                error = new ErrorResponse(Errors.InvalidInput, "CustomerId not found");
                return new ResultOrError<string, ErrorResponse>(error);
            }

            try
            {
                // Create a list for line items and define the amount.
                _logger.Log(LogLevel.Information, "Creating line items for order.");
                var lineItems = new List<OrderLineItem>();
                var amountMoney = new Money.Builder()
                    .Amount(100L) // Amount in the smallest currency unit (e.g., cents for USD)
                    .Currency("USD")
                    .Build();

                // Create a single line item for the order.
                var orderLineItem = new OrderLineItem.Builder(quantity: "1")
                    .Name("Example Item")
                    .BasePriceMoney(amountMoney)
                    .Build();
                lineItems.Add(orderLineItem);

                // Build the order with the line item and customer details.
                _logger.Log(LogLevel.Information, "Building the order for customerId: {CustomerId}", customerId);
                var order = new Square.Models.Order.Builder(locationId: LocationId)
                    .CustomerId(customerId)
                    .LineItems(lineItems)
                    .Build();

                // Create the order in Square.
                _logger.Log(LogLevel.Information, "Sending request to Square API to create order.");
                var createOrderRequest = new CreateOrderRequest.Builder()
                    .Order(order)
                    .Build();
                var orderResult = await squareClient.OrdersApi.CreateOrderAsync(createOrderRequest);

                if (orderResult?.Order == null || string.IsNullOrWhiteSpace(orderResult.Order.Id))
                {
                    _logger.Log(LogLevel.Error, "Order creation failed: Order ID not found.");
                    error = new ErrorResponse(Errors.DatabaseSaveError, "Order Id not found");
                    return new ResultOrError<string, ErrorResponse>(error);
                }

                var orderId = orderResult.Order.Id;
                _logger.Log(LogLevel.Information, "Order created successfully with OrderId: {OrderId}", orderId);

                // Fetch the card details associated with the customer from the database.
                _logger.Log(LogLevel.Information, "Fetching card details for customerId: {CustomerId}", customerId);
                var cardDetails = await _dBContext.WalletModel
                    .Where(x => x.CustomerId == customerId)
                    .FirstOrDefaultAsync();

                if (cardDetails == null || string.IsNullOrWhiteSpace(cardDetails.CardId))
                {
                    _logger.Log(LogLevel.Error, "Card details not found for customerId: {CustomerId}", customerId);
                    error = new ErrorResponse(Errors.DatabaseSaveError, "Card Id not found");
                    return new ResultOrError<string, ErrorResponse>(error);
                }

                // Create a payment request using the retrieved card details and order information.
                _logger.Log(LogLevel.Information, "Creating payment request for OrderId: {OrderId}", orderId);
                var paymentRequest = new CreatePaymentRequest.Builder(
                        sourceId: cardDetails.CardId,
                        idempotencyKey: Guid.NewGuid().ToString())
                    .CustomerId(customerId)
                    .AmountMoney(amountMoney)
                    .OrderId(orderId)
                    .Note($"Payment for Order: {orderId}")
                    .Build();

                // Execute the payment request.
                _logger.Log(LogLevel.Information, "Sending payment request to Square API.");
                var paymentResponse = await squareClient.PaymentsApi.CreatePaymentAsync(paymentRequest);

                if (paymentResponse?.Payment == null || string.IsNullOrWhiteSpace(paymentResponse.Payment.Id))
                {
                    _logger.Log(LogLevel.Error, "Payment failed for OrderId: {OrderId}", orderId);
                    error = new ErrorResponse(Errors.PaymentGatewayError, "Payment returned with exception");
                    return new ResultOrError<string, ErrorResponse>(error);
                }

                // Return success with the payment ID.
                string success = paymentResponse.Payment.Id;
                _logger.Log(LogLevel.Information, "Payment successful with PaymentId: {PaymentId}", success);
                return new ResultOrError<string, ErrorResponse>(success);
            }
            catch (ApiException apiEx)
            {
                // Handle Square API exceptions.
                _logger.Log(LogLevel.Error, "Square API exception occurred: {Exception}", apiEx.ToString());
                var squareError = new ErrorResponse(Errors.PaymentGatewayError, apiEx.ToString());
                return new ResultOrError<string, ErrorResponse>(squareError);
            }
            catch (Exception ex)
            {
                // Handle general exceptions.
                _logger.Log(LogLevel.Critical, "Unexpected exception occurred: {Exception}", ex.Message);
                error = new ErrorResponse(Errors.SomethingWentWrong, ex.Message);
                return new ResultOrError<string, ErrorResponse>(error);
            }
        }


    }
}
