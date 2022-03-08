using MegaFortnite.Common.Enums;

namespace MegaFortnite.Common.Result
{
    public class Result : IResult
    {
        public ErrorCode Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        protected Result() { }

        public static Result NotFound(string message = default)
        {
            return new Result
            {
                Message = message,
                Code = ErrorCode.NotFound,
            };
        }

        public static Result Internal(string message = default)
        {
            return new Result
            {
                Message = message,
                Code = ErrorCode.Internal,
            };
        }

        public static Result Bad(string message = default)
        {
            return new Result
            {
                Message = message,
                Code = ErrorCode.BadRequest,
            };
        }

        public static Result Failed(IResult result)
        {
            return new Result
            {
                Message = result.Message,
                Code = result.Code,
                Success = result.Success,
            };
        }

        public static Result Ok()
        {
            return new Result
            {
                Success = true,
            };
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public static Result<T> NotFound(string message = default)
        {
            return new Result<T>
            {
                Message = message,
                Code = ErrorCode.NotFound,
            };
        }

        public static Result<T> Internal(string message = default)
        {
            return new Result<T>
            {
                Message = message,
                Code = ErrorCode.Internal,
            };
        }

        public static Result<T> Bad(string message = default)
        {
            return new Result<T>
            {
                Message = message,
                Code = ErrorCode.BadRequest,
            };
        }

        public static Result<T> Failed(IResult result)
        {
            return new Result<T>
            {
                Message = result.Message,
                Code = result.Code,
                Success = result.Success,
            };
        }

        public static Result<T> Ok(T data)
        {
            return new Result<T>
            {
                Success = true,
                Data = data,
            };
        }
    }
}
