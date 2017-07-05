using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoDLib
{
    public abstract class Character : GameObject
    {    
        public virtual int Health { get; set; }
        public int Damage { get; set; }
        public int Stamina { get; set; }
        public string Name { get; }
        public int Bleed { get; set; } = 0;

        public Character(int health, int damage, char symbol, string name)
        {
            Health = health;
            Damage = damage;
            Name = name;
        }

        // Metoder

        public virtual string Attack(Character opponent)
        {
            opponent.Health -= this.Damage;
            return $"{this.Name} attacked {opponent.Name} with {this.Damage} points. " +
                   $"\t {this.Name}: {this.Health} \t {opponent.Name}: {opponent.Health}";
        }
    }
}
