using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.helper.generator
{
    /// <summary>
    /// A utility class for generating random numbers within 
    /// a specified range. This will be used to generate the
    /// amount of damage a creature can do and the amount of
    /// hit points a creature will lose when hit.
    /// </summary>
    public class NumberGenerator
    {
        #region Instances
        private Random _generator;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the NumberGenerator class,
        /// with default settings for the random generator.
        /// </summary>
        public NumberGenerator()
        {
            _generator = new Random();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a random number in the interval between the values
        /// of "min" and "max"
        /// </summary>
        public int Next(int min, int max)
        {
            int value = min + _generator.Next(max - min + 1);
            return value;
        }
        #endregion
    }
}
