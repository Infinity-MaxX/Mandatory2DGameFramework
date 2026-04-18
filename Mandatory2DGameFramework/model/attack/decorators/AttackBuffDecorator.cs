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
    public class AttackBuffDecorator : AttackDecorator
    {
        #region Instances
        private readonly int _buff;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the attack item with added buff.
        /// Subclasses may override this to modify the buff value.
        /// </summary>
        public override int Hit
        {
            get { return _baseWeapon.Hit + _buff; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new decorator that wraps the specified attack
        /// item with added buff.
        /// </summary>
        /// <param name="baseWeapon">The attack item to buff.</param>
        /// <param name="buff">The amount to buff the attack item with.
        /// Default value is 5.</param>
        public AttackBuffDecorator(AttackItem baseWeapon, int buff = 5)
            : base(baseWeapon)
        {
            _buff = buff;
            Name = baseWeapon.Name + $" +{buff}";
        }
        #endregion
    }
}
