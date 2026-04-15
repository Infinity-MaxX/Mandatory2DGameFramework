using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.attack
{
    // There needs to be a subclass for AttackItem that uses the
    // Decorator Design Pattern: example: boost or weaken defence
    // There needs to be a subclass for AttackItem that uses the
    // Composite Design Pattern; it needs to store weapons

    // Note: The AttackItem must have a property weight. A creature
    // must be configurable to be able to carry a maximum weight of
    // attack weapons (attackItems). Hint: May Implemented in the
    // Composite class

    /// <summary>
    /// Represents an item that can be used to perform attacks within 
    /// the game world. Provides properties for the item's name, attack
    /// strength, and range.
    /// </summary>
    /// <remarks>AttackItem serves as a base class for specialized attack 
    /// items, including those that implement design patterns such as 
    /// Decorator (for modifying attack or defense properties) and Composite 
    /// (for grouping multiple weapons). Subclasses may extend functionality 
    /// to support features like weight management or composite behaviors. 
    /// Instances of AttackItem are typically used by creatures or entities 
    /// capable of carrying and utilizing attack items.</remarks>
    public class AttackItem : WorldObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the attack item, which 
        /// identifies the type of attack or weapon it represents.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the hit strength of the attack item, 
        /// which determines the amount of damage it can inflict 
        /// on a target when used in an attack.
        /// </summary>
        public int Hit { get; set; }
        /// <summary>
        /// Gets or sets the range of the attack item, which 
        /// indicates how far the attack can reach or affect 
        /// targets. This could represent the distance at which 
        /// the attack can be effective, such as melee range or 
        /// ranged attacks.
        /// </summary>
        public int Range { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the AttackItem class with default values.
        /// </summary>
        public AttackItem()
        {
            Name = string.Empty;
            Hit = 0;
            Range = 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A string representation of the AttackItem, 
        /// including its name, hit strength, and range.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(Hit)}={Hit.ToString()}, " +
                $"{nameof(Range)}={Range.ToString()}}}";
        }
        #endregion
    }
}
