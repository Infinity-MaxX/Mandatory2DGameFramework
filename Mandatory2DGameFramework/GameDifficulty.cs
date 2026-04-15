using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mandatory2DGameFramework.config
{
    /// <summary>
    /// Specifies the available difficulty levels for a game.
    /// </summary>
    /// <remarks>Use this enumeration to select or indicate the 
    /// current difficulty setting. The values typically correspond 
    /// to increasing levels of challenge, with 'Beginner' being 
    /// the easiest and 'Expert' the most difficult. The meaning of 
    /// each level may vary depending on the game implementation.</remarks>
    public enum GameDifficulty
    {
        /// <summary>
        /// Represents the easiest level in the game and 
        /// is designed for new players or those seeking 
        /// a more relaxed gaming experience.
        /// </summary>
        Beginner,
        /// <summary>
        /// Represents a moderate level of difficulty, 
        /// providing a balanced gaming experince for players.
        /// </summary>
        Normal,
        /// <summary>
        /// Represents the most difficuly level in the game, 
        /// suited for experienced players seeking a challenge.
        /// </summary>
        Expert
    }

    /// <summary>
    /// Represents the configuration settings for a game, 
    /// including world parameters and difficulty level.
    /// </summary>
    /// <remarks>This class is typically used to serialize 
    /// or deserialize game configuration data, such as when
    /// loading or saving settings to XML. It serves as the 
    /// root element for game configuration files.</remarks>
    [XmlRoot("GameConfig")]
    public class GameConfig
    {
        /// <summary>
        /// Gets or sets the configuration settings for the world environment.
        /// </summary>
        [XmlElement("World")]
        public WorldConfig World { get; set; } = new WorldConfig();

        /// <summary>
        /// Gets or sets the difficulty level for the game.
        /// </summary>
        [XmlElement("GameDifficulty")]
        public GameDifficulty Difficulty { get; set; } = GameDifficulty.Beginner;
    }

    /// <summary>
    /// Represents configuration settings that define the boundaries of a
    /// of a two-dimensional world.
    /// </summary>
    public class WorldConfig
    {
        /// <summary>
        /// Gets or sets the maximum X-coordinate value.
        /// </summary>
        [XmlElement("MaxX")]
        public int MaxX { get; set; }

        /// <summary>
        /// Gets or sets the maximum Y-coordinate value.
        /// </summary>
        [XmlElement("MaxY")]
        public int MaxY { get; set; }
    }

    /// <summary>
    /// Represents a utility class responsible for loading game configuration.
    /// </summary>
    public static class GameConfigLoader
    {
        /// <summary>
        /// Loads the game configuration from an XML file at the specified path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Throw an exception if the 
        /// config file cannot be found.</exception>
        /// <exception cref="InvalidDataException">Throw an exception if the
        /// game configurations cannot be deserialised.</exception>
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
