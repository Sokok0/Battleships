using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    internal class Player : Playfield
    {
        public int playerNumber;
        public int winNumber = 0;
        int aliveShips = 10;
        public Ship[] ships = new Ship[10];
        public Player(int playerNumber)
        {

            PrepareFields();
            this.playerNumber = playerNumber;
            for (int i = 0; i < 10; i++)
            {

                ships[i] = new Ship();
            }

        }
        public int[] getPlayerInput(bool attack = false)
        {

            string input;
            int x, y;
            if (attack) Console.WriteLine("Gdzie chcesz strzelić");
            else Console.WriteLine("Gdzie chcesz postawić swój statek:");

            Console.WriteLine("Podaj pole: \n");
            input = Console.ReadLine();


            try
            {
                x = Int32.Parse(input.Substring(1, input.Length - 1)) - 1;
            }
            catch (Exception error)
            {
                Console.WriteLine("Liczba nie jest na planszy");
                return getPlayerInput(attack);

            }
            y = input.ToLower()[0] - 'a';
            if(y < 0 || x < 0 || x > 9 || y > 9) 
            {
                Console.WriteLine("Liczba nie jest na planszy");
                return getPlayerInput(attack);
            }
            return new int[] { x, y };
        }
        public bool ShipHitAndReturnIfGameover(Player playerWhoIsShooting, int[] pos)
        {
            foreach (var ship in ships)
            {
                for (int i = 0; i < ship.shipLocation.GetLength(0); i++)
                {
                    if (ship.shipLocation[i, 0] == pos[0] && ship.shipLocation[i, 1] == pos[1])
                    {
                        ship.Hit(pos, this, playerWhoIsShooting);
                        if (ship.dead)
                            return ShipDownAndCheckIfGameover(playerWhoIsShooting); ;
                    }
                }
            }

            return false;
        }
        bool ShipDownAndCheckIfGameover(Player enemy)
        {
            aliveShips--;
            if (aliveShips == 0)
            {
                return true;
            }
            return false;
        }
        public void whatRound()
        {
            for (int round = 1; round <= 10; round++)
            {
                if (round % 2 == 0)
                {
                    Console.WriteLine("Runda gracza 2");
                    break;
                }
                else
                {
                    Console.WriteLine("Runda gracza 1");
                    break;
                }
            }
        }
        public bool Attack(Player enemy)
        {
            showEnemyField();
            showField();
            int[] input = getPlayerInput(true);

            if (field[input[0], input[1]] == "P" || field[input[0], input[1]] == "X")
            {
                Console.WriteLine("Nie możesz strzelić w to samo miejsce");
                return Attack(enemy);
            }

            if (enemy.field[input[0], input[1]] != "O")
            {
                Console.WriteLine("Pudło :box:");
                Console.ReadLine();
                field[input[0], input[1]] = "P";
                Console.Clear();
                Console.WriteLine("Runda kolejnego gracza");
                return false;
            }
            field[input[0], input[1]] = "X";
            Console.WriteLine("Trafiony :O");
            if (enemy.ShipHitAndReturnIfGameover(this, input))
            {
                return true;
            }
            return Attack(enemy);
        }
    }
}
