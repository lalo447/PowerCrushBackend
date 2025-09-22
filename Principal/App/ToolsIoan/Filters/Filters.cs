using Microsoft.EntityFrameworkCore;
using NewSystem.App.Shared;
using NewSystem.Domain.ToolsIoan;

namespace NewSystem.App.ToolsIoan.Filters
{
    public class ToolsIoanStatusFilter(string status) : IFilter<ToolsIoans>
    {
        private readonly string _status = status;

        /// <summary>Applies the specified q.</summary>
        /// <param name="data">The q.</param>
        /// <returns>Return data filter of q</returns>
        public IQueryable<ToolsIoans> Apply(IQueryable<ToolsIoans> data) =>
            data.Where(t => t.ToolsIoanStatus == _status);
    }

    public class ClientNameContainsFilter(string name) : IFilter<ToolsIoans>
    {
        private readonly string _name = name;

        /// <summary>Applies the specified q.</summary>
        /// <param name="data">The q.</param>
        /// <returns>Return data filter of q</returns>
        public IQueryable<ToolsIoans> Apply(IQueryable<ToolsIoans> data) =>
            data.Where(t => EF.Functions.Like(t.ClientName, $"%{_name}%"));
    }

    public class IsActiveFilter(bool isActive) : IFilter<ToolsIoans>
    {
        private readonly bool _isActive = isActive;

        /// <summary>Applies the specified q.</summary>
        /// <param name="data">The q.</param>
        /// <returns>Return data filter of q</returns>
        public IQueryable<ToolsIoans> Apply(IQueryable<ToolsIoans> data) =>
            data.Where(t => t.IsActive == _isActive);
    }

    public class NumberIoanEqualsFilter(int number) : IFilter<ToolsIoans>
    {
        private readonly int _number = number;

        /// <summary>Applies the specified q.</summary>
        /// <param name="data">The q.</param>
        /// <returns>Return data filter of q</returns>
        public IQueryable<ToolsIoans> Apply(IQueryable<ToolsIoans> data) =>
            data.Where(t => t.NumberIoan == _number);
    }
}
