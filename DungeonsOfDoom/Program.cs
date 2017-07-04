using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(); //öppnar en instans av klassen game och kör Play()
            game.Play();
        }
    }
}
