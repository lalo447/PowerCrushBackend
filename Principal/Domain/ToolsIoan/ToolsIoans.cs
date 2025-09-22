using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NewSystem.Domain.ToolsIoan
{
    /// <summary>Tool loan entity</summary>
    public class ToolsIoans
    {
        public int ToolIoanId { get; private set; }
        public string? ClientName { get; private set; }
        public int NumberIoan { get; private set; }
        public int TotalItems { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; private set; } = null;
        public string? ToolsLoanDetails { get; private set; }
        public string? ToolsIoanStatus { get; private set; }
        public string? Comments { get; private set; } = null;

        /// <summary>Creates the specified client name.</summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="totalItems">The total items.</param>
        /// <param name="toolsLoanDetails">The tools loan details.</param>
        /// <param name="toolsIoanStatus">The tools ioan status.</param>
        /// <param name="numberIoan">The number ioan.</param>
        /// <returns>Returns the object that was created from the rest of the tools.</returns>
        public static ToolsIoans? Create(string clientName, int totalItems,
            string toolsLoanDetails, string toolsIoanStatus, int numberIoan)
        {
           DateTime? returnDate = null;

           if (toolsIoanStatus.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
            {
                returnDate = DateTime.UtcNow;
            }
            return new ToolsIoans
            {
                ClientName = clientName,
                TotalItems = totalItems,
                NumberIoan = numberIoan,
                ToolsLoanDetails = toolsLoanDetails,
                ToolsIoanStatus = toolsIoanStatus,
                ReturnDate = returnDate
            };
        }

        /// <summary>Updates the specified client name.</summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="totalItems">The total items.</param>
        /// <param name="toolsLoanDetails">The tools loan details.</param>
        /// <param name="toolsIoanStatus">The tools ioan status.</param>
        /// <param name="isActive"></param>
        /// <param name="comments"></param>
        public void Update(string clientName, int totalItems, string toolsLoanDetails, string toolsIoanStatus, bool isActive, string comments)
        {
            ClientName = clientName;
            TotalItems = totalItems;
            ToolsLoanDetails = toolsLoanDetails;
            ToolsIoanStatus = toolsIoanStatus;
            IsActive = isActive;
            Comments = comments;
            if (toolsIoanStatus.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
            {
                ReturnDate = DateTime.UtcNow;
            }
        }
    }
}
