using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Ogre : Monster
    {
        public Ogre() : base(30, 2, "Ogre", 5)
        {

        }

        public override char Symbol => 'O';
    }
}
