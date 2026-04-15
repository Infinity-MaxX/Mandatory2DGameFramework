using Mandatory2DGameFramework.logging;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Cretures
{
    // This needs to be a Template Design Pattern
    // It needs to support the Observer Design Pattern
    // At least one method needs to implement the Strategy Design Pattern
    public class Creature
    {
        public string Name { get; set; }
        public int HitPoint { get; set; }
        // Todo consider how many attack / defence weapons are allowed
        public AttackItem? Attack { get; set; }
        public DefenceItem? Defence { get; set; }

        public Creature()
        {
            Name = string.Empty;
            HitPoint = 100;

            // if multiple attack items are equiped then the creature
            // should hit opponent with the sum of all equipped weapons
            Attack = null;
            // when a creature is hit, the defence items will reduce
            // the incoming attack damage by the sum of ReduceHitPoint
            Defence = null;
        }

        public int Hit()
        {
            throw new NotImplementedException();
        }

        public void ReceiveHit(int hit)
        {
            // conditional before this logging is needed, but for now
            // we just log the info
            Logger.Log.LogInfo($"{Name} received {hit} damage");
        }

        public void Loot(WorldObject obj)
        {
            // conditional before this logging is needed, but for now
            // we just log the error
            Logger.Log.LogError("Tried to loot a non-lootable object");
        }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(HitPoint)}={HitPoint.ToString()}, " +
                $"{nameof(Attack)}={Attack}, " +
                $"{nameof(Defence)}={Defence}}}";
        }
    }
}
