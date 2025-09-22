using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSystem.App
{
    public interface ICreateNumberIoan
    {
        /// <summary>Generates the number ioan.</summary>
        /// <returns>Return the numberIoan</returns>
        Task<int> GenerateNumberIoan();
    }
}
