using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Sword : Weapon
    {
        public int Range { get; set; }

        public Sword(int range, double dmg, string name) : base(dmg, name)
        {
            Range = range;
        }
    }
}
