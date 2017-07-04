using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    static class Spawner
    {
        static Random random = new Random();
        public static bool SpawnPercentage(int percentage)
        {
            return random.Next(0, 100) < percentage;
        }
    }
}
