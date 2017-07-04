using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Player : Character
    {
        // todo add max health
        public Player(int health, int x, int y) : base(health, 10, 'P', "You")
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Bag Bag { get; } = new Bag(20);
        public Weapon CurrentWeapon { get; set; }
        public override char Symbol => 'P';

        // Metoder
        virtual public void UseItem(Item item)
        {
            this.Stamina += item.StaminaBuff;
            this.Health += item.HealthBuff;

            if (item is Weapon)
            {
                Weapon tempWeapon = item as Weapon;
                this.EquipWeapon(tempWeapon);
            }
        }

        private void EquipWeapon(Weapon weapon)
        {
            if (CurrentWeapon != null)
            {
                Weapon oldWeapon = CurrentWeapon;
                this.Bag.Contents.Add(oldWeapon);
                this.Damage -= CurrentWeapon.DmgBuff;
            }
            CurrentWeapon = weapon;
            this.Damage += weapon.DmgBuff;
        }
    }
}
