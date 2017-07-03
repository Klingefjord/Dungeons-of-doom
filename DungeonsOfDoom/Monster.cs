using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Monster : Character
    {
        public string Name { get; }
        public Monster(int health, int damage, string name) : base(health, damage, 'M')
        {
            Name = name;
        }
    }
}
