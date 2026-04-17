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
    // attack weapons (attackItems). Hint: May be implemented in the
    // Composite class

    /// <summary>
    /// Represents an item that can be used to perform attacks within 
    /// the game world. Provides properties for the item's name, attack
    /// strength, range, and weight.
    /// </summary>
    /// <remarks>
    /// AttackItem serves as a base class for specialized attack items,
    /// including those that implement design patterns such as Decorator
    /// (for modifying attack or defense properties) and Composite (for
    /// grouping multiple weapons). Subclasses may extend functionality 
    /// to support features like weight management or composite behaviors. 
    /// Instances of AttackItem are typically used by creatures or entities 
    /// capable of carrying and utilizing attack items.
    /// </remarks>
    public class AttackItem : WorldObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the hit strength of the attack item, 
        /// which determines the amount of damage it can inflict 
        /// on a target when used in an attack.
        /// </summary>
        public virtual int Hit { get; set; }

        /// <summary>
        /// Gets or sets the range of the attack item, which 
        /// indicates how far the attack can reach or affect 
        /// targets.
        /// </summary>
        public virtual int Range { get; set; }

        /// <summary>
        /// Gets or sets the weight of the attack item.
        /// Used to determine whether a creature can carry it.
        /// </summary>
        public virtual int Weight { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the AttackItem class with 
        /// default values. This version of the constructor assumes 
        /// that the player has no attack items equipped.
        /// </summary>
        public AttackItem()
        {
            Name = string.Empty;
            Hit = 0;
            Range = 0;
            Weight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the AttackItem class with a
        /// name and default values for hit strength and range. This
        /// allows for junk/gag weapons that have no attack strength 
        /// or range.
        /// </summary>
        /// <param name="name">The name of the weapon.</param>
        public AttackItem(string name)
        {
            Name = name;
            Hit = 0;
            Range = 0;
            Weight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackItem"/> class 
        /// with explicit values for name, hit strength, range, and weight.
        /// </summary>
        /// <param name="name">The name of the attack item.</param>
        /// <param name="hit">The amount of damage this item can inflict 
        /// when used in combat.</param>
        /// <param name="range">The effective distance at which this item 
        /// can be used.</param>
        /// <param name="weight">The weight of the item, used to determine 
        /// whether a creature is able to carry it based on its maximum 
        /// carrying capacity.</param>
        public AttackItem(string name, int hit, int range, int weight)
        {
            Name = name;
            Hit = hit;
            Range = range;
            Weight = weight;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A string representation of the AttackItem, 
        /// including its name, hit strength, range, and weight.
        /// </summary>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(Hit)}={Hit}, " +
                $"{nameof(Range)}={Range}, " +
                $"{nameof(Weight)}={Weight}}}";
        }
        #endregion
    }
}
