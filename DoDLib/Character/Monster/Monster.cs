using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoDLib.Character
{
    public class Monster : Character, IBringable
    {
        public override char Symbol => 'M';

        public int Weight { get; set; }
        public string Name { get; set; }

        public Monster(int health, int damage, string name, int weight) : base(health, damage, 'M', name)
        {
            Weight = weight;
            Name = name;
                    
        }
    }
}
