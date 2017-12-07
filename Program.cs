using System;
using System.Media;

namespace BrickGameEmulator
{
    internal class Program
    {
        public static void Main(string[] args)
        {            
            BrickGame brickGame = new BrickGame();
            brickGame.Start(); 
        }
    }
}