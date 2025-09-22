namespace NewSystem.App.Shared
{
    public static class QueryableFilterExtensions
    {
        /// <summary>Applies the filters.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The q.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> data, IEnumerable<IFilter<T>> filters)
        {
            foreach (var f in filters) data = f.Apply(data);
            return data;
        }
    }
}
