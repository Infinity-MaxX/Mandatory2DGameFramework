using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.attack
{
    // There needs to be a subclass for AttackItem that uses the
    // Decorator Design Pattern: example: boost or weaken defence
    // There needs to be a subclass for AttackItem that uses the
    // Composite Design Pattern; it needs to store weapons

    // Note: The AttackItem must have a property weight. A creature
    // must be configurable to be able to carry a maximum weight of
    // attack weapons (attackItems). Hint: May Implemented in the
    // Composite class
    public class AttackItem : WorldObject
    {
        public string Name { get; set; }
        public int Hit { get; set; }
        public int Range { get; set; }

        public AttackItem()
        {
            Name = string.Empty;
            Hit = 0;
            Range = 0;
        }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(Hit)}={Hit.ToString()}, " +
                $"{nameof(Range)}={Range.ToString()}}}";
        }
    }
}
