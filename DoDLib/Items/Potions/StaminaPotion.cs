using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoDLib.Items
{
    public class StaminaPotion : Potion
    {
        public StaminaPotion(int weight, int strength) : base("Stamina Potion", weight, strength)
        {

        }
        public override int StaminaBuff { get => this.Strength; set => this.Strength = value; }
    }
}