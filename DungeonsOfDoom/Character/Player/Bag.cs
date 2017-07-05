﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Bag
    {
        public int Size { get; set; }
        public List<IBringable> Contents { get; set; } = new List<IBringable>();

        public Bag(int size)
        {
            Size = size;
        }
    }
}
