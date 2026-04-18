using Mandatory2DGameFramework.gameInterface.strategy;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.combat
{
    /// <summary>
    /// An implementation of the IStrategy interface that 
    /// calculates combat in an aggressive manner, increasing
    /// the total damage by 25% and decreasing defence by 25%.
    /// </summary>
    public class AggressiveStrategy : IStrategy
    {
        /// <summary>
        /// Calculates the total damage by summing the Hit 
        /// values of all AttackItems and applying an aggressive 
        /// multiplier of 1.25.
        /// </summary>
        /// <param name="items">The collection of AttackItems to 
        /// calculate damage from.</param>
        /// <returns>The total damage after applying the aggressive 
        /// multiplier.</returns>
        public int CalculateDamage(IEnumerable<AttackItem> items)
        {
            return (int)(items.Sum(i => i.Hit) * 1.25);
        }

        /// <summary>
        /// Calculates the total defence by summing the ReduceHitPoint 
        /// values of all DefenceItems and applying a defensive 
        /// multiplier of 0.75.
        /// </summary>
        /// <param name="items">The collection of DefenceItems to 
        /// calculate defence from.</param>
        /// <returns>The total defence after applying the defensive 
        /// multiplier.</returns>
        public int CalculateDefence(IEnumerable<DefenceItem> items)
        {
            return (int)(items.Sum(i => i.ReduceHitPoint) * 0.75);
        }
    }

    /// <summary>
    /// An implementation of the IStrategy interface that calculates 
    /// combat in a balanced manner. No modifications are made to the 
    /// total damage or defence.
    /// </summary>
    public class BalancedStrategy : IStrategy
    {
        /// <summary>
        /// Calculates the total damage by summing the Hit values of
        /// all AttackItems without any modifications.
        /// </summary>
        /// <param name="items">The collection of AttackItems to  
        /// calculates damage from.</param>
        /// <returns>The total damage without any modifications.
        /// </returns>
        public int CalculateDamage(IEnumerable<AttackItem> items)
        {
            return items.Sum(i => i.Hit);
        }

        /// <summary>
        /// Calculates the total defence by summing the ReduceHitPoint
        /// values of all DefenceItems without any modifications.
        /// </summary>
        /// <param name="items">The collection of DefenceItems to 
        /// calculate defence from.</param>
        /// <returns>The total defence without any modifications.
        /// </returns>
        public int CalculateDefence(IEnumerable<DefenceItem> items)
        {
            return items.Sum(i => i.ReduceHitPoint);
        }
    }

    /// <summary>
    /// An implementation of the IStrategy interface that calculates  
    /// combat in a defensive manner, decreasing the total damage by 25% 
    /// and increasing defence by 25%.
    /// </summary>
    public class DefensiveStrategy : IStrategy
    {
        /// <summary>
        /// Calculates the total damage by summing the Hit values of all 
        /// AttackItems and applying a defensive multiplier of 0.75.
        /// </summary>
        /// <param name="items">The collection of AttackItems to calculate 
        /// damage from.</param>
        /// <returns>The total damage after applying the defensive
        /// multiplier.</returns>    
        public int CalculateDamage(IEnumerable<AttackItem> items)
        {
            return (int)(items.Sum(i => i.Hit) * 0.75);
        }

        /// <summary>
        /// Calculates the total defence by summing the ReduceHitPoint 
        /// values of all DefenceItems and applying a defensive multiplier 
        /// of 1.25.
        /// </summary>
        /// <param name="items">The collection of DefenceItems to calculate 
        /// defence from.</param>
        /// <returns>The total defence after applying the defensive 
        /// multiplier.</returns>
        public int CalculateDefence(IEnumerable<DefenceItem> items)
        {
            return (int)(items.Sum(i => i.ReduceHitPoint) * 1.25);
        }
    }
}
