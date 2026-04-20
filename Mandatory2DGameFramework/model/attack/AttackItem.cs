using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.attack
{
    /// <summary>
    /// Represents an item that can be used to perform attacks within 
    /// the game world. Provides properties for the item's name, attack
    /// strength, range, and weight. Implements operator overloading to 
    /// allow combining multiple attack items, thereby supporting the 
    /// Composite design pattern.
    /// </summary>
    /// <remarks>
    /// <see cref="AttackItem"/> serves as a base class for specialized 
    /// attack items, including those that implement design patterns such 
    /// as Decorator (for modifying attack or defense properties) and 
    /// Composite (for grouping multiple weapons). Subclasses may extend 
    /// functionality to support features like weight management or 
    /// composite behaviors. Instances of <see cref="AttackItem"/> are 
    /// typically used by creatures or entities capable of carrying and 
    /// utilizing attack items.
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
        /// Initializes a new instance of the <see cref="AttackItem"/> 
        /// class with default values. This version of the constructor 
        /// assumes that the player has no attack items equipped.
        /// </summary>
        public AttackItem()
        {
            Name = string.Empty;
            Hit = 0;
            Range = 0;
            Weight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackItem"/> 
        /// class with a name and default values for hit strength and 
        /// range. This allows for junk/gag weapons that have no attack 
        /// strength or range.
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
        /// Combines two <see cref="AttackItem"/> instances into a single 
        /// <see cref="AttackComposite"/> item. If either item is null,
        /// the non-null item is  returned. If both items are composites,
        /// their contents are merged. If one item is a composite, the
        /// other item is added to it. If neither item is a composite, a
        /// new composite containing both items is created and returned.
        /// </summary>
        /// <param name="a">The first attack item.</param>
        /// <param name="b">The second attack item.</param>
        /// <returns>A composite attack item containing both input items.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if both attack 
        /// items are null.</exception>
        public static AttackItem operator +(AttackItem a, AttackItem b)
        {
            // we need to deal with every case in a reasonable order
            // handle the most extreme case first: if both are null
            if (a == null && b == null)
            {
                throw new ArgumentNullException("Both attack items are null."); 
            }
            // deal with less extreme case: if either is null
            if (a == null) { return b; }
            if (b == null) { return a; }

            // deal with the next most complicated case
            // if both are composites, merge their items
            // "a is AttackComposite compositeA" checks if a is an 
            // AttackComposite and, if so, cast it and assign it to
            // compositeA. do the same for b
            if (a is AttackComposite compositeA && b is AttackComposite compositeB)
            {
                // create a new composite with the items from compositeA,
                // then add the items from compositeB
                var merged = new AttackComposite(compositeA.Items);
                foreach (var item in compositeB.Items)
                {
                    merged.Add(item);
                }
                return merged;
            }

            // deal with simpler cases where one is a composite
            // if only left is composite, add b's single item
            if (a is AttackComposite left)
            {
                var composite = new AttackComposite(left.Items);
                composite.Add(b);
                return composite;
            }

            // if only right is composite, add a's single item
            if (b is AttackComposite right)
            {
                var composite = new AttackComposite(right.Items);
                composite.Add(a);
                return composite;
            }

            // deal with the simplest case
            // if neither is composite, create a new composite with both
            return new AttackComposite([a, b]);
        }

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
