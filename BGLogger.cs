using System;

namespace BrickGameEmulator
{
    public class BGLogger
    {
        private readonly int _x;

        private readonly int _startY;
        private int _currentY;
        private int _line = 1;

        private const int Buffer = 24;

        public BGLogger(int x, int y)
        {
            _x = x;
            _startY = y;
            _currentY = y;
        }

        public void Log(string level, string message)
        {
            if (_startY + Buffer == _currentY + 1) _currentY = _startY;
            Console.SetCursorPosition(_x, _currentY);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                            ");
            Console.SetCursorPosition(_x, _currentY);
            Console.WriteLine(_line+":"+level + ": " + message);
            Console.ResetColor();
            _currentY++;
            _line++;
        }
    }
}