﻿using Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoDLib;
using DoDLib.Character;
using DoDLib.Items;
using System.Threading;

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

            Console.BackgroundColor = ConsoleColor.Black;
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
                MoveMonsters(player);
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

        /// <summary>
        /// Loop through game world and update chasing monsters positions
        /// </summary>
        private void MoveMonsters(Player p)
        {
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    if (world[x,y].Monster != null)
                        if (world[x,y].Monster.Chasing)
                        {
                            int xHolder = x,
                                yHolder = y;



                            // Move towards player
                            if (p.X > x)
                                xHolder++;
                            else if (p.X < x)
                                xHolder--;
                            if (p.Y > y)
                                yHolder++;
                            else if (p.Y < y)
                                yHolder--;

                            world[xHolder, yHolder].Monster = world[x, y].Monster;
                            world[x, y].Monster = null;
                        }
                }
            }
        }

        private static void Win()
        {
            Console.Clear();
            TextUtils.Animate("You survived the Dungeons of Doom. You are a true hero!");
            Console.WriteLine();
            TextUtils.Animate("Made by: Johanna, Karam, Oliver");

            Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125);
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
                } else
                {

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

            // Det finns ett item i rummet
            else if (tempRoom.Item != null)
            {
                string tempString = player.PickUpSomething(world[player.X, player.Y].Item);
                Console.WriteLine(tempString);

                if (tempString != world[player.X, player.Y].Item.Name)
                    world[player.X, player.Y].Item = null;
            }
        }

        void DisplayStats()
        {
            Console.WriteLine($"Health: {player.Health} \t Attack Dmg: {player.Damage}");
            // Calling instance "player." If player moves, buffs should apply:
            Console.WriteLine(player.CheckForEffects());
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

                //spelarens position ändras:
                player.X = newX;
                player.Y = newY;

                // När spelaren rör på sig, applicera effekter
                player.ApplyEffects();
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
                    {
                        Console.Write(room.Monster.Symbol);
                    }
                    
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
            Console.BackgroundColor = ConsoleColor.DarkRed;
            TextUtils.Animate("Game over...");
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
