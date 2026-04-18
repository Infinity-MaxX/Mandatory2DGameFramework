using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Mandatory2DGameFramework.model.attack
{
    /// <summary>
    /// Represents a base decorator for attack items, allowing additional
    /// behavior to be layered on top of an existing AttackItem instance.
    /// </summary>
    /// <remarks>
    /// This class implements the Decorator Design Pattern. It wraps another
    /// AttackItem and forwards all property calls to the wrapped item unless
    /// overridden by subclasses. Concrete decorators can modify Hit, Range,
    /// Weight, or other behavior without changing the underlying item.
    /// </remarks>
    public abstract class AttackDecorator : AttackItem
    {
        #region Instances
        /// <summary>
        /// The wrapped attack item being decorated.
        /// </summary>
        protected readonly AttackItem _baseWeapon;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the hit strength of the decorated attack item.
        /// Subclasses may override this to modify the hit value.
        /// </summary>
        public override int Hit
        {
            get { return base.Hit; }
            set { base.Hit = value; }
        }

        /// <summary>
        /// Gets or sets the range of the decorated attack item.
        /// Subclasses may override this to modify the range.
        /// </summary>
        public override int Range
        {
            get { return base.Range; }
            set { base.Range = value; }
        }

        /// <summary>
        /// Gets or sets the weight of the decorated attack item.
        /// Subclasses may override this to modify the weight.
        /// </summary>
        public override int Weight
        {
            get { return base.Weight; }
            set { base.Weight = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new decorator that wraps the specified attack item.
        /// </summary>
        /// <param name="baseWeapon">The attack item to decorate.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the decorated attack item is null.
        /// </exception>
        public AttackDecorator(AttackItem baseWeapon)
        {
            if (baseWeapon == null)
            {
                throw new ArgumentNullException(nameof(baseWeapon), "Decorated attack item cannot be null.");
            }
            _baseWeapon = baseWeapon;
            Name = baseWeapon.Name + " (Decorated)";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a string representation of the decorated attack item.
        /// </summary>
        public override string ToString()
        {
            return $"{{Decorator: {Name}, Hit={Hit}, " +
                $"Range={Range}, Weight={Weight}}}";
        }
        #endregion
    }
}
