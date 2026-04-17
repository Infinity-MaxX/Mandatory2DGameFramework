using Mandatory2DGameFramework.gameInterface.observer;
using Mandatory2DGameFramework.gameInterface.strategy;
using Mandatory2DGameFramework.helper.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.combat;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System.Collections.Generic;

namespace Mandatory2DGameFramework.model.creatures
{
    /// <summary>
    /// Represents a creature in the game world and serves as
    /// the base class for all creature types. Implements 
    /// Template Method, Observer, and Strategy design patterns.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Template Method:</b> The <see cref="PerformHit"/> 
    /// method defines the attack flow, while subclasses may 
    /// override <see cref="AfterHit"/> to customize post‑attack 
    /// behavior.
    /// </para>
    /// <para>
    /// <b>Strategy:</b> Damage calculation is delegated to an 
    /// <see cref="IHitStrategy"/> implementation, allowing 
    /// creatures to change combat behavior dynamically.
    /// </para>
    /// <para>
    /// <b>Observer:</b> External observers can subscribe to hit 
    /// and death events using <see cref="RegisterObserver"/> 
    /// and <see cref="RemoveObserver"/>.
    /// </para>
    /// </remarks>
    public abstract class Creature
    {
        #region Instances
        private readonly Logger _log = Logger.Log; // singleton logger instance
        private readonly List<ICreatureObserver> _observers = new(); // for observer pattern
        private readonly List<AttackItem> _attackItems = new();
        private readonly List<DefenceItem> _defenceItems = new();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the creature's display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the creature's current hit points.
        /// </summary>
        public int HitPoint { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the creature is dead.
        /// </summary>
        public bool IsDead { get { return HitPoint <= 0; } }

        /// <summary>
        /// Gets or sets the maximum total weight of attack items 
        /// the creature can carry.
        /// </summary>
        public int MaxAttackWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum total weight of defence items 
        /// the creature can carry.
        /// </summary>
        public int MaxDefenceWeight { get; set; }

        /// <summary>
        /// Gets or sets the hit strategy used to calculate 
        /// outgoing damage.
        /// </summary>
        public IHitStrategy HitStrategy { get; set; }

        /// <summary>
        /// Gets or sets the creature's X‑coordinate in the world.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the creature's Y‑coordinate in the world.
        /// </summary>
        public int Y { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> 
        /// class.
        /// </summary>
        /// <param name="name">The creature's name.</param>
        /// <param name="hitPoint">The starting hit points. 
        /// Default is 100.</param>
        /// <param name="maxWeight">The maximum attack weight the 
        /// creature can carry. Default is 50.</param>
        protected Creature(string name, int hitPoint = 100, int maxWeight = 50)
        {
            Name = name;
            HitPoint = hitPoint;
            HitStrategy = new BalancedHitStrategy();
            MaxAttackWeight = maxWeight;
        }
        #endregion

        #region Methods
        // ----------------------------------------------------------
        // TEMPLATE METHOD
        // ----------------------------------------------------------

        // Hook methods should be protected and virtual, allowing
        // subclasses to override them without exposing them as part
        // of the public API.

        /// <summary>
        /// Hook method invoked after a successful hit. Subclasses 
        /// may override
        /// this to implement custom post‑attack behavior.
        /// </summary>
        /// <param name="target">The creature that was hit.</param>
        protected virtual void AfterHit(Creature target)
        {
            // Optional override
        }

        /// <summary>
        /// Hook method invoked before a hit. Subclasses may override
        /// this to implement custom pre‑attack behavior.
        /// </summary>
        /// <param name="target">The creature that will be hit.</param>
        protected virtual void BeforeHit(Creature target)
        {
            // Optional override
        }

        /// <summary>
        /// Calculates the outgoing damage using the configured hit 
        /// strategy.
        /// </summary>
        /// <returns>The calculated damage value.</returns>
        protected virtual int CalculateDamage()
        {
            return HitStrategy.CalculateDamage(_attackItems);
        }

        /// <summary>
        /// Performs an attack on the specified target using the 
        /// Template Method pattern.
        /// </summary>
        /// <param name="target">The creature being attacked.</param>
        /// <returns>The amount of damage dealt.</returns>
        /// <remarks>
        /// This method defines the attack sequence:
        /// <list type="number">
        /// <item><description>Invoke a customizable hook 
        /// (<see cref="BeforeHit"/>).</description></item>
        /// <item><description>Calculate damage using the active hit 
        /// strategy.</description></item>
        /// <item><description>Apply the damage to the target.
        /// </description></item>
        /// <item><description>Invoke a customizable hook 
        /// (<see cref="AfterHit"/>).</description></item>
        /// </list>
        /// </remarks>
        public int PerformHit(Creature target)
        {
            BeforeHit(target);
            int damage = CalculateDamage();
            target.ReceiveHit(damage);
            AfterHit(target);
            return damage;
        }

        // ---------------------------------------------------------
        // OBSERVER PATTERN
        // ---------------------------------------------------------

        /// <summary>
        /// Registers an observer that will be notified when the 
        /// creature is hit or dies.
        /// </summary>
        /// <param name="observer">The observer to register.</param>
        public void RegisterObserver(ICreatureObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        /// <summary>
        /// Removes a previously registered observer.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        public void RemoveObserver(ICreatureObserver observer)
        {
            _observers.Remove(observer);
        }

        // These methods are private because they are only called
        // internally to notify observers of events. They should
        // not be exposed as part of the public API.
        private void NotifyHit(int damage)
        {
            foreach (var obs in _observers)
            {
                obs.OnCreatureHit(this, damage);
            }
        }

        private void NotifyDeath()
        {
            foreach (var obs in _observers)
            {
                obs.OnCreatureDeath(this);
            }
        }

        // ---------------------------------------------------------
        // COMBAT
        // ---------------------------------------------------------

        /// <summary>
        /// Calculates the total defence value from all equipped 
        /// defence items.
        /// </summary>
        /// <returns>The total defence value.</returns>
        public int TotalDefence()
        {
            int sum = 0;
            foreach (var d in _defenceItems)
            {
                sum += d.ReduceHitPoint;
            }
            return sum;
        }
        /// <summary>
        /// Applies incoming damage to the creature, reduced by all 
        /// equipped defence items.
        /// </summary>
        /// <param name="damage">The raw incoming damage.</param>
        public void ReceiveHit(int damage)
        {
            int reduced = damage - TotalDefence();
            if (reduced < 0) { reduced = 0; }

            HitPoint -= reduced;

            _log.LogInfo($"{Name} receives {reduced} damage (raw {damage}).");

            NotifyHit(reduced);

            if (IsDead)
            {
                _log.LogInfo($"{Name} has died.");
                NotifyDeath();
            }
        }

        // ---------------------------------------------------------
        // INVENTORY
        // ---------------------------------------------------------

        /// <summary>
        /// Attempts to add an attack item to the creature's inventory.
        /// </summary>
        /// <param name="item">The attack item to add.</param>
        /// <returns>
        /// <c>true</c> if the item was added successfully; otherwise 
        /// <c>false</c>.
        /// </returns>
        public bool AddAttackItem(AttackItem item)
        {
            if (CurrentAttackWeight() + item.Weight > MaxAttackWeight)
            {
                _log.LogWarning($"{Name} cannot carry {item.Name}, too heavy.");
                return false;
            }
            _attackItems.Add(item);
            return true;
        }

        // Note: There is no weight limit for defence items in this design
        // ToDo: A similar weight check should be implemented for defence
        // items as well.

        /// <summary>
        /// Adds a defence item to the creature's inventory.
        /// </summary>
        /// <param name="item">The defence item to add.</param>
        public void AddDefenceItem(DefenceItem item)
        {
            _defenceItems.Add(item);
        }

        /// <summary>
        /// Checks the total weight of all currently equipped attack items.
        /// </summary>
        /// <returns>The total weight of attack items.</returns>
        public int CurrentAttackWeight()
        {
            int sum = 0;
            foreach (var a in _attackItems)
            {
                sum += a.Weight;
            }
            return sum;
        }

        // ---------------------------------------------------------
        // LOOTING
        // ---------------------------------------------------------

        /// <summary>
        /// Attempts to loot a world object. If the object is lootable 
        /// and is an attack or defence item, it is added to the 
        /// creature's inventory.
        /// </summary>
        /// <param name="obj">The world object to loot.</param>
        public void Loot(WorldObject obj)
        {
            if (!obj.Lootable)
            {
                _log.LogWarning($"{Name} tried to loot non-lootable object.");
                return;
            }

            else
            {
                if (obj is AttackItem atk)
                {
                    AddAttackItem(atk);
                    _log.LogInfo($"{Name} looted {atk.Name} (Attack Item).");
                }
                else if (obj is DefenceItem def)
                {
                    AddDefenceItem(def);
                    _log.LogInfo($"{Name} looted {def.Name} (Defence Item).");
                }
                else
                {
                    _log.LogInfo($"{Name} looted {obj.Name}, but it has no effect.");
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the creature, including
        /// name, hit points, and world position.
        /// </summary>
        public override string ToString()
        {
            return $"{Name} (HP: {HitPoint}, Pos: {X},{Y})";
        }
        #endregion
    }
}
