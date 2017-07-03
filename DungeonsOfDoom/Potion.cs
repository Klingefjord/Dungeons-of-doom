using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Potion : Item
    {
        public string Type { get; set; }
        public int Strength { get; set; }
        public Potion(string name, int weight, string type, int strength) : base(name, weight)
        {
            Type = type;
            Strength = strength;
        }

        // public bool IsConsumable { get; } = true;
    }
}
