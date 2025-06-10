
namespace ECommerce.Core.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string SuccessMessage { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }
        public ErrorType? ErrorType { get; } // Optional for typed errors

        protected Result(bool isSuccess, string successMessage, string error, ErrorType? errorType = null)
        {
            if (isSuccess && !string.IsNullOrEmpty(error))
                throw new InvalidOperationException("Successful result cannot have an error");

            if (!isSuccess && string.IsNullOrEmpty(error))
                throw new InvalidOperationException("Failed result must have an error");

            IsSuccess = isSuccess;
            Error = error;
            ErrorType = errorType;
            SuccessMessage = successMessage;
        }

        public static Result Success(string successMessage) => new(true, successMessage, string.Empty);
        public static Result<T> Success<T>(T value, string successMessage = "")
            => new(value, true, successMessage, string.Empty);
        public static Result Failure(string error) => new(false, string.Empty, error);
        public static Result Failure(string error, ErrorType errorType) => new(false, string.Empty,
            error, errorType);
        public static Result<T> Failure<T>(string error) => new(default, false, string.Empty, error);
        public static Result<T> Failure<T>(string error, ErrorType errorType) =>
            new(default, false, string.Empty, error, errorType);
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("Cannot access value of a failed result");

        protected internal Result(T value, bool isSuccess, string successMessage, string error, ErrorType? errorType = null)
            : base(isSuccess, successMessage, error, errorType)
        {
            _value = value;
        }

    }
}
