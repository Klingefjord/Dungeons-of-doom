using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class HealthPotion : Potion
    {
 
        public HealthPotion(int weight, int strength) : base("Health Potion", weight, strength)
        {

        }
        public override int HealthBuff { get => this.Strength; set => this.Strength = value; }
    }
}
