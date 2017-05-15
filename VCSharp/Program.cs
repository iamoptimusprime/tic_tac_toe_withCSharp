using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard g = new GameBoard();
            g.start();
        }
    }

    class GameBoard
    {
        private char[,] gameBoard;
        private bool gameOnGoing;

        public GameBoard()
        {
            gameBoard = new char[3, 3];
            gameOnGoing = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i, j] = ' ';
                }
            }
        }

        public void displayBoard()
        {
            Console.WriteLine("\n\n");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(gameBoard[i, j]);
                    if (j == 0 || j == 1)
                    {
                        Console.Write("   | ");
                    }
                }
                if (i == 0 || i == 1)
                {
                    Console.WriteLine("\n----------------");
                }
            }
            Console.WriteLine("\n\n");
        }

        public void setGameOnGoing(bool input)
        {
            gameOnGoing = input;
        }

        public bool gameActive()
        {
            return gameOnGoing;
        }

        public bool isEmpty(int row, int col)
        {
            if (gameBoard[row - 1, col - 1] == ' ')
            {
                return true;
            }
            else
            {
                Console.WriteLine("\nThis place is already taken.");
                return false;
            }
        }

        public bool notValid(int row, int col)
        {
            if (row > 3 || row < 1 || col > 3 || col < 1 || !isEmpty(row, col))
            {
                Console.WriteLine("\nInvalid move... Try again.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void makeMove(char player, int row, int col)
        {
            gameBoard[row - 1, col - 1] = player;
        }

        public void askPlayer(char player)
        {
            int row, col;
            do
            {
                Console.Write("\nPlayer {0}, enter a row (1-3): ", player);
                row = int.Parse(Console.ReadLine());
                Console.Write("\nPlayer {0}, enter a column (1-3: ", player);
                col = int.Parse(Console.ReadLine());
            } while (notValid(row, col));
            makeMove(player, row, col);
        }

        public void checkForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] == gameBoard[i, 1] && gameBoard[i, 0] == gameBoard[i, 2] && gameBoard[i, 0] != ' ' && gameActive())
                {
                    Console.WriteLine("The winner is player {0}!", gameBoard[i, 0]);
                    setGameOnGoing(false);
                }
                else if (gameBoard[0, i] == gameBoard[1, i] && gameBoard[0, i] == gameBoard[2, i] && gameBoard[0, i] != ' ' && gameActive())
                {
                    Console.WriteLine("The  winner is player {0}!", gameBoard[0, i]);
                    setGameOnGoing(false);
                }
            }
            if (gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[0, 0] == gameBoard[2, 2] && gameBoard[0, 0] != ' ' && gameActive())
            {
                Console.WriteLine("The winner is player {0}!", gameBoard[0, 0]);
                setGameOnGoing(false);
            }
            else if (gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[0, 2] == gameBoard[2, 0] && gameBoard[0, 2] != ' ' && gameActive())
            {
                Console.WriteLine("The winner is player {0}!", gameBoard[0, 2]);
                setGameOnGoing(false);
            }
        }

        public void emptyGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i, j] = ' ';
                }
            }
        }

        public void start()
        {
            displayBoard();
            Console.WriteLine("\nX makes the first move... Good luck!\n");
            int counter = 0;
            while (gameActive())
            {
                if (counter % 2 == 0)
                {
                    askPlayer('X');
                } else
                {
                    askPlayer('O');
                }
                displayBoard();
                checkForWinner();
                counter++;
                if (counter == 9)
                {
                    if (gameActive())
                    {
                        Console.WriteLine("\nThe match is a tie.");
                        setGameOnGoing(false);
                    }
                }
            }
            while (!gameActive())
            {
                string decision;
                Console.WriteLine("\nPress C to play again or Q to quit...");
                decision = Console.ReadLine();
                if (decision == "c" || decision == "C")
                {
                    emptyGrid();
                    setGameOnGoing(true);
                    start();
                } else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
