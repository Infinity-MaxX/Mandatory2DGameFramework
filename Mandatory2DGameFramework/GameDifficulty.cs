using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mandatory2DGameFramework.config
{
    public enum GameDifficulty
    {
        Beginner,
        Normal,
        Expert
    }

    [XmlRoot("GameConfig")]
    public class GameConfig
    {
        [XmlElement("World")]
        public WorldConfig World { get; set; } = new WorldConfig();

        [XmlElement("GameDifficulty")]
        public GameDifficulty Difficulty { get; set; } = GameDifficulty.Beginner;
    }

    public class WorldConfig
    {
        [XmlElement("MaxX")]
        public int MaxX { get; set; }

        [XmlElement("MaxY")]
        public int MaxY { get; set; }
    }

    public static class GameConfigLoader
    {
        public static GameConfig Load(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Config file not found", filePath);

            var serializer = new XmlSerializer(typeof(GameConfig));

            using var stream = File.OpenRead(filePath);
            var config = (GameConfig?)serializer.Deserialize(stream);

            if (config == null)
                throw new InvalidDataException("Could not deserialize GameConfig");

            return config;
        }
    }
}
