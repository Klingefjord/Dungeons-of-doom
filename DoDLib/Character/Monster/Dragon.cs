using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoDLib.Character
{
    public class Dragon : Monster
    {
        public Dragon() : base(33, 8, "Dragon", 10)
        {
            this.Chasing = true;
        }

        public override char Symbol => 'D';
        
        /// <summary>
        /// Subtract health from opponent based on damage. 
        /// Also adds "Bleed" debuff
        /// </summary>
        /// <param name="opponent"></param>
        /// <returns></returns>
        public override string Attack(Character opponent)
        {
            opponent.Health -= this.Damage;
            opponent.Bleed = 10;

            return $"{this.Name} attacked {opponent.Name} with {this.Damage} points. " +
                   $"\t {this.Name}: {this.Health} \t {opponent.Name}: {opponent.Health}";
        }
    }
}
