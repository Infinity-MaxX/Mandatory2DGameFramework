using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mandatory2DGameFramework.model.attack;

namespace Mandatory2DGameFramework.model.attack.decorators
{
    /// <summary>
    /// Reduces the hit strength of the wrapped attack item.
    /// </summary>
    public class AttackDebuffDecorator : AttackDecorator
    {
        #region Instances
        private readonly int _debuff;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the attack subtracted by the debuff. A check
        /// is employed to ensure the damage output does not return a
        /// negative value. Subclasses may override this to modify the
        /// debuff value.
        /// </summary>
        public override int Hit
        {
            get
            {
                int debuff = _baseWeapon.Hit - _debuff;
                if (debuff < 0) { return 0; } else { return debuff; }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new decorator that wraps the specified attack
        /// item with added debuff.
        /// </summary>
        /// <param name="baseWeapon">The attack item to debuff.</param>
        /// <param name="debuff">The amount to debuff the attack item
        /// with. Default value is 3.</param>
        public AttackDebuffDecorator(AttackItem baseWeapon, int debuff = 3)
            : base(baseWeapon)
        {
            _debuff = debuff;
            Name = baseWeapon.Name + $" {baseWeapon.Hit} - {debuff}";
        }
        #endregion
    }
}

