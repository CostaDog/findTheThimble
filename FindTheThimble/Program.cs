using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheThimble
{
    internal struct Position
    {
        public int x;
        public int y;

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;
            int width = 20;
            int height = 10;
            Position thimble = new Position();
            thimble.x = new Random().Next(0, width - 1);
            thimble.y = new Random().Next(0, height - 1);
            
            Position guess = new Position();
            List<Position> guesses = new List<Position>();
            do
            {
                Console.Clear();
                Console.WriteLine("Find The Thimble".ToUpper());
                Console.WriteLine("");
                for (int r = 0; r < height; r++)
                {
                    for (int c = 0; c < width; c++)
                    {
                        Position rc = new Position() { x = c, y = r };

                        if (alreadyInput(rc, guesses))
                        {
                            ConsoleColor colour = getColour(thimble, rc);
                            Console.ForegroundColor = colour;
                            Console.Write("X");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("-");
                        }
                    }
                    Console.WriteLine();
                }
                guess.x = getUserInput($"Enter your X-Coordinate (between 1 and {width})");
                guess.y = getUserInput($"Enter your Y-Coordinate (between 1 and {height})");
                if (guess.x == thimble.x && guess.y == thimble.y)
                {
                    playing = false;
                }
                else
                {
                    if (!alreadyInput(guess, guesses))
                    {
                        if ((guess.x >= 0 && guess.x <= width) && (guess.y >= 0 && guess.y <= height))
                        {
                            guesses.Add(guess);

                            string temp = HWorC(thimble, guess);
                            Console.WriteLine($"You are {temp}, keep trying.");
                        }
                        else
                        {
                            Console.WriteLine("Enter a valid coordinate");
                        }
                        Console.ReadKey();
                    }
                }
            } while (playing);
            Console.WriteLine("You found the thimble");
            Console.ReadKey();
        }
        private static int getUserInput(string message)
        {
            Console.WriteLine(message);
            int userinput;
            if (int.TryParse(Console.ReadLine(), out userinput))
            {
                return userinput - 1;
            }
            return 0;
        }
        private static bool alreadyInput(Position guess, List<Position> guesses)
        {
            return guesses.FindIndex(g => g.x == guess.x && g.y == guess.y) >= 0;
        }

        private static double Distance(Position thimble, Position guess)
        {
            int distanceY;
            int distanceX;
            if (thimble.y >= guess.y)
            {
                distanceY = thimble.y - guess.y;
            }
            else
            {
                distanceY = guess.y - thimble.y;
            }
            if (thimble.x >= guess.x)
            {
                distanceX = thimble.x - guess.x;
            }
            else
            {
                distanceX = guess.x - thimble.x;
            }
            double distance = Math.Sqrt((distanceY * distanceY) + (distanceX * distanceX));
            return distance;
        }
        private static ConsoleColor getColour(Position thimble, Position guess)
        {
            double d = Distance(thimble, guess);
            if (d <= 3)
            {
                return ConsoleColor.Red;
            }
            if (d > 3 && d <= 5)
            {
                return ConsoleColor.Yellow;
            }
            return ConsoleColor.Cyan;
        }
        private static string HWorC(Position thimble, Position guess)
        {
            double d = Distance(thimble, guess);
            if (d <= 3)
            {
                return "hot";
            }
            if (d > 3 && d <= 5)
            {
                return "warm";
            }
            return "cold";
        }

    }
}
