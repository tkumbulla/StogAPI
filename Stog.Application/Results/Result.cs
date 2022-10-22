using Stog.Application.Results.Messages;

namespace Stog.Application.Results
{
    /// <summary>
    /// Defines the structure of the Result class.
    /// </summary>
    public class Result : BaseResult
    {
        #region "Static constructors"
        /// <summary>
        /// Returns an OK(200) result
        /// </summary>
        /// <returns></returns>
        public static Result Ok()
        {
            return new Result();
        }
        /// <summary>
        /// Returns an OK(200) result with a value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<TValue> Ok<TValue>(TValue value)
        {
            return new Result<TValue>(value);
        }
        /// <summary>
        /// Returns an OK(200) result with a message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result Ok(string message)
        {
            var result = new Result();
            result.Messages.Add(new Message(message) { Text = message, Level = MessageLevel.Warn });
            return result;
        }
        /// <summary>
        /// Returns result Fail to indicate something went wrong with an error message.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <returns></returns>
        public static Result Fail(Message message)
        {
            return new Result()
                .WithError(message);
        }
        /// <summary>
        /// Returns result Fail to indicate something went wrong with a string as an error message.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <returns></returns>
        public static Result Fail(string message)
        {
            return new Result()
                .WithError(message);
        }
        /// <summary>
        /// Returns result Fail with value and error message
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<TValue> Fail<TValue>(Message message)
        {
            return new Result<TValue>()
                .WithError(message);
        }
        /// <summary>
        /// Returns result Fail with value and string as error message
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<TValue> Fail<TValue>(string message)
        {
            return new Result<TValue>()
                .WithError(message);
        }
        #endregion
    }
    /// <summary>
    /// Result that contains a value of TValue type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Result<TValue> : BaseValueResult<TValue>
    {
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Result()
        {
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value"></param>
        public Result(TValue value)
        {
            Value = value;
        }
    }
}
