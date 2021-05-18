using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        private Random _rand;

        public RandomGenerator()
        {
            _rand = new Random();
        }

        /// <summary>
        /// Generates a string of numbers for the length given.
        /// Max length allowed is 10. It defaults to 10 if invalid length given.
        /// It does not allow first number to be zero.
        /// </summary>
        /// <param name="stringLength">The length of string desired.</param>
        /// <returns></returns>
        public string GetNumberString(int stringLength)
        {
            var sb = new StringBuilder();

            // Ensure string length is > 1 and <= 10.
            stringLength = stringLength > 0 && stringLength <= 10 ? stringLength : 10;

            for(int i = 0; i < stringLength; i++)
            {
                // Get the next number that is between 0 and 9
                var n = _rand.Next(10);

                // Ensure first number is not zero
                while(n == 0 && i == 0)
                {
                    n = _rand.Next(10);
                }
                sb.Append(n);
            }

            return sb.ToString();
        }
    }
}
