﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Weapon : Item
    {
        public double DmgBuff { get; set; }

        public Weapon(double damage, string name, int weight) : base(name, weight)  
        {
            DmgBuff = damage;
        }
    }
}
