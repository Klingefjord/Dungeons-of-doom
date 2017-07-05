using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoDLib
{
    /// <summary>
    /// This class represents a cell in the world!!!!
    /// </summary>
    public class Room : GameObject
    {
        public Monster Monster { get; set; }
        public Item Item { get; set; }
    }
}
