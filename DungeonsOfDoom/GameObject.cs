﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class GameObject
    {
        virtual public char Symbol { get; } = '.';
    }
}
