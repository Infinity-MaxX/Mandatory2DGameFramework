using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    /// <summary>
    /// Represents a composite defence item that groups multiple 
    /// <see cref="DefenceItem"/> instances into a single logical 
    /// defence.
    /// </summary>
    public class DefenceComposite : DefenceItem
    {
        #region Instances
        private readonly List<DefenceItem> _items;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the collection of defence items.
        /// </summary>
        public IEnumerable<DefenceItem> Items { get { return _items; } }

        /// <summary>
        /// Gets the total hit strength of all defence items.
        /// </summary>
        public override int ReduceDamage
        {
            get { return _items.Sum(defenceItem => defenceItem.ReduceDamage); }
        }

        /// <summary>
        /// Gets the total weight of all defence items in the composite.
        /// </summary>
        public override int Weight { get { return _items.Sum(i => i.Weight); } }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new empty composite defence item.
        /// </summary>
        public DefenceComposite()
        {
            _items = new List<DefenceItem>();
            Name = "CompositeDefence";
        }

        /// <summary>
        /// Initializes a new composite defence item with an initial 
        /// list of items.
        /// </summary>
        /// <param name="items">The defence items to include in the 
        /// composite.</param>
        public DefenceComposite(IEnumerable<DefenceItem> items)
        {
            _items = items.ToList();
            Name = "CompositeDefence";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a single defence item to the composite.
        /// </summary>
        /// <param name="item">The defence item to add.</param>
        public void Add(DefenceItem item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Removes a single defence item from the composite.
        /// </summary>
        /// <param name="item">The defence item to remove.</param>
        public void Remove(DefenceItem item)
        {
            _items.Remove(item);
        }

        /// <summary>
        /// Returns a string representation of the composite, including 
        /// total ReduceDamage, weight, and the number of items.
        /// </summary>
        public override string ToString()
        {
            return $"{{Composite: Count={_items.Count}, " +
                $"ReduceDamage={ReduceDamage}, Weight={Weight}}}";
        }
        #endregion
    }
}
