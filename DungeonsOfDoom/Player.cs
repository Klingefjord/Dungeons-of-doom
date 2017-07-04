using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Player : Character
    {
        public Player(int health, int x, int y) : base(health, 10, 'P', "You")
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Bag Bag { get; } = new Bag(20);
        public override char Symbol => 'P';

        // Metoder
        virtual public void UseItem(Item item)
        {
            this.Stamina += item.StaminaBuff;
            this.Health += item.HealthBuff;
            this.Damage += item.DmgBuff;
        }
    }
}
