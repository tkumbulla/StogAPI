using System.ComponentModel.DataAnnotations;

namespace Stog.Application.Models
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual Guid Id { get; set; }
        /// <summary>
        /// Gets the entity identifier
        /// </summary>
        /// <returns></returns>
        public virtual object GetId()
        {
            return $"({Id})";
        }
    }
}
