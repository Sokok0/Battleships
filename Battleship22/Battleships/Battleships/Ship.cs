using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Battleships
{

    internal class Ship
    {
        public int[,] shipLocation = { };
        public bool dead = false;

        public void blockNearLocations(string[,] field)
        {
            List<(int, int)> locationsToBlock = new List<(int, int)>();

            for(int i=0; i<shipLocation.GetLength(0); i++)
            {
                int x = shipLocation[i,0];
                int y = shipLocation[i,1];
                AddIfValidLocation(x + 1, y, field, locationsToBlock);
                AddIfValidLocation(x - 1, y, field, locationsToBlock);
                AddIfValidLocation(x, y + 1, field, locationsToBlock);
                AddIfValidLocation(x, y - 1, field, locationsToBlock);
                AddIfValidLocation(x + 1, y - 1, field, locationsToBlock);
                AddIfValidLocation(x - 1, y + 1, field, locationsToBlock);
                AddIfValidLocation(x - 1, y - 1, field, locationsToBlock);
                AddIfValidLocation(x + 1, y + 1, field, locationsToBlock);
            }

            foreach ((int x, int y) in locationsToBlock)
            {
                field[x, y] = "#";
            }
        }

        private void AddIfValidLocation(int x, int y, string[,] field, List<(int, int)> locations)
        {
            if (x >= 0 && x < 10 && y >= 0 && y < 10 && field[x, y] != "O" && field[x, y] != "X")
            {
                locations.Add((x, y));
            }
        }
        public void Hit(int[] pos, Player playerWhoOwnShip, Player enemy)
        {
            bool deadLocal = true;
            playerWhoOwnShip.field[pos[0], pos[1]] = "X";
            for (int i = 0; i < shipLocation.GetLength(0); i++)
            {
                if (playerWhoOwnShip.field[shipLocation[i, 0], shipLocation[i, 1]] == "O") deadLocal = false;
            }

            if (deadLocal)
            {
                dead = true;
                Console.WriteLine("Brawo, zatopiłeś statek!");
                blockNearLocations(enemy.enemyField);

            }
        }
        public void placeShip1(Player player)
        {
            shipLocation = new int[1, 2];
            int[] playerInput = player.getPlayerInput();
            if (player.field[playerInput[0], playerInput[1]] == "O" || player.field[playerInput[0], playerInput[1]] == "#")
            {
                Console.WriteLine("Jest już tam statek");
                placeShip1(player);
                return;
            }
            player.field[playerInput[0], playerInput[1]] = "O";

            shipLocation[0, 0] = playerInput[0];
            shipLocation[0, 1] = playerInput[1];
        }

        public void placeShipLenght2(int lenght, Player player)
        {
            shipLocation = new int[lenght, 2];
            string[,] fieldBackup = new string[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    fieldBackup[i, j] = player.field[i, j];
                }
            }
            string direction;
            string directionSecond;
            int directionValue = 0;
            int[] input = player.getPlayerInput();


            if (player.field[input[0], input[1]] == "O" || player.field[input[0], input[1]] == "#")
            {
                placeShipLenght2(lenght, player);
                return;
            }
            player.field[input[0], input[1]] = "O";
            shipLocation[0, 0] = input[0];
            shipLocation[0, 1] = input[1];
            Console.WriteLine("W jakim kierunku chcesz postawić statek?");
            Console.WriteLine("1. Poziomo");
            Console.WriteLine("2. Pionowo");
            direction = Console.ReadLine();

            if (direction == "1")
            {
                Console.WriteLine("W którą stronę ma iść statek?");
                Console.WriteLine("1. Lewo");
                Console.WriteLine("2. Prawo");
                directionSecond = Console.ReadLine();
                if (directionSecond == "1")
                {
                    directionValue = -1;
                }
                else if (directionSecond == "2")
                {
                    directionValue = +1;
                }
                else
                {
                    player.field = fieldBackup;
                    Console.WriteLine("głuptasek - zła orientacja");
                    placeShipLenght2(lenght, player);
                    return;
                }
                if (directionValue == 1)
                {
                    if (!(input[1] + (lenght - 1) < 10))
                    {
                        player.field = fieldBackup;
                        placeShipLenght2(lenght, player);
                        return;
                    }
                    for (int i = 1; i < lenght; i++)
                    {
                        if (player.field[input[0], (input[1] + i)] == "O" || player.field[input[0], input[1] + i] == "#")
                        {
                            player.field = fieldBackup;
                            placeShipLenght2(lenght, player);

                            return;
                        }
                        else
                        {
                            shipLocation[i, 0] = input[0];
                            shipLocation[i, 1] = input[1] + i;
                            player.field[input[0], input[1] + i] = "O";

                        }

                    }
                    return;
                }
                if (!(input[1] - (lenght - 1) >= 0))
                {
                    player.field = fieldBackup;
                    placeShipLenght2(lenght, player);
                    return;
                }
                for (int i = 1; i < lenght; i++)
                {
                    if (player.field[input[0], (input[1] - i)] == "O" || player.field[input[0], input[1] - 1] == "#")
                    {
                        player.field = fieldBackup;
                        placeShipLenght2(lenght, player);

                        return;
                    }
                    else
                    {
                        shipLocation[i, 0] = input[0];
                        shipLocation[i, 1] = input[1] - i;
                        player.field[input[0], input[1] - i] = "O";
                    }

                }
            }

            else if (direction == "2")
            {

                Console.WriteLine("W którą stronę ma iść statek?");
                Console.WriteLine("1. Góra");
                Console.WriteLine("2. Dół");
                directionSecond = Console.ReadLine();
                if (directionSecond == "1")
                {
                    directionValue = +1;
                }
                else if (directionSecond == "2")
                {
                    directionValue = -1;
                }
                else
                {
                    player.field = fieldBackup;
                    Console.WriteLine("głuptasek - zła orientacja");
                    placeShipLenght2(lenght, player);
                    return;
                }
                if (directionValue == 1)
                {
                    if (!(input[0] - (lenght - 1) >= 0))
                    {
                        Console.WriteLine("No niestety, nie możesz postawić tak tego statku");
                        player.field = fieldBackup;
                        placeShipLenght2(lenght, player);
                        return; //jeszzce cos napisac
                    }
                    for (int i = 1; i < lenght; i++)
                    {
                        if (player.field[input[0] - i, (input[1])] == "O" || player.field[input[0] - i, input[1]] == "#")
                        {
                            player.field = fieldBackup;
                            placeShipLenght2(lenght, player);

                            return;
                        }
                        else
                        {
                            shipLocation[i, 0] = input[0] - i;
                            shipLocation[i, 1] = input[1];
                            player.field[input[0] - i, input[1]] = "O";
                        }

                    }
                    return;
                }
                if (!(input[0] + (lenght - 1) < 10))
                {
                    player.field = fieldBackup;
                    placeShipLenght2(lenght, player);
                    return;
                }
                for (int i = 1; i < lenght; i++)
                {
                    if (player.field[input[0] + i, (input[1])] == "O" || player.field[input[0] + i, input[1]] == "#")
                    {
                        player.field = fieldBackup;
                        placeShipLenght2(lenght, player);

                        return;
                    }
                    else
                    {
                        shipLocation[i, 0] = input[0] + i;
                        shipLocation[i, 1] = input[1];
                        player.field[input[0] + i, input[1]] = "O";
                    }

                }
                return;
            }
            else
            {
                player.field = fieldBackup;
                Console.WriteLine("głuptasek - zła orientacja");
                placeShipLenght2(lenght, player);
                return;
            }

        }




    }
}

