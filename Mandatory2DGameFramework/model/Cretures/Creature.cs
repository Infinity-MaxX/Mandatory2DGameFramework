using Mandatory2DGameFramework.helper.generator;
using Mandatory2DGameFramework.helper.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mandatory2DGameFramework.model.Cretures
{
    // This needs to be a Template Design Pattern
    // It needs to support the Observer Design Pattern
    // At least one method needs to implement the
    // Strategy Design Pattern

    /// <summary>
    /// Represents a creature in the game world, encapsulating 
    /// its name, hit points, and equipped attack and defence
    /// items.
    /// </summary>
    /// <remarks>The Creature class serves as a base for entities 
    /// that participate in combat and interact with the environment. 
    /// It provides properties for managing the creature's state 
    /// and equipment, and defines methods for handling combat actions 
    /// such as attacking, receiving damage, and looting objects.
    /// Derived classes can extend or override behavior to implement 
    /// specific creature types or advanced combat logic.</remarks>
    public class Creature
    {
        #region Instances
        private NumberGenerator _generator;
        private Logger _log;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name associated with the creature.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the current hit points for the creature.
        /// </summary>
        public int HitPoint { get; set; }
        // Todo consider how many attack / defence weapons are allowed
        /// <summary>
        /// Gets or sets the attack item equipped by the creature. When 
        /// the creature performs an attack, it will use the hit strength 
        /// specified in the equipped attack item to determine the damage 
        /// inflicted on the enemy. If no attack item is equipped, the 
        /// creature will not be able to inflict damage through attacks.
        /// </summary>
        public AttackItem? Attack { get; set; }
        /// <summary>
        /// Gets or sets the defence item equipped by the creature. The
        /// creature can have no defence item equipped, in which case this
        /// property will be null. When the creature receives a hit, it
        /// will take the incoming damage at full strength. If a defence 
        /// item is equipped, the damage will be reduced by the amount 
        /// specified in the defence item's ReduceHitPoint property.
        /// </summary>
        public DefenceItem? Defence { get; set; }
        /// <summary>
        /// Checks if a creature is dead by evaluating whether its hit
        /// points are less or equal to zero.
        /// </summary>
        public bool IsDead { get { return HitPoint <= 0; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Creature class with default values.
        /// </summary>
        public Creature()
        {
            _generator = new NumberGenerator();
            _log = Logger.Log;

            Name = string.Empty;
            HitPoint = 100;

            // if multiple attack items are equiped then the creature
            // should hit the opponent with the sum of all equipped weapons
            Attack = null;
            // when a creature is hit, the defence items will reduce
            // the incoming attack damage by the sum of ReduceHitPoint
            Defence = null;

            Reset();
        }
        /// <summary>
        /// Initializes a new instance of the Creature class with specified 
        /// values for name, hit points, attack item, and defence item.
        /// </summary>
        /// <param name="name">Instantiates the name of the creature. Must 
        /// be provided at the start of initialisation.</param>
        /// <param name="hitPoint">Instantiates the amount of HitPoints. 
        /// Defaults to 100 if no argument is provided.</param>
        /// <param name="attack">Instantiates the attack items. If the creature
        /// has none at the start of initalisation, defaults to null.</param>
        /// <param name="defence">Instantiates the defence items. If the creature
        /// has none at the start of initialisation, defaults to null.</param>
        public Creature(string name, int hitPoint = 100, 
            AttackItem? attack = null, DefenceItem? defence = null)
        {
            _generator = new NumberGenerator();
            _log = Logger.Log;

            Name = name;
            HitPoint = hitPoint;
            Attack = attack;
            Defence = defence;

            Reset();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Simulates the creature performing an attack by generating a 
        /// random amount of damage within a specified range. The damage
        /// is determined by a minimum value of 10 and a maximum value 
        /// of 30, which can be adjusted as needed. The method also logs
        /// the amount of damage dealt for informational purposes.
        /// </summary>
        /// <returns>The damage dealt to an opponent based on a number 
        /// generated randomly between specified interval.</returns>
        public int Hit()
        {
            int damage = _generator.Next(10, 30);
            _log.LogInfo($"{Name} dealt {damage} damage!");
            return damage;
        }

        /// <summary>
        /// Simulates the amount of a damage a creature receives and deducts 
        /// it from the creature's hit points. If the creature has a defence 
        /// item equipped, the incoming damage will be reduced by the amount 
        /// specified in the defence item's ReduceHitPoint property before 
        /// being applied to the creature's hit points. The method also logs 
        /// the amount of damage received for informational purposes.
        /// </summary>
        /// <param name="hit">The amount of hit point damage recieved.</param>
        public void ReceiveHit(int hit)
        {
            DefenceItem? defence = Defence;

            if (defence == null) 
            {
                HitPoint = HitPoint - hit;
                _log.LogInfo($"{Name} has no defence item equipped, " +
                    $"and takes the full hit of {hit} damage! \n{Name} " +
                    $"is down to {HitPoint} hit points.");
            }
            else
            {
                int reducedHit = hit - defence.ReduceHitPoint;
                HitPoint = HitPoint - reducedHit;
                _log.LogInfo($"{Name} has a defence item equipped that " +
                    $"reduces the incoming hit by {defence.ReduceHitPoint}, " +
                    $"resulting in {reducedHit} damage taken! \n{Name} is " +
                    $"down to {HitPoint} hit points.");
            }

            if (IsDead)
            {
                _log.LogInfo($"{Name} died!");
            }
        }

        /// <summary>
        /// Resets the creature's hit points to the default value of 100.
        /// </summary>
        public void Reset()
        {
            HitPoint = 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Loot(WorldObject obj)
        {
            // conditional before this logging is needed, but for now
            // we just log the error
            _log.LogError("Tried to loot a non-lootable object");
        }

        /// <summary>
        /// A string representation of the Creature, including its name, 
        /// hit points, and the details of its equipped attack and defence 
        /// items (if any are equipped).
        /// </summary>
        /// <returns>Returns the creature's name, its current hitspoints,
        /// and the details of its equiptment if any are equipped.</returns>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(HitPoint)}={HitPoint.ToString()}, " +
                $"{nameof(Attack)}={Attack}, " +
                $"{nameof(Defence)}={Defence}}}";
        }
        #endregion
    }
}
