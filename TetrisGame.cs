using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace BrickGameEmulator
{
    public class TetrisGame : Game
    {
        private int[][] figureOne = {
            new[] {1, 1, 1, 1}
        };

        private int[][] figureTwo =
        {
            new[] {1, 1},
            new[] {1, 1}
        };

        private int[][] figureThree =
        {
            new[] {0, 1, 0},
            new[] {1, 1, 1}
        };
        
        private int[][] figureFour =
        {
            new[] {1, 1, 0},
            new[] {0, 1, 1}
        };
        
        private int[][] figureFive =
        {
            new[] {0, 1, 1},
            new[] {1, 1, 0}
        };
        
        private int[][] figureSix =
        {
            new[] {0, 1},
            new[] {1, 1},
            new[] {1, 0}
        };
        
        private int[][] figureSeven =
        {
            new[] {1, 0},
            new[] {1, 1},
            new[] {0, 1}
        };

        private int[][] currentFigure;

        private BGField field;

        private Random random;

        private int figureXPosition;
        private int figureYPosition;

        private bool figureIsFall = false;
        
        public override void Create()
        {
            field = new BGField();
            random = new Random();
            figureXPosition = GetNext(2, 7);
            figureYPosition = -1;
        }
        
        public override BGField Run(ConsoleKey key)
        {
            Thread.Sleep(150);
            
            if (!figureIsFall)
            {
                figureIsFall = true;
                currentFigure = GetRandomFigure();
                figureXPosition = GetNext(2, 7);
            }
            
           // if (figureYPosition >= 0) Clear(figureXPosition, figureYPosition, currentFigure);
            
            
            //Draw(figureXPosition, figureYPosition, currentFigure);

            if (figureIsFall && !IsIntersection(currentFigure))
            {
                if (figureYPosition >= 0) Clear(figureXPosition, figureYPosition, currentFigure);
                if (key == ConsoleKey.LeftArrow && figureXPosition > 0 && CanMoveLeft(currentFigure)) figureXPosition--;
                if (key == ConsoleKey.RightArrow && figureXPosition < 9 && CanMoveRight(currentFigure)) figureXPosition++;
                figureYPosition++;
                Draw(figureXPosition, figureYPosition, currentFigure);
            }
            else
            {
                figureIsFall = false;
                figureYPosition = -1;
            }
            
            return field;
        }

        public override string SplashScreen()
        {
            return "TetrisGame.sph";
        }

        public void Draw(int x, int y, int[][] figure)
        {
            for (int i = 0; i < figure.Length; i++)
            {
                for (int j = 0; j < figure[i].Length; j++)
                {
                    field.SetValueAtPosition(x + i, y + j, figure[i][j]);
                }
            }
        }
        
        public void Clear(int x, int y, int[][] figure)
        {
            for (int i = 0; i < figure.Length; i++)
            {
                for (int j = 0; j < figure[i].Length; j++)
                {
                    field.SetValueAtPosition(x + i, y + j, 0);
                }
            }
        }

        public bool IsIntersection(int[][] figure)
        {
            if (figureYPosition == -1) return false;
            for (int i = 0; i < figure.Length; i++)
            {
                if (figureYPosition + figure[0].Length - 1 == 19) return true;
                if (field.GetValueByPosition(figureXPosition + i, figureYPosition + figure[0].Length) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanMoveLeft(int[][] figure)
        {
            for (int i = figureYPosition; i < figureYPosition + figure[0].Length; i++)
            {
                if (i != -1 && field.GetValueByPosition(figureXPosition - 1, i) == 1) return false;
            }

            return true;
        }
        
        public bool CanMoveRight(int[][] figure)
        {
            for (int i = figureYPosition; i < figureYPosition + figure[0].Length; i++)
            {
                if (i != -1 && field.GetValueByPosition(figureXPosition + figure.Length, i) == 1) return false;
            }

            return true;
        }

        public int[][] GetRandomFigure()
        {
            int randomInt = GetNext(0, 6);
            if (randomInt == 0) return figureOne;
            if (randomInt == 1) return figureTwo;
            if (randomInt == 2) return figureThree;
            if (randomInt == 3) return figureFour;
            if (randomInt == 4) return figureFive;
            if (randomInt == 5) return figureSix;
            if (randomInt == 6) return figureSeven;
            return figureOne;
        }
        
        private int GetNext(int min, int max)
        { 
            return random.Next(min - 1, max + 1);
        }
    }
}