using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Character : GameObject
    {    
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Stamina { get; set; }

        public Character(int health, int damage, char symbol)
        {
            Health = health;
            Damage = damage;
        }
    }
}
