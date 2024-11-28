using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.domain.Common
{
    public class ResultOrError<TResult, TError>
    {
       
            public ResultOrError(TResult result)
            {
                this.Result = result;
                this.IsError = false;
            }

            public ResultOrError(TError error)
            {
                this.Error = error;
                this.IsError = true;
            }
            public TResult Result { get; }

            public TError Error { get; }

            public bool IsError { get; }

            public void Match(Action<TResult> onSuccess, Action<TError> onError)
            {
                if (this.IsError)
                {
                    onError(this.Error);
                }
                else
                {
                    onSuccess(this.Result);
                }
            }

            public T Match<T>(Func<TResult, T> resultFunc, Func<TError, T> errorFunc) => this.IsError ?
                                                                                errorFunc(this.Error) : resultFunc(this.Result);

    }
}
