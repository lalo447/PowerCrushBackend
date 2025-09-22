namespace NewSystem.App.Shared
{
    public interface IFilter<T>
    {
        /// <summary>Applies the specified queryable.</summary>
        /// <param name="queryable">The queryable.</param>
        IQueryable<T> Apply(IQueryable<T> queryable);
    }
}
