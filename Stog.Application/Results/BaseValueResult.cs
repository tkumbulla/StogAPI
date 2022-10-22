namespace Stog.Application.Results
{
    /// <summary>
    /// Base value result
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public abstract class BaseValueResult<TValue> : BaseResult
    {

        /// <summary>
        /// The value of the base result
        /// </summary>
        public TValue? Value { get; set; }
    }
}
