using System;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleTetrisTanki
{
    public class BGSurface : BGConstants
    {
        private int x;
        private int y;

        private int highScore = 0;
        private int score = 0;

        public int Score
        {
            get => score;
            set => score = value;
        }

        private int speed = 1;
        private int splashPosition = 0;

        private BGField[] splashFrames = null;

        private bool splashIsPlaying = true;

        public bool SplashIsPlaying => splashIsPlaying;        

        public BGSurface(int x, int y)
        {
            this.x = x;
            this.y = y;
            Console.CursorVisible = false;
            DrawBorder(x, y);
            _render(new BGField());
            PrintMessageAtPosition(27, 1, "HI-SCORE", ConsoleColor.White);
            _updateHighScore();
            PrintMessageAtPosition(27, 4, "SCORE", ConsoleColor.White);
            _updateScore();
        }

        public void Render(BGField bgField)
        {
            _clear();
            if (splashIsPlaying)
            {
                _showSplash();
            }
            else
            {
                _render(bgField);
            }
            _updateHighScore();
            _updateScore();
        }

        private void _updateHighScore()//89656293994
        {
            if (score > highScore) highScore = score;
            PrintMessageAtPosition(27, 2, highScore.ToString(), ConsoleColor.White);
        }

        private void _updateScore()
        {
            PrintMessageAtPosition(27, 5, score.ToString(), ConsoleColor.White);
        }

        private void _render(BGField bgField)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    int value = bgField.GetValueByPosition(i, j);
                    if (value == 0)
                    {
                        PrintAtPosition(x + 2 + (i * 2 + 1), j + 1, borderSymbol, ConsoleColor.White);
                        PrintAtPosition(x + 2 + (i * 2), j + 1, borderSymbol, ConsoleColor.White);
                    }
                    else
                    {
                        PrintAtPosition(x + 2 + (i * 2 + 1), j + 1, borderSymbol, ConsoleColor.Black);
                        PrintAtPosition(x + 2 + (i * 2), j + 1, borderSymbol, ConsoleColor.Black);
                    }
                    
                }
            }
        }

        private void DrawBorder(int x, int y)
        {
            for (int i = x; i < x + gameFieldWidth * 2 + 4; i++)
            {
                for (int j = y; j < y + gameFieldHeight + 2; j++)
                {
                    if (i == x || i == x + 1 || i == x + gameFieldWidth * 2 + 3 
                        || i == x + gameFieldWidth * 2 + 2
                        || j == y || j == y + gameFieldHeight + 1)
                    {
                        PrintAtPosition(i, j, borderSymbol, ConsoleColor.Blue);
                    }
                }
            }
        }

        public void SetSplash(BGField[] frames)
        {
            splashFrames = frames;
        }

        public void StartSplash()
        {
            splashIsPlaying = true;
            splashPosition = 0;
        }

        public void StopSplash()
        {
            splashIsPlaying = false;
            splashPosition = 0;
        }

        private void _showSplash()
        {
            if (splashFrames == null || splashPosition >= splashFrames.Length || !splashIsPlaying)
            {
                splashIsPlaying = false;
                splashPosition = 0;
                return;
            }
            
            _render(splashFrames[splashPosition]);
            Thread.Sleep(200);
            splashPosition++;
        }

        private void _clear()
        {
            _render(new BGField());
            Console.BackgroundColor = ConsoleColor.White;
        }
        
        private void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Console.Write(symbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        private void PrintMessageAtPosition(int x, int y, string text, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ResetColor();
            Console.ForegroundColor = color;
            Console.Write("            ");
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }
    }
}