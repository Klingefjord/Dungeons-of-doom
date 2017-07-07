using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoDLib.Items;

namespace DoDLib.Character
{
    // Player class
    public class Player : Character
    {
        public Player(int health, int x, int y) : base(health, 10, 'P', "You")
        {
            this.X = x;
            this.Y = y;
        }

        const int maxHealth = 50;

        int health;

        public override int Health {
            get { return health; }
            set
            {
                health = Math.Min(value, maxHealth);
            }
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Bag Bag { get; } = new Bag(40);
        public Weapon CurrentWeapon { get; set; }
        public override char Symbol => 'P';

        /// <summary>
        /// Method to use item, Adds a buff to player dependent of item property
        /// </summary>
        /// <param name="item"></param>
        virtual public void UseItem(Item item)
        {
            this.Stamina += item.StaminaBuff;
            this.Health += item.HealthBuff;

            //todo think about polymorphism 

            if (item is Weapon)
            {
                Weapon tempWeapon = item as Weapon;
                this.EquipWeapon(tempWeapon);
            }
        }
        /// <summary>
        /// Method that picks up stuff and adds in bag
        /// </summary>
        /// <param name="thing"></param>
        /// <returns></returns>
        public string PickUpSomething(IBringable thing)
        {
            if (CheckSize(thing))
            {
                this.Bag.Contents.Add(thing);
                return $"{this.Name} picked up {thing.Name}.";
            }
            else
            {
                return $"Bag is full! Can't pick up {thing.Name}";
            }
        }

        public bool CheckSize(IBringable thing)
        {
            int total = 0;
            this.Bag.Contents.ForEach((item) => total += item.Weight);

            if (total + thing.Weight < this.Bag.Size)
            {
                return true;
            }
            return false;
        }

        // todo pick up item metod


        /// <summary>
        /// Method to equip weapon, adds damage buff of new weapon to default damage, and removes earlier possible buffs
        /// </summary>
        /// <param name="weapon"></param>
        private void EquipWeapon(Weapon weapon)
        {
            if (CurrentWeapon != null)
            {
                this.Bag.Contents.Add(CurrentWeapon);
                this.Damage -= CurrentWeapon.DmgBuff;
            }
            CurrentWeapon = weapon;
            this.Damage += weapon.DmgBuff;
        }
        
        public void ApplyEffects()
        {
            if (this.Bleed > 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                this.Health--;
                this.Bleed--;
            }
            //todo fixa bleed effekt!!!!!
        }

        public string CheckForEffects()
        {
            if (this.Bleed > 0)
            {
                return "You bled 1 HP!";
            }
            Console.BackgroundColor = ConsoleColor.Black;
            return "Status Fine";
        }
    }
}
