using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mandatory2DGameFramework.model.attack;

namespace Mandatory2DGameFramework.model.attack.decorators
{
    /// <summary>
    /// Increases the hit strength of the wrapped attack item.
    /// </summary>
    public class AttackBoostDecorator : AttackDecorator
    {
        #region Instances
        private readonly int _boost;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the attack item with added booster.
        /// Subclasses may override this to modify the hit value.
        /// </summary>
        public override int Hit
        {
            get { return base.Hit + _boost; }
            set { base.Hit = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new decorator that wraps the specified attack item
        /// with added booster.
        /// </summary>
        /// <param name="item">The attack item to boost.</param>
        /// <param name="boost">The amount to boost the attack item with.
        /// Default value is 5.</param>
        public AttackBoostDecorator(AttackItem item, int boost = 5)
            : base(item)
        {
            _boost = boost;
            Name = item.Name + " +AttackBoost";
        }
        #endregion
    }
}
