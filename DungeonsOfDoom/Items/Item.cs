using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Item : GameObject, IBringable
    {
        public Item(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public override char Symbol => 'I';

        public string Name { get; set; }
        public int Weight { get; set; }

        // Buffs har en default på 0
        public virtual int StaminaBuff { get; set; } = 0;
        public virtual int HealthBuff { get; set; } = 0;
        public virtual int DmgBuff { get; set; } = 0;
    }
}
