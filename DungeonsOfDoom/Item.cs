using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Item : GameObject
    {
        public Item(string name, int weight) : base('I')
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; }
        public int Weight { get; }
    }
}
