using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mandatory2DGameFramework.model.defence;

namespace Mandatory2DGameFramework.model.defence.decorators
{
    /// <summary>
    /// Reduces the hit strength of the wrapped defence item.
    /// </summary>
    public class DefenceDebuffDecorator : DefenceDecorator
    {
        #region Instances
        private readonly int _debuff;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the defence subtracted by the debuff. A check
        /// is employed to ensure the defence output does not return a
        /// negative value. Subclasses may override this to modify the
        /// debuff value.
        /// </summary>
        public override int ReduceHitPoint
        {
            get
            {
                int debuff = _baseDefence.ReduceHitPoint - _debuff;
                if (debuff < 0) { return 0; } else { return debuff; }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new decorator that wraps the specified defence
        /// item with added debuff.
        /// </summary>
        /// <param name="baseDefence">The defence item to debuff.</param>
        /// <param name="debuff">The amount to debuff the defence item
        /// with. Default value is 3.</param>
        public DefenceDebuffDecorator(DefenceItem baseDefence, int debuff = 3)
            : base(baseDefence)
        {
            _debuff = debuff;
            Name = baseDefence.Name + $" {baseDefence.ReduceHitPoint} - {debuff}";
        }
        #endregion
    }
}