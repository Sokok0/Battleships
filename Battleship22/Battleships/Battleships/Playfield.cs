using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    internal class Playfield
    {
        public string[,] field = new string[10, 10];

        public string[,] enemyField = new string[10, 10];

        public void PrepareFields()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = " ";
                    enemyField[i, j] = " ";
                }
            }
        }
        public void showField()
        {
            Console.WriteLine("     | A | B | C | D | E | F | G | H | I | J ");
            Console.WriteLine("-----+---+---+---+---+---+---+---+---+---+---");
            int num = 1;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                if (num == 10)
                {
                    Console.Write(" " + num);
                }
                else
                {
                    Console.Write("  " + num);
                }

                for (int j = 0; j < field.GetLength(1); j++)
                {



                    Console.Write("  |" + field[i, j]);

                }
                num++;
                Console.WriteLine("\n-----+---+---+---+---+---+---+---+---+---+---");


            }
            Console.WriteLine("Kliknij aby kontynuować :3");
            Console.ReadLine();
        }

        public void showEnemyField()
        {
            Console.WriteLine("    | A | B | C | D | E | F | G | H | I | J ");
            Console.WriteLine("----+---+---+---+---+---+---+---+---+---+---");
            int num = 1;
            for (int i = 0; i < enemyField.GetLength(0); i++)
            {
                if (num == 10)
                {
                    Console.Write(num);
                }
                else
                {
                    Console.Write(" " + num);
                }

                for (int j = 0; j < enemyField.GetLength(1); j++)
                {



                    Console.Write("  |" + enemyField[i, j]);

                }
                num++;
                Console.WriteLine("\n----+---+---+---+---+---+---+---+---+---+---");


            }
        }

        public void ChooseYourShip(Player player)
        {
            int howMany1 = 4;
            int howMany2 = 3;
            int howMany3 = 2;
            int howMany4 = 1;
            for (int i = 0; i < 4; i++)
            {

                string choice;
                Console.WriteLine("Wybierz statek: ");
                Console.WriteLine(" +---------------------+\n " +
                    "| 1. Jednomasztowiec  |\n" +
                    " | 2. Dwumasztowiec    |\n" +
                    " | 3. Trzymasztowiec   |\n" +
                    " | 4. Czteromasztowiec |\n" +
                    " +---------------------+\n ");
                choice = Console.ReadLine();

                int TryPlacingShip(int index, int length, int howMany)
                {
                    
                    if (howMany == 0)
                    {
                        Console.WriteLine("Nie masz więcej takich statków!");
                        i--;
                        return 0;
                    }
                    else
                    {
                        if(length == 1)
                        {
                            player.ships[index - 1].placeShip1(player);
                        }
                        else
                        {
                            player.ships[index - 1].placeShipLenght2(length, player);
                        }
                        
                        player.ships[index - 1].blockNearLocations(player.field);
                        player.showField();
                        index--;
                       

                    }
                    return 1;
                }

                if (choice == "1")
                {
                    howMany1 -= TryPlacingShip(howMany1, 1, howMany1);
                    Console.Write($"Pozostałe statki jednomasztowe:  {howMany1}\n");
                }

                else if (choice == "2")
                {
                    howMany2 -= TryPlacingShip((howMany2+4), 2, howMany2);
                    Console.Write($"Pozostałe statki dwumasztowe:  {howMany2}\n");
                }

                else if (choice == "3")
                {
                    howMany3 -= TryPlacingShip((howMany3 + 4 + 3), 3, howMany3);
                    Console.Write($"Pozostałe statki trzymasztowe:  {howMany3}\n");

                }

                else if (choice == "4")
                {
                    howMany4 -= TryPlacingShip((howMany4 + 9), 4, howMany4);
                    Console.Write($"Pozostałe statki czterowasztowe:  {howMany4}\n");

                }
                else
                {
                    Console.WriteLine("głuptasek - złe cyfery");
                    i--;
                    
                }
            }

        }


    }
}
