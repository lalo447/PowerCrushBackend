using NewSystem.App;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewSystem.Service
{
    public class CreateNumberIoan : ICreateNumberIoan
    {
        /// <summary>Generates the number ioan.</summary>
        /// <returns>Return the numberIoan</returns>
        public Task<int> GenerateNumberIoan()
        {
            var random = new Random();
            var sb = new StringBuilder(8);

            sb.Append(random.Next(1, 10));

            for (int i = 1; i < 8; i++)
            {
                sb.Append(random.Next(0, 10));
            }

            int number = int.Parse(sb.ToString());

            return Task.FromResult(number);
            
    }
}
}
