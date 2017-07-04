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
    }
}
