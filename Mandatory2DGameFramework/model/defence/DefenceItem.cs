using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    /// <summary>
    /// Represents an object in the world that provides 
    /// defensive capabilities by reducing incoming hit points.
    /// </summary>
    public class DefenceItem : WorldObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the amount of hit points to reduce 
        /// from the creature when damage is applied.
        /// </summary>
        public int ReduceHitPoint { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DefenceItem class with 
        /// default values. This version assumes the player is not
        /// wearing any defensive items.
        /// </summary>
        public DefenceItem()
        {
            Name = string.Empty;
            ReduceHitPoint = 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A string representation of the DefenceItem, including
        /// its name and the amount of hit points it reduces.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(ReduceHitPoint)}={ReduceHitPoint.ToString()}}}";
        }
        #endregion
    }
}
