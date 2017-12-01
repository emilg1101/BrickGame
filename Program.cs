using System;

namespace ConsoleTetrisTanki
{
    internal class Program
    {
        
        public static void Main(string[] args)
        {  
            BrickGame brickGame = new BrickGame();
            brickGame.Start();
            Console.ReadKey();
        }
    }
}