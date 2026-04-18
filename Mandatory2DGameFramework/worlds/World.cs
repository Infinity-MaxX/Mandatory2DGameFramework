using Mandatory2DGameFramework.config;
using Mandatory2DGameFramework.gameInterface.observer;
using Mandatory2DGameFramework.helper.logger;
using Mandatory2DGameFramework.model.creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    /// <summary>
    /// Represents the game world, including its dimensions, 
    /// difficulty level, creatures and objects placed in the
    /// world.
    /// </summary>
    /// <remarks>The World class serves as the primary container 
    /// for game state, defining the playable area's boundaries 
    /// and difficulty. Use the FromConfig method to create a 
    /// World instance from a configuration object. The MaxX and 
    /// MaxY properties specify the world's size along the X and 
    /// Y axes, respectively. The Difficulty property determines 
    /// the overall challenge level for the world.</remarks>
    public class World : ICreatureObserver
    {
        #region Instances
        private List<WorldObject> _worldObjects;
        private List<Creature> _creatures;
        private Logger _log;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the maximum X-coordinate value.
        /// </summary>
        public int MaxX { get; set; }
        /// <summary>
        /// Gets or sets the maximum Y-coordinate value.
        /// </summary>
        public int MaxY { get; set; }
        /// <summary>
        /// Gets or sets the difficulty level for the game. 
        /// </summary>
        public GameDifficulty Difficulty { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instatiates a new <see cref="World"/> object with 
        /// specified dimensions and difficulty level.
        /// </summary>
        /// <param name="maxX">Sets the maximum X-coordinate value.
        /// </param>
        /// <param name="maxY">Sets the maximum Y-coordinate value.
        /// </param>
        /// <param name="difficulty">Sets the difficulty of the game. 
        /// Options include Beginner, Intermediate, and Expert. 
        /// Default is set to Beginner.</param>
        public World(int maxX, int maxY, 
            GameDifficulty difficulty = GameDifficulty.Beginner)
        {
            MaxX = maxX;
            MaxY = maxY;
            Difficulty = difficulty;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
            _log = Logger.Log;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new instance of the <see cref="World"/> class 
        /// using the specified game configuration.
        /// </summary>
        /// <param name="config">The configuration settings that 
        /// define the world's dimensions and difficulty. Cannot 
        /// be null.</param>
        /// <returns>A new <see cref="World"/> instance initialized
        /// with the parameters from the specified configuration.</returns>
        // factory design pattern method
        public static World FromConfig(GameConfig config)
        {
            return new World(config.World.MaxX, 
                config.World.MaxY, config.Difficulty);
        }

        /// <summary>
        /// Adds a <see cref="WorldObject"/> to the world.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        public void AddObject(WorldObject obj)
        {
            _worldObjects.Add(obj);
            _log.LogInfo($"Added object: {obj.ToString()} at " +
                $"({obj.X}, {obj.Y})");

        }

        /// <summary>
        /// Removes a <see cref="WorldObject"/> from the world if 
        /// it is marked as removable.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        public void RemoveObject(WorldObject obj)
        {
            if (obj.Removable)
            {
                _worldObjects.Remove(obj);
                _log.LogInfo($"Removed object: {obj.ToString()} from " +
                    $"({obj.X}, {obj.Y})");
            }
        }

        /// <summary>
        /// Adds a <see cref="Creature"/> to the world.
        /// </summary>
        /// <param name="creature">The creature to add.</param>
        public void AddCreature(Creature creature)
        {
            _creatures.Add(creature);
            creature.RegisterObserver(this);
            _log.LogInfo($"Added creature: {creature.ToString()} at " +
                    $"({creature.X}, {creature.Y})");
        }

        /// <summary>
        /// Removes a <see cref="Creature"/> from the world.
        /// </summary>
        /// <param name="creature">The creature to remove.</param>
        public void RemoveCreature(Creature creature)
        {
            _creatures.Remove(creature);
            creature.RemoveObserver(this);
            _log.LogInfo($"Removed creature: {creature.ToString()} from " +
                    $"({creature.X}, {creature.Y})");
        }

        /// <summary>
        /// Called when a creature in the world takes damage.
        /// <param name="creature">The creature to that was hit.</param>
        /// <param name="damage">How much net damage the creature took.</param>
        /// </summary>
        public void OnCreatureHit(Creature creature, int damage)
        {
            _log.LogInfo($"[WORLD] {creature.Name} took {damage} damage.");
        }

        /// <summary>
        /// Called when a creature in the world dies.
        /// <param name="creature">The creature to that has died.</param>
        /// </summary>
        public void OnCreatureDeath(Creature creature)
        {
            _log.LogInfo($"[WORLD] {creature.Name} has died.");
            RemoveCreature(creature);
        }

        /// <summary>
        /// Called when a creature in the world loots an object.
        /// </summary>
        /// <param name="creature">The creature that looted the object.</param>
        /// <param name="obj">The object that was looted.</param>
        public void OnCreatureLoot(Creature creature, WorldObject obj)
        {
            _log.LogInfo($"[WORLD] {creature.Name} has looted {obj.Name}.");
            RemoveObject(obj);
        }

        /// <summary>
        /// Returns all <see cref="WorldObject"/> instances located 
        /// at the specified coordinates.
        /// </summary>
        /// <param name="x">The X-coordinate to check.</param>
        /// <param name="y">The Y-coordinate to check.</param>
        /// <returns>An enumerable collection of <see cref="WorldObject"/> 
        /// instances at the specified coordinates.</returns>
        public IEnumerable<WorldObject> ObjectsAt(int x, int y)
        {
            var found = new List<WorldObject>();
            foreach (var obj in _worldObjects)
            {
                if (obj.X == x && obj.Y == y)
                {
                    found.Add(obj);
                }
            }
            return found;
        }

        /// <summary>
        /// Returns all <see cref="Creature"/> instances located at 
        /// the specified coordinates.
        /// </summary>
        /// <param name="x">The X-coordinate to check.</param>
        /// <param name="y">The Y-coordinate to check.</param>
        /// <returns>An enumerable collection of <see cref="Creature"/> 
        /// instances at the specified coordinates.</returns>
        public IEnumerable<Creature> CreaturesAt(int x, int y)
        {
            var found = new List<Creature>();
            foreach (var cre in _creatures)
            {
                if (cre.X == x && cre.Y == y)
                {
                    found.Add(cre);
                }
            }
            return found;
        }

        /// <summary>
        /// A string representation of the World object, 
        /// including its dimensions and difficulty level.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(MaxX)}={MaxX.ToString()}, " +
                $"{nameof(MaxY)}={MaxY.ToString()}, " +
                $"{nameof(Difficulty)}={Difficulty.ToString()}}}";
        }
        #endregion
    }
}
