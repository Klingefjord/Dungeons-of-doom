using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Game
    {
        Player player;
        Room[,] world;
        Random random = new Random();

        public void Play()
        {
            CreatePlayer();
            CreateWorld();

            do
            {
                Console.Clear();                
                DisplayStats();
                DisplayWorld();
                PrintBagContents();
                CheckRoom();
                AskForMovement();
                // UseItems();
            } while (player.Health > 0);

            GameOver();
        }

        private void PrintBagContents()
        {
            for (int i = 0; i < player.Bag.Contents.Count; i++)
            {
                Console.WriteLine(player.Bag.Contents.ElementAt(i).Name);
            }
        }

        private void CheckRoom()
        {
            var tempRoom = world[player.X, player.Y];
            if (tempRoom.Monster != null)
            {
                for (int i = 0; true; i++)
                {
                    if (player.Health <= 0)
                    {
                        GameOver();
                        break;
                    }
                    if (tempRoom.Monster.Health <= 0)
                    {
                        tempRoom.Monster = null;
                        break;
                    }

                    if (i % 2 == 0)
                    {
                        Console.WriteLine(player.Attack(tempRoom.Monster));
                    } else
                    {
                        Console.WriteLine(tempRoom.Monster.Attack(player));
                    }
                }
                //player.Health -= 5;
                //Console.WriteLine($"You just fought a {world[player.X, player.Y].Monster.Name}");
                //world[player.X, player.Y].Monster = null;
            }
            else if (world[player.X, player.Y].Item != null)
            {
                player.Bag.Contents.Add(world[player.X, player.Y].Item);
                world[player.X, player.Y].Item = null;
            }
        }

        void DisplayStats()
        {
            Console.WriteLine($"Health: {player.Health}");
        }

        private void AskForMovement()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            int newX = player.X;
            int newY = player.Y;
            bool isValidMove = true;

            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                case ConsoleKey.D: UsePotion(); break;
                case ConsoleKey.E: Equip(); break;
                default: isValidMove = false; break;
            }

            if (isValidMove &&
                // Check that move is within world boundaries
                newX >= 0 && newX < world.GetLength(0) &&
                newY >= 0 && newY < world.GetLength(1))
            {
                player.X = newX;
                player.Y = newY;
            }
        }

        private void UsePotion()
        {

            // ofType för att hitta första potion.
            foreach (Item i in player.Bag.Contents.OfType<Potion>())
            {
                player.UseItem(i);
                player.Bag.Contents.Remove(i);
                break;
            }
        }

        private void Equip()
        {

            // ofType för att hitta första potion.
            foreach (Item i in player.Bag.Contents.OfType<Potion>())
            {
                player.UseItem(i);
                player.Bag.Contents.Remove(i);
                break;
            }
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
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
            world = new Room[20, 5];
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                
                    // Instantiera ett room-objekt
                    world[x, y] = new Room();

                    // Ifall inte spelaren står i rutan, slumpa och se om vi placerar ett objekt i room
                    if (player.X != x || player.Y != y)
                    {
                        if (random.Next(0, 100) < 10) //Vi bestämmer chansen för förekomst (10/100)
                            world[x, y].Monster = new Ogre();

                        if (random.Next(0, 100) < 2) //Vi bestämmer chansen för förekomst (10/100)
                            world[x, y].Monster = new Dragon();

                        if (random.Next(0, 100) < 1)  // 1/100 
                            world[x, y].Item = new Sword(20, "Harbringer Of Doom (Sword)", 5);

                        if (random.Next(0, 100) < 4)
                            world[x, y].Item = new Sword(12, "Steel Sword (Sword)", 2);

                        if (random.Next(0, 100) < 5)
                            world[x, y].Item = new Sword(2, "Iron Sword (Sword)", 1);

                        if (random.Next(0, 100) < 3)
                            world[x, y].Item = new HealthPotion(2, 2);
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
