
namespace Stog.Application.Extensions
{
    /// <summary>
    /// Extension class used to hold extension methods for LINQ
    /// </summary>
    public static class LINQExtensions
    {
        /// <summary>
        /// If conditionals inside queries
        /// </summary>
        /// <typeparam name="T">The general type</typeparam>
        /// <param name="query">The query</param>
        /// <param name="should">Condition</param>
        /// <param name="transforms"></param>
        /// <returns></returns>
        public static IQueryable<T> If<T>(
            this IQueryable<T> query,
            bool should,
            params Func<IQueryable<T>, IQueryable<T>>[] transforms)
        {
            return should
                ? transforms.Aggregate(query,
                    (current, transform) => transform.Invoke(current))
                : query;
        }
        /// <summary>
        /// If conditionals inside queries that return an IEnumerable collection
        /// </summary>
        /// <typeparam name="T">The general type entity</typeparam>
        /// <param name="query">Query</param>
        /// <param name="should">Condition</param>
        /// <param name="transforms"></param>
        /// <returns></returns>
        public static IEnumerable<T> If<T>(
            this IEnumerable<T> query,
            bool should,
            params Func<IEnumerable<T>, IEnumerable<T>>[] transforms)
        {
            return should
                ? transforms.Aggregate(query,
                    (current, transform) => transform.Invoke(current))
                : query;
        }
    }
}
