using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mandatory2DGameFramework.model.defence;

namespace Mandatory2DGameFramework.model.defence.decorators
{
    /// <summary>
    /// Increases the reduce hit point of the wrapped defence item.
    /// </summary>
    public class DefenceBuffDecorator : DefenceDecorator
    {
        #region Instances
        private readonly int _buff;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the defence item with added buff.
        /// Subclasses may override this to modify the buff value.
        /// </summary>
        public override int ReduceHitPoint
        {
            get { return _baseDefence.ReduceHitPoint + _buff; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new decorator that wraps the specified defence
        /// item with added buff.
        /// </summary>
        /// <param name="baseDefence">The defence item to buff.</param>
        /// <param name="buff">The amount to buff the defence item with.
        /// Default value is 5.</param>
        public DefenceBuffDecorator(DefenceItem baseDefence, int buff = 5)
            : base(baseDefence)
        {
            _buff = buff;
            Name = baseDefence.Name + $" +{buff}";
        }
        #endregion
    }
}
