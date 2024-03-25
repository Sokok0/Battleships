using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    internal class Program
    {
        public static void Gameover(Player playerWon, Player playerLoser)
        {

            Console.Clear();
            playerWon.winNumber++;
            Console.WriteLine("GRACZ NR " + playerWon.playerNumber + " WYGRAŁ!!!");
            Console.WriteLine("Gracz nr " + playerWon.playerNumber + " ma wygranych: " + playerWon.winNumber + " a gracz nr " + playerLoser.playerNumber + " ma wygranych: " + playerLoser.winNumber);
        }
        public static void Gameloop(Player player1, Player player2)
        {
            Console.Write("tura gracza 1: \n");
            bool gameover = false;
            player1.ChooseYourShip(player1);


            Console.Write("Koniec tury gracza 1: ");
            Console.ReadLine();
            Console.Clear();

            Console.Write("tura gracza 2: \n");

            player2.ChooseYourShip(player2);

            Console.Write("Koniec tury gracza 2: ");
            Console.ReadLine();
            Console.Clear();

            while (!gameover)
            {
                Console.Write("tura gracza 1: \n");
                gameover = player1.Attack(player2);
                if (gameover)
                {
                    Program.Gameover(player1, player2);
                    Program.AskIfStartAgain(player1, player2);
                }
                Console.ReadLine();
                Console.Clear();
                Console.Write("tura gracza 2: \n");
                player2.Attack(player1);
                if (gameover)
                {
                    Program.Gameover(player2, player1);
                    Program.AskIfStartAgain(player1, player2);
                }
                Console.ReadLine();
                Console.Clear();
            }


        }
        public static void AskIfStartAgain(Player player1, Player player2)
        {
            int winCount1 = player1.winNumber;
            int winCount2 = player2.winNumber;
            player1 = new Player(player1.playerNumber);
            player1.winNumber = winCount1;
            player2 = new Player(player2.playerNumber);
            player2.winNumber = winCount2;
            Console.WriteLine("Kontynuuj");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Czy chcesz zagrac ponownie? T - tak, F - nie");
            string input = Console.ReadLine();
            if (input.ToLower() == "t")
            {
                Gameloop(player1, player2);
                
            }
            else if (input.ToLower() == "f")
            {
                Console.WriteLine("To koniec zegnam.");
                
            }
            else
            {
                Console.WriteLine("Nie ma takiej odpowiedzi bambiku ^^");
                AskIfStartAgain(player1, player2);
                
            }
        }
        static void Main(string[] args)
        {

            Player player1 = new Player(1);
            Player player2 = new Player(2);

            Program.Gameloop(player1, player2);
        }
    }
}
