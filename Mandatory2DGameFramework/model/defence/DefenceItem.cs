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
    /// <remarks>
    /// This class implements the Composite design pattern, allowing 
    /// multiple defence items to be treated as a single item. The 
    /// composite sums the ReduceDamage and Weight values of all 
    /// contained items. It can be used by creatures to represent 
    /// carrying or equipping multiple defence items at once.
    /// </remarks>
    public class DefenceItem : WorldObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the amount of hit points to reduce 
        /// from the creature when damage is applied.
        /// </summary>
        public virtual int ReduceDamage { get; set; }
        /// <summary>
        /// Gets or sets the weight of the defence item, which 
        /// will be used to determine how much it affects the 
        /// player's mobility or weight capacity in subclasses.
        /// </summary>
        public virtual int Weight { get; set; }
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
            ReduceDamage = 0;
            Weight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the DefenceItem class with a  
        /// name and default values for hit point reduction and weight.
        /// This allows for junk/gag defensive items that have no
        /// defensive strength or weight.
        /// </summary>
        public DefenceItem(string name, int reduceDamage, int weight)
        {
            Name = name;
            ReduceDamage = reduceDamage;
            Weight = weight;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Combines two <see cref="DefenceItem"/> instances into a single 
        /// <see cref="DefenceComposite"/> item. If either item is null,
        /// the non-null item is  returned. If both items are composites,
        /// their contents are merged. If one item is a composite, the
        /// other item is added to it. If neither item is a composite, a
        /// new composite containing both items is created and returned.
        /// </summary>
        /// <param name="a">The first defence item.</param>
        /// <param name="b">The second defence item.</param>
        /// <returns>A composite defence item containing both input items.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if both defence 
        /// items are null.</exception>
        public static DefenceItem operator +(DefenceItem a, DefenceItem b)
        {
            // we need to deal with every case in a reasonable order
            // handle the most extreme case first: if both are null
            if (a == null && b == null)
            {
                throw new ArgumentNullException("Both defence items are null."); 
            }
            // deal with less extreme case: if either is null
            if (a == null) { return b; }
            if (b == null) { return a; }

            // deal with the next most complicated case
            // if both are composites, merge their items
            // "a is DefenceComposite compositeA" checks if a is an 
            // DefenceComposite and, if so, cast it and assign it to
            // compositeA. do the same for b
            if (a is DefenceComposite compositeA && b is DefenceComposite compositeB)
            {
                // create a new composite with the items from compositeA,
                // then add the items from compositeB
                var merged = new DefenceComposite(compositeA.Items);
                foreach (var item in compositeB.Items)
                {
                    merged.Add(item);
                }
                return merged;
            }

            // deal with simpler cases where one is a composite
            // if only left is composite, add b's single item
            if (a is DefenceComposite left)
            {
                var composite = new DefenceComposite(left.Items);
                composite.Add(b);
                return composite;
            }

            // if only right is composite, add a's single item
            if (b is DefenceComposite right)
            {
                var composite = new DefenceComposite(right.Items);
                composite.Add(a);
                return composite;
            }

            // deal with the simplest case
            // if neither is composite, create a new composite with both
            return new DefenceComposite([a, b]);
        }
        
        /// <summary>
        /// A string representation of the DefenceItem, including
        /// its name and the amount of hit points it reduces.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(ReduceDamage)}={ReduceDamage}, " +
                $"{nameof(Weight)}={Weight}}}";
        }
        #endregion
    }
}
