using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Mandatory2DGameFramework.config
{
    /// <summary>
    /// Specifies the available difficulty levels for a game.
    /// </summary>
    /// <remarks>Use this enumeration to select or indicate the 
    /// current difficulty setting. The values typically correspond 
    /// to increasing levels of challenge, with 'Beginner' being 
    /// the easiest and 'Expert' the most difficult.
    /// </remarks>
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
        /// providing a balanced gaming experience for players.
        /// </summary>
        Normal,
        /// <summary>
        /// Represents the most difficult level in the game, 
        /// suited for experienced players seeking a challenge.
        /// </summary>
        Expert
    }

    /// <summary>
    /// Represents configuration settings that define the boundaries
    /// of a two-dimensional world.
    /// </summary>
    public class WorldConfig
    {
        /// <summary>
        /// Gets or sets the maximum X-coordinate value.
        /// Mapped onto the XML element name "MaxX" for
        /// deserialization from the game configuration file.
        /// </summary>
        [XmlElement("MaxX")]
        public int MaxX { get; set; }

        /// <summary>
        /// Gets or sets the maximum Y-coordinate value.
        /// Mapped onto the XML element name "MaxY" for
        /// deserialization from the game configuration file.
        /// </summary>
        [XmlElement("MaxY")]
        public int MaxY { get; set; }
    }

    /// <summary>
    /// Represents the configuration settings for a game, including
    /// parameters that define the world and the game's difficulty level.
    /// </summary>
    /// <remarks>This class is used to deserialize game configuration data 
    /// from an XML file. It contains properties that correspond to various 
    /// settings in the game, such as world dimensions and difficulty level. 
    /// </remarks>
    [XmlRoot("GameConfig")]
    public class GameConfig
    {
        /// <summary>
        /// Gets or sets the configuration settings for the world 
        /// environment.
        /// </summary>
        [XmlElement("World")]
        public WorldConfig World { get; set; } = new WorldConfig();

        /// <summary>
        /// Gets or sets the difficulty level for the game. Default
        /// value is set to 'Beginner' to provide an accessible starting
        /// point for players.
        /// </summary>
        [XmlElement("GameDifficulty")]
        public GameDifficulty Difficulty { get; set; } = GameDifficulty.Beginner;
    }

    /// <summary>
    /// Represents a utility class responsible for loading game configuration.
    /// </summary>
    public static class GameConfigLoader
    {
        /// <summary>
        /// Loads and reads the game configuration from an XML file at the 
        /// specified path.
        /// </summary>
        /// <param name="filePath">The path to the XML file containing the game 
        /// configuration.</param>
        /// <returns>The deserialized <see cref="GameConfig"/> object.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the config file 
        /// cannot be found.</exception>
        /// <exception cref="InvalidDataException">Thrown if the game 
        /// configurations cannot be deserialized.</exception>
        public static GameConfig Load(string filePath)
        {
            // always check the most extreme case first
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Config file not found", filePath);
            }

            // use a 'using' statement to ensure that the XmlReader is properly
            // disposed of after use, preventing resource leaks and ensuring that
            // file handles are released.
            using var reader = XmlReader.Create(filePath);

            // serializer needed to build XML objects from the config file.
            // XmlReader can't do that, it can only read the XML structure and
            // content but not convert it into objects, and converting the nodes
            // into objects is what we need to get the game configuration data in
            // a usable form. doing it manually would be very tedious and error-
            // prone, so we use the XmlSerializer to handle the deserialization
            // process for us.
            var serializer = new XmlSerializer(typeof(GameConfig));

            // typecast the deserialized object to GameConfig for easier access to
            // its properties
            var config = (GameConfig?)serializer.Deserialize(reader);

            if (config == null)
            {
                throw new InvalidDataException("Could not deserialize GameConfig");
            }
            return config;
        }
    }
}
