using Mandatory2DGameFramework.gameInterface.strategy;
using Mandatory2DGameFramework.model.attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.combat
{
    /// <summary>
    /// An implementation of the IHitStrategy interface that 
    /// calculates damage in an aggressive manner, increasing
    /// the total damage by 50%.
    /// </summary>
    public class AggressiveHitStrategy : IHitStrategy
    {
        /// <summary>
        /// Calculates the total damage by summing the Hit 
        /// values of all AttackItems and applying an aggressive 
        /// multiplier of 1.5.
        /// </summary>
        /// <param name="items">The collection of AttackItems to 
        /// calculate damage from.</param>
        /// <returns>The total damage after applying the aggressive 
        /// multiplier.</returns>
        public int CalculateDamage(IEnumerable<AttackItem> items)
        {
            return (int)(items.Sum(i => i.Hit) * 1.5);
        }
    }
    
    /// <summary>
    /// An implementation of the IHitStrategy interface that 
    /// calculates damage in a balanced manner. No modifications 
    /// are made to the total damage.
    /// </summary>
    public class BalancedHitStrategy : IHitStrategy
    {
        /// <summary>
        /// Calculates the total damage by summing the Hit value
        /// of all AttackItems without any modifications.
        /// </summary>
        /// <param name="items">The collection of AttackItems to 
        /// calculate damage from.</param>
        /// <returns>The total damage without any modifications.
        /// </returns>
        public int CalculateDamage(IEnumerable<AttackItem> items)
        {
            return items.Sum(i => i.Hit);
        }
    }

    /// <summary>
    /// An implementation of the IHitStrategy interface that
    /// calculates damage in a defensive manner, reducing the
    /// total damage by 25%.
    /// </summary>
    public class DefensiveHitStrategy : IHitStrategy
    {
        /// <summary>
        /// Calculates the total damage by summing the Hit values 
        /// of all AttackItems and applying a defensive multiplier 
        /// of 0.75.
        /// </summary>
        /// <param name="items">The collection of AttackItems to 
        /// calculate damage from.</param>
        /// <returns>The total damage after applying the defensive 
        /// multiplier.</returns>
        public int CalculateDamage(IEnumerable<AttackItem> items)
        {
            return (int)(items.Sum(i => i.Hit) * 0.75);
        }
    }
}
