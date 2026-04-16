using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    /// <summary>
    /// Represents an object within the world with a name 
    /// and properties indicating whether it can be looted
    /// or removed.
    /// </summary>
    public class WorldObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the item can
        /// be looted.
        /// </summary>
        public bool Lootable { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the item can
        /// be removed.
        /// </summary>
        public bool Removeable { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the WorldObject class with
        /// default values.
        /// </summary>
        public WorldObject()
        {
            Name = string.Empty;
            Lootable = false;
            Removeable = false;
        }

        /// <summary>
        /// Initialises a new instance of the WorldObject class with
        /// name, lootability and removeability specified. Defaults
        /// to false for lootability and removeability if not provided.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="lootable">A boolean type which returns true or
        /// false depending on whether the item can be looted or not.
        /// Defaults to false if no specification is provided.</param>
        /// <param name="removeable">A boolean type which returns true
        /// or false depending on whether the item can be removed or not.
        /// Defaults to false if no specification is provided.</param>
        public WorldObject(string name, bool lootable = false, 
            bool removeable = false)
        {
            Name = name;
            Lootable = lootable;
            Removeable = removeable;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A string representation of the WorldObject, including its
        /// name and properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(Lootable)}={Lootable.ToString()}, " +
                $"{nameof(Removeable)}={Removeable.ToString()}}}";
        }
        #endregion
    }
}
