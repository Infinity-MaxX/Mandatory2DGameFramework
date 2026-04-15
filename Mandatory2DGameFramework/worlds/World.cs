using Mandatory2DGameFramework.config;
using Mandatory2DGameFramework.model.Cretures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    // Some place in the code there must be at least one
    // Operator Overload
    /// <summary>
    /// Represents the game world, including its dimensions, 
    /// difficulty level, with objects and creatures.
    /// </summary>
    /// <remarks>The World class serves as the primary container 
    /// for game state, defining the playable area's boundaries 
    /// and difficulty. Use the FromConfig method to create a 
    /// World instance from a configuration object. The MaxX and 
    /// MaxY properties specify the world's size along the X and 
    /// Y axes, respectively. The Difficulty property determines 
    /// the overall challenge level for the world.</remarks>
    public class World
    {
        #region Instances
        private List<WorldObject> _worldObjects;
        private List<Creature> _creatures;
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
        /// Instatiates a new World object with specified dimensions and difficulty level.
        /// </summary>
        /// <param name="maxX">Sets the maximum X-coordinate value.</param>
        /// <param name="maxY">Sets the maximum Y-coorindate value</param>
        /// <param name="difficulty">Sets the difficulty of the game. Options include 
        /// Beginner, Intermediate, and Expert. Default is set to Beginner.</param>
        public World(int maxX, int maxY, GameDifficulty difficulty = GameDifficulty.Beginner)
        {
            MaxX = maxX;
            MaxY = maxY;
            Difficulty = difficulty;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new instance of the World class 
        /// using the specified game configuration.
        /// </summary>
        /// <param name="config">The configuration settings that define
        /// the world's dimensions and difficulty. Cannot be null.</param>
        /// <returns>A new World instance initialized with the
        /// parameters from the specified configuration.</returns>
        // factory design pattern method
        public static World FromConfig(GameConfig config)
        {
            return new World(config.World.MaxX, config.World.MaxY, config.Difficulty);
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
