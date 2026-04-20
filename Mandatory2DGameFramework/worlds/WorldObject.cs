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
        public bool Removable { get; set; }
        /// <summary>
        /// Gets or sets the X-coordinate position of the object 
        /// in the world.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the Y-coordinate position of the object 
        /// in the world.
        /// </summary>
        public int Y { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the WorldObject class
        /// with default values.
        /// </summary>
        public WorldObject()
        {
            Name = string.Empty;
            Lootable = true;
            Removable = true;
        }

        /// <summary>
        /// Initialises a new instance of the WorldObject class
        /// with name, lootability and removeability specified.
        /// defaults to false for lootability and removeability 
        /// if not provided.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="lootable">A boolean type which returns
        /// true or false depending on whether the item can be
        /// looted or not. Defaults to false if no specification 
        /// is provided.</param>
        /// <param name="removable">A boolean type which returns
        /// true or false depending on whether the item can be 
        /// removed or not. Defaults to false if no specification 
        /// is provided.</param>
        public WorldObject(string name, bool lootable = true, bool removable = true)
        {
            Name = name;
            Lootable = lootable;
            Removable = removable;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Moves the object to a new position in the world by 
        /// updating its X and Y coordinates.
        /// </summary>
        /// <param name="x">The new X-coordinate.</param>
        /// <param name="y">The new Y-coordinate.</param>
        public void MoveObject(int x, int y)
        {
            X = x;
            Y = y;
            // in an ideal world, there would be some validation here
            // to ensure the new coordinates are within the world bounds
            // and to ensure that there is no collision errors
        }

        /// <summary>
        /// A string representation of the WorldObject, including
        /// its name and properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, " +
                $"{nameof(Lootable)}={Lootable.ToString()}, " +
                $"{nameof(Removable)}={Removable.ToString()}}}";
        }
        #endregion
    }
}
