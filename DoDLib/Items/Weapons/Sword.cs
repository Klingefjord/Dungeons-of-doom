using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoDLib
{
    public class Sword : Weapon
    {
        public int Range { get; } = 1;

        public Sword(int dmg, string name, int weight) : base(dmg, name, weight)
        {
            
        }
    }
}
