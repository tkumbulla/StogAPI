using Stog.Application.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StogAPI.Extensions
{
    /// <summary>
    /// Extension method needed to manage the state of the model
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Remove all the errors from the model 
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveErrors(this ModelStateDictionary modelState, string key)
        {

            if (modelState.ContainsKey(key))
            {
                modelState[key].Errors.Clear();
                modelState[key].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

                return true;
            }

            return false;
        }
        /// <summary>
        /// Add errors to model from the result
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="result"></param>
        public static void AddModelErrorFromResults(this ModelStateDictionary modelState, Result result)
        {
            foreach (var message in result.Messages)
            {
                modelState.AddModelError(message.Code, message.Text);
            }
        }
    }
}
