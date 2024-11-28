# Payyd-test-project

## Description

The **Payyd-test-project** is a backend system that provides a set of RESTful APIs designed to facilitate simple payment-related transactions between users and a payment gateway. The project includes various controllers that manage different aspects of the payment process, including user management, transaction handling, wallet management, and optional third-party integration.

## Technology Stack

- **.NET Core 6.0 LTS** – A stable, long-term support version of .NET for building and running the application.
- **Repository Design Pattern** – Used to separate concerns and provide an abstraction layer between the business logic and data access layers. This ensures that the application is scalable and maintainable.

## Project Structure

The project is divided into four main controllers:

1. **UserController**  
   Handles requests related to user creation, both on the payment gateway and in the local database. This ensures that user information is properly stored and associated with payment activities.

2. **TransactionController**  
   Manages the transactions between the user and the payment gateway. It processes payment requests, handles responses from the gateway, and ensures the integrity of the transaction.

3. **WalletController**  
   Manages card details on the payment gateway. It facilitates wallet-related actions like saving, updating, or deleting payment methods, but **does not store any card details locally** to comply with finance institution policies.

4. **ThirdPartyController (Optional)**  
   An optional controller for handling requests related to merchants and payment gateway interactions. This is used when third-party integrations are necessary for the system.

## Key Features

- **Simple Transactions**: The project allows seamless transactions between users and the payment gateway, ensuring that payments are processed efficiently.
- **Security Compliance**: The project does not store any card details locally in the database, adhering to finance institution policies on handling sensitive payment information.
- **Modular Architecture**: The controllers are designed to be loosely coupled, making it easy to extend the system with additional features or integrate with other services.
  
## Setup & Installation

1. Clone the repository:
   ```bash
   [git clone https://github.com/yourusername/payyd-test-project.git](https://github.com/jnair7291/payyd-test.git)
