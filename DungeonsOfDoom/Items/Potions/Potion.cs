using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Potion : Item
    {
        public int Strength { get; set; }
        public Potion(string name, int weight, int strength) : base(name, weight)
        {
            Strength = strength;
        }
    }
}
