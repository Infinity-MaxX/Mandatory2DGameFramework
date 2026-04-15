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
    public class World
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public GameDifficulty Difficulty { get; set; }

        // world objects
        private List<WorldObject> _worldObjects;
        // world creatures
        private List<Creature> _creatures;

        public World(int maxX, int maxY, GameDifficulty difficulty = GameDifficulty.Beginner)
        {
            MaxX = maxX;
            MaxY = maxY;
            Difficulty = difficulty;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
        }

        // factory design pattern method
        public static World FromConfig(GameConfig config)
        {
            return new World(config.World.MaxX, config.World.MaxY, config.Difficulty);
        }

        public override string ToString()
        {
            return $"{{{nameof(MaxX)}={MaxX.ToString()}, " +
                $"{nameof(MaxY)}={MaxY.ToString()}, " +
                $"{nameof(Difficulty)}={Difficulty.ToString()}}}";
        }
    }
}
