using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Dragon : Monster
    {
        public Dragon() : base(33, 8, "Dragon")
        {

        }

        public override char Symbol => 'D';

        public override string Attack(Character opponent)
        {
            opponent.Health -= this.Damage;
            opponent.Bleed = 10;

            return $"{this.Name} attacked {opponent.Name} with {this.Damage} points. " +
                   $"\t {this.Name}: {this.Health} \t {opponent.Name}: {opponent.Health}";
        }
    }
}
