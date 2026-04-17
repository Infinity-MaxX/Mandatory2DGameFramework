using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Mandatory2DGameFramework.model.attack
{
    /// <summary>
    /// Represents a composite attack item that groups multiple 
    /// AttackItem instances into a single logical weapon.
    /// </summary>
    /// <remarks>
    /// This class implements the Composite design pattern, allowing 
    /// multiple attack items to be treated as a single item. The 
    /// composite sums the Hit, Range, and Weight values of all 
    /// contained items. It can be used by creatures to represent 
    /// carrying or equipping multiple weapons at once.
    /// </remarks>
    public class AttackComposite : AttackItem
    {
        #region Instances
        private readonly List<AttackItem> _items;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the collection of attack items.
        /// </summary>
        public IEnumerable<AttackItem> Items { get { return _items; } }

        /// <summary>
        /// Gets the total hit strength of all attack items.
        /// </summary>
        public override int Hit
        {
            get { return _items.Sum(attackItem => attackItem.Hit); }
        }

        /// <summary>
        /// Gets the maximum range among all attack items.
        /// </summary>
        /// <remarks>
        /// Range is not summed, because a creature cannot attack farther 
        /// than its longest-range weapon.
        /// </remarks>
        public override int Range
        {
            get
            {
                if (_items.Count == 0) { return 0; }
                else { return _items.Max(i => i.Range); }
            }
        }

        /// <summary>
        /// Gets the total weight of all attack items in the composite.
        /// </summary>
        public override int Weight { get { return _items.Sum(i => i.Weight); } }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new empty composite attack item.
        /// </summary>
        public AttackComposite()
        {
            _items = new List<AttackItem>();
            Name = "CompositeWeapon";
        }

        /// <summary>
        /// Initializes a new composite attack item with an initial list of items.
        /// </summary>
        /// <param name="items">The attack items to include in the composite.</param>
        public AttackComposite(IEnumerable<AttackItem> items)
        {
            _items = items.ToList();
            Name = "CompositeWeapon";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a single attack item to the composite.
        /// </summary>
        /// <param name="item">The attack item to add.</param>
        public void Add(AttackItem item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Removes a single attack item from the composite.
        /// </summary>
        /// <param name="item">The attack item to remove.</param>
        public void Remove(AttackItem item)
        {
            _items.Remove(item);
        }

        /// <summary>
        /// Returns a string representation of the composite, including 
        /// total hit, range, weight, and the number of items.
        /// </summary>
        public override string ToString()
        {
            return $"{{Composite: Count={_items.Count}, " +
                $"Hit={Hit}, Range={Range}, Weight={Weight}}}";
        }
        #endregion
    }
}
