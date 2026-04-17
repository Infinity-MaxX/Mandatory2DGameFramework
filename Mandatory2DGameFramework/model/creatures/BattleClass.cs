using Mandatory2DGameFramework.model.combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.creatures.classes
{
    /// <summary>
    /// A concrete implementation of the Creature class 
    /// representing a Warrior character instance.
    /// </summary>
    public class Warrior : Creature
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Warrior class 
        /// with the specified name.
        /// </summary>
        /// <param name="name">The name of the warrior.</param>
        public Warrior(string name) : base(name)
        {
            HitStrategy = new AggressiveHitStrategy();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hooks into the hit process after an attack is performed, 
        /// allowing the warrior to gloat.
        /// </summary>
        /// <param name="target">The creature that was hit.</param>
        protected override void AfterHit(Creature target)
        {
            Console.WriteLine($"{Name} shouts victoriously at {target.Name}!");
        }
        #endregion
    }

    /// <summary>
    /// A concrete implementation of the Creature class representing
    /// a Mage character instance.
    /// </summary>
    public class Mage : Creature
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Mage class with the 
        /// specified name.
        /// </summary>
        /// <param name="name">The name of the mage.</param>
        public Mage(string name) : base(name)
        {
            HitStrategy = new DefensiveHitStrategy();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hooks into the hit process before an attack is performed, 
        /// allowing the mage to prepare a spell
        /// </summary>
        /// <param name="target">The creature that will be targeted by 
        /// the spell.</param>
        protected override void BeforeHit(Creature target)
        {
            Console.WriteLine($"{Name} exclaims a spell at {target.Name}!");
        }
        #endregion
    }

}
