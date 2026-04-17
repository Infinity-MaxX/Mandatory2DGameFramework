using Mandatory2DGameFramework.model.attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.gameInterface.strategy
{
    /// <summary>
    /// Interface for calculating the damage of an attack 
    /// based on a collection of attack items. Implementations 
    /// of this interface can define different strategies for 
    /// how the damage is calculated.
    /// </summary>
    public interface IHitStrategy
    {
        /// <summary>
        /// Calculates the total damage of an attack based on 
        /// the provided collection of attack items.
        /// </summary>
        /// <param name="items">The collection of attack items 
        /// to calculate damage from.</param>
        /// <returns>The total damage calculated from the attack 
        /// items.</returns>
        int CalculateDamage(IEnumerable<AttackItem> items);
    }
}
