using Mandatory2DGameFramework.model.Cretures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.gameInterface.observer
{
    /// <summary>
    /// Interface for observing events related to creatures, 
    /// such as being hit or dying.
    /// </summary>
    public interface ICreatureObserver
    {
        /// <summary>
        /// A method that is called when a creature is hit, 
        /// providing the creature and the damage taken.
        /// </summary>
        /// <param name="creature">The creature that was hit.</param>
        /// <param name="damage">The amount of damage taken by the 
        /// creature.</param>
        void OnCreatureHit(Creature creature, int damage);
        /// <summary>
        /// A method that is called when a creature dies.
        /// </summary>
        /// <param name="creature">The creature that died.</param>
        void OnCreatureDeath(Creature creature);
    }
}
