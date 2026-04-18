using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    /// <summary>
    /// Represents a base decorator for defence items, allowing additional
    /// behavior to be layered on top of an existing DefenceItem instance.
    /// </summary>
    /// <remarks>
    /// This class implements the Decorator Design Pattern. It wraps another
    /// DefenceItem and forwards all property calls to the wrapped item unless
    /// overridden by subclasses. Concrete decorators can modify ReduceHitPoint,
    /// Weight, or other behavior without changing the underlying item.
    /// </remarks>
    public abstract class DefenceDecorator : DefenceItem
    {
        #region Instances
        /// <summary>
        /// The wrapped defence item being decorated.
        /// </summary>
        protected readonly DefenceItem _baseDefence;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the ReduceHitPoint of the decorated defence item.
        /// Subclasses may override this to modify the hit value.
        /// </summary>
        public override int ReduceHitPoint
        {
            get { return _baseDefence.ReduceHitPoint; }
            set {  _baseDefence.ReduceHitPoint = value; }
        }

        /// <summary>
        /// Gets or sets the weight of the decorated defence item.
        /// Subclasses may override this to modify the weight.
        /// </summary>
        public override int Weight
        {
            get { return _baseDefence.Weight; }
            set { _baseDefence.Weight = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new decorator that wraps the specified defence item.
        /// </summary>
        /// <param name="baseDefence">The defence item to decorate.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the decorated defence item is null.
        /// </exception>
        public DefenceDecorator(DefenceItem baseDefence)
        {
            if (baseDefence == null)
            {
                throw new ArgumentNullException(nameof(baseDefence), "Decorated defence item cannot be null.");
            }
            _baseDefence = baseDefence;
            Name = baseDefence.Name + " (Decorated)";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a string representation of the decorated defence item.
        /// </summary>
        public override string ToString()
        {
            return $"{{Decorator: {Name}, ReduceHitPoint={ReduceHitPoint}, " +
                $"Weight={Weight}}}";
        #endregion
        }
    }
}
