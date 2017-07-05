using Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoDLib;
using DoDLib.Character;
using DoDLib.Items;

namespace DungeonsOfDoom
{
    class Game
    {
        Player player; //deklarerar instans av klassen Player
        Room[,] world; //deklarerar 2D-array av klassen Room.
        //Random random = new Random(); kan tas bort då vi har Utils-klassen Spawner

        const int boardWidth = 30;
        const int boardHeight = 10;

        public static int monsterCount = 0; //statisk int som går på klassen Game hela tiden

        public void Play()
        {
            CreatePlayer();
            CreateWorld();

            TextUtils.Animate("Now entering the Dungeons of Doom");
            Console.ReadKey(true);

            //Spel-loopen:
            do
            {
                Console.Clear();                
                DisplayStats();
                DisplayWorld();               
                CheckRoom();
                AskForInput();
                // UseItems();
            } while (player.Health > 0 && monsterCount > 0);

            if (monsterCount == 0)
            {
                Win();
            }
            else
            {
                GameOver();
            }
        }

        private static void Win()
        {
            Console.Clear();
            TextUtils.Animate("You survived the Dungeons of Doom. You are a true hero!");
            Console.WriteLine();
            TextUtils.Animate("Made by: Johanna, Karam, Oliver");
        }

        private void CheckForEffects()
        {
            if (player.Bleed > 0)
            {
                player.Health--;
                player.Bleed--;
                Console.WriteLine("You bled 1 HP!");//todo fixa bleed effekt

            }
        }

        private void PrintBagContents()
        {
            Console.WriteLine("Bag:");
            Console.WriteLine("==============");
            for (int i = 0; i < player.Bag.Contents.Count; i++) //loopar igenom listan Contents i vår bag
            {
                Console.WriteLine($"{player.Bag.Contents.ElementAt(i).Name} \t Index: {i}");
            }
            Console.WriteLine();
            Console.WriteLine("Select item to use or close bag (C):");

            string input = Console.ReadLine(); 
            int result;
            if (Int32.TryParse(input, out result)) //om det går att översätta input till en integer (dvs. ej close bag) går vi in i if-sats:
                //TryParse returnerar en bool. Användarens input motsvarar itemets index som används(UseItem) och sen försvinner från bag(Remove):
            {
                IBringable item = player.Bag.Contents.ElementAt(result);

                if (item is Item)
                {
                    Item tempItem = item as Item;
                    player.UseItem(tempItem);
                    player.Bag.Contents.Remove(item);
                }                
            }
        }

        private void CheckRoom()
        {
            Room tempRoom = world[player.X, player.Y];

            if (tempRoom.Monster != null)
            {
                //Combat-loop:
                for (int i = 0; true; i++) //körs så länge den är true, dvs. går ur for-loopen när vi når en break;
                {
                    if (player.Health <= 0)
                    {
                        GameOver();
                        break;
                    }
                    if (tempRoom.Monster.Health <= 0)
                    {
                        Console.WriteLine($"The hero prevails, slaying the {tempRoom.Monster.Name} after a bloody fight!");
                        Console.WriteLine(player.PickUpSomething(tempRoom.Monster));
                        tempRoom.Monster = null;
                        monsterCount--;
                        break;
                    }

                    if (i % 2 == 0) //varannan (genom modulus) attackerar
                    {
                        Console.WriteLine(player.Attack(tempRoom.Monster));
                    } else
                    {
                        Console.WriteLine(tempRoom.Monster.Attack(player));
                    }
                }
            }
            else if (tempRoom.Item != null)
            {

                player.Bag.Contents.Add(world[player.X, player.Y].Item);
                world[player.X, player.Y].Item = null;
            }
        }

        void DisplayStats()
        {
            Console.WriteLine($"Health: {player.Health} \t Attack Dmg: {player.Damage}");
            //if (player.Bleed > 0)
            //{
            //    Console.WriteLine($"\t {player.Name} is currently Bleeding!");
            //}
        }

        private void AskForInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            int newX = player.X;
            int newY = player.Y;
            bool isValidMove = true;

            switch (keyInfo.Key)
            {
                case ConsoleKey.B: PrintBagContents(); isValidMove = false; break; //satt till falskt då spelaren ej förflyttas
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                default: isValidMove = false; break;
            }

            if (isValidMove &&
                // Check that move is within world boundaries
                newX >= 0 && newX < world.GetLength(0) &&
                newY >= 0 && newY < world.GetLength(1))
            {
                // If player moves, buffs should apply:
                CheckForEffects();
                //spelarens position ändras:
                player.X = newX;
                player.Y = newY;
            }
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < world.GetLength(1); y++) //varje rad är ett y-värde
            {
                for (int x = 0; x < world.GetLength(0); x++) //Varje kolumn är ett x-värde och loopar för varje cell i arrayen
                { //skriver ut alla symboler i respektive cell, som ligger i basklassen GameObject
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                        Console.Write(player.Symbol);
                    else if (room.Monster != null)
                        Console.Write(room.Monster.Symbol);
                    else if (room.Item != null)
                        Console.Write(room.Item.Symbol);
                    else
                        Console.Write(room.Symbol);
                }
                Console.WriteLine();
            }
        }

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over...");
            Console.ReadKey();
            //vill vi fråga om användaren vill spela igen?
            Play();
        }

        private void CreateWorld()
        {
            world = new Room[boardWidth, boardHeight];

            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                
                    // Instantiera ett room-objekt
                    world[x, y] = new Room();

                    // Ifall inte spelaren står i rutan, slumpa och se om vi placerar ett objekt i room
                    if (player.X != x || player.Y != y)
                    {
                        if (Spawner.SpawnPercentage(5)) //Vi bestämmer chansen för förekomst (10/100)         
                        {
                            world[x, y].Monster = new Ogre();
                            Game.monsterCount++;
                        }

                        if (Spawner.SpawnPercentage(1)) //Vi bestämmer chansen för förekomst (10/100)
                        {
                            world[x, y].Monster = new Dragon();
                            Game.monsterCount++;
                        }
                        if (Spawner.SpawnPercentage(1))  // 1/100 
                            world[x, y].Item = new Sword(20, "Harbringer Of Doom (Sword)", 5);

                        if (Spawner.SpawnPercentage(3))
                            world[x, y].Item = new Sword(12, "Steel Sword (Sword)", 2);

                        if (Spawner.SpawnPercentage(5))
                            world[x, y].Item = new Sword(2, "Iron Sword (Sword)", 1);

                        if (Spawner.SpawnPercentage(8))
                            world[x, y].Item = new HealthPotion(2, 2);

                        if (Spawner.SpawnPercentage(2))
                            world[x, y].Item = new StaminaPotion(2, 2);
                    }
                }
            }
        }

        private void CreatePlayer()
        {
            player = new Player(30, 0, 0);

        }
    }
}
