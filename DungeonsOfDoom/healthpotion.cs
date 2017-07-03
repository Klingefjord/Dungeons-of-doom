using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class HealthPotion : Potion
    {
 
        public HealthPotion(string name, int weight, int strength) : base(name, weight, "Health", strength)
        {

        }
    }
}
