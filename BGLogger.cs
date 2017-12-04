using System;

namespace BrickGameEmulator
{
    public class BGLogger
    {
        private int x;
        private int y;

        private int startY;
        private int line = 1;

        private int buffer = 24;

        public BGLogger(int x, int y)
        {
            this.x = x;
            this.y = y;
            startY = y;
        }

        public void Log(String level, String message)
        {
            if (startY + buffer == y + 1) y = startY;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                            ");
            Console.SetCursorPosition(x, y);
            Console.WriteLine(line+":"+level + ": " + message);
            Console.ResetColor();
            y++;
            line++;
        }
    }
}