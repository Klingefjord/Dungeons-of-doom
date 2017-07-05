using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Utils
{
    static class TextUtils
    {
        public static void Animate(string value)
        {
            foreach (char c in value) //för varje bokstav i strängen pausar vi i 70millisek. och skriver ut bokstaven
            {
                Console.Write(c);
                Thread.Sleep(70);
            }
            Console.WriteLine();
        }
    }
}
