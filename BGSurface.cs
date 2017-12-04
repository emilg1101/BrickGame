using System;
using System.Threading;

namespace BrickGameEmulator
{
    public class BGSurface : BGConstants
    {
        private readonly int surfacePositionX;
        private readonly int surfacePositionY;

        private int highScore = GAME_HIGHSCORE;
        private int score = GAME_SCORE;
        private int level = GAME_LEVEL;
        private int speed = GAME_SPEED;

        public int Score
        {
            get => score;
            set => score = value;
        }

        public int Level
        {
            get => level;
            set => level = value;
        }

        public int Speed
        {
            get => speed;
            set => speed = value;
        }

        private int splashPosition;

        private BGField[] splashFrames;

        private bool splashIsPlaying = true;

        public bool SplashIsPlaying => splashIsPlaying;        

        public BGSurface(int surfacePositionX, int surfacePositionY)
        {
            this.surfacePositionX = surfacePositionX;
            this.surfacePositionY = surfacePositionY;
            
            Console.CursorVisible = false;
            
            _drawBorder(surfacePositionX, surfacePositionY);
            PrintMessageAtPosition(27, 1, "HI-SCORE", ConsoleColor.White);
            PrintMessageAtPosition(27, 4, "SCORE", ConsoleColor.White);
            PrintMessageAtPosition(27, 7, "LEVEL", ConsoleColor.White);
            PrintMessageAtPosition(27, 10, "SPEED", ConsoleColor.White);
            _renderStatusPanel();
        }

        
        public void Render(BGField bgField)
        {
            Console.BackgroundColor = ConsoleColor.White;
            if (splashIsPlaying || bgField == null)
            {
                _showSplash();
            }
            else
            {
                _render(bgField);
            }
            _renderStatusPanel();
        }

        private void _renderStatusPanel()
        {
            _updateHighScore();
            _updateScore();
            _updateLevel();
            _updateSpeed();
        }

        private void _updateHighScore()
        {
            if (score > highScore) highScore = score;
            PrintMessageAtPosition(27, 2, highScore.ToString(), ConsoleColor.White);
        }

        private void _updateScore()
        {
            PrintMessageAtPosition(27, 5, score.ToString(), ConsoleColor.White);
        }

        private void _updateLevel()
        {
            PrintMessageAtPosition(27, 8, level.ToString(), ConsoleColor.White);
        }

        private void _updateSpeed()
        {
            PrintMessageAtPosition(27, 11, speed.ToString(), ConsoleColor.White);
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
                        PrintAtPosition(surfacePositionX + 2 + (i * 2), j + 1, "■", ConsoleColor.White);
                        PrintAtPosition(surfacePositionX + 2 + (i * 2 + 1), j + 1, borderSymbol, ConsoleColor.White);
                    }
                    else
                    {
                        PrintAtPosition(surfacePositionX + 2 + (i * 2), j + 1, "■", ConsoleColor.Black);
                        PrintAtPosition(surfacePositionX + 2 + (i * 2 + 1), j + 1, borderSymbol, ConsoleColor.White);
                    }
                    
                }
            }
        }

        private void _drawBorder(int x, int y)
        {
            for (int i = x; i < x + FIELD_WIDTH * 2 + 4; i++)
            {
                for (int j = y; j < y + FIELD_HEIGHT + 2; j++)
                {
                    if (i == x + 1 && j == y)
                    {
                        PrintAtPosition(i, j, '\u2554', ConsoleColor.White);
                    }
                    else if ((i == x + 1 || i == x + FIELD_WIDTH * 2 + 2) && j != FIELD_HEIGHT + 1 && j != y)
                    {
                        PrintAtPosition(i, j, '\u2551', ConsoleColor.White);
                    }
                    else if (i == x + 1 && j == FIELD_HEIGHT + 1)
                    {
                        PrintAtPosition(i, j, '\u255A', ConsoleColor.White);
                    }
                    else if (i == x + FIELD_WIDTH * 2 + 2 && j == y)
                    {
                        PrintAtPosition(i, j, '\u2557', ConsoleColor.White);
                    }
                    else if (i == x + FIELD_WIDTH * 2 + 2 && j == FIELD_HEIGHT + 1)
                    {
                        PrintAtPosition(i, j, '\u255D', ConsoleColor.White);
                    }
                    else if (i != x && i != x + 1 && i != x + FIELD_WIDTH * 2 + 2 && i != x + FIELD_WIDTH * 2 + 3 && (j == y || j == FIELD_HEIGHT + 1))
                    {
                        PrintAtPosition(i, j, '\u2550', ConsoleColor.White);
                    }
                }
            }
        }

        public void SetSplash(string filename)
        {
            splashFrames = new SplashReader().Read(filename);
            StartSplash();
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
                StopSplash();
                return;
            }
            
            _render(splashFrames[splashPosition]);
            Thread.Sleep(10);
            splashPosition++;
        }

        public void Pause(bool pause)
        {
            if (pause)
            {
                PrintMessageAtPosition(27, 13, "PAUSE", ConsoleColor.Yellow);
            }
            else
            {
                PrintMessageAtPosition(27, 13, "     ", ConsoleColor.Yellow);
            }
        }

        public void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Console.Write(symbol);
            Console.BackgroundColor = ConsoleColor.White;
        }
        
        public void PrintAtPosition(int x, int y, string symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public void PrintMessageAtPosition(int x, int y, string text, ConsoleColor color)
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