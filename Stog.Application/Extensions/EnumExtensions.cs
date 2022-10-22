using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Stog.Application.Extensions
{
    /// <summary>
    /// Extension methods for enumerations
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Method needed to get the value of the display name attribute for a enumeration value
        /// </summary>
        /// <param name="enumValue">The enum value with a display name attribute</param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName() ?? "";
        }
    }
}
