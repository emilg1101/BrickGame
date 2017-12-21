using System;
using System.Collections.Generic;
using System.Threading;

namespace BrickGameEmulator
{
    public class CarRunGame : Game
    {
        private BGField field;

        private bool pause;
        private bool isGameOver;

        private int x = 2;
        private int y = 16;

        private int playerPosition = 1;

        private int count;
        private int time;
        private int speed;
        private int score;
        private int carsOut;

        private List<int[]> cars;
        
        int[] leftBorder = new int[20];
        int[] rightBorder = new int[20];

        private int[][] car = {
            new[] {0, 1, 0, 1},
            new[] {1, 1, 1, 0},
            new[] {0, 1, 0, 1}
        };

        private Random _random;        

        public override void Create()
        {
            field = new BGField();
            cars = new List<int[]>();
            _random = new Random();

            x = 2;
            speed = 25;
            score = 0;
            carsOut = 0;
            count = 0;
            playerPosition = 1;
            
            SetScore(score);
            SetSpeed(speed);
            
            PrepareBorder(ref leftBorder);
            PrepareBorder(ref rightBorder);
            DrawCar(x, y);
            ClearCar(x == 2 ? 5 : 2, y);
        }

        public override BGField Run(ConsoleKey key)
        {
            if (isGameOver)
            {
                if (key == ConsoleKey.RightArrow ||
                    key == ConsoleKey.LeftArrow ||
                    key == ConsoleKey.UpArrow ||
                    key == ConsoleKey.DownArrow ||
                    key == ConsoleKey.Spacebar)
                {
                    Create();
                    isGameOver = false;
                    return field;
                }
                
                DrawCar(playerPosition == 1 ? 2 : 5, y);
                return field;
            }
            
            if (key == ConsoleKey.RightArrow)
            {
                Right();
            }
            
            if (key == ConsoleKey.LeftArrow)
            {
                Left();
            }
            
            DrawCar(playerPosition == 1 ? 2 : 5, y);
            
            
            if (time % speed == 0) Draw();
            
            time++;

            return field;
        }

        public void Draw()
        {
            if (isGameOver) return;
            
            DrawCars();
            MoveBorders();
            
            if ((count / 4) % 2 == 0 && count % 4 == 1) cars.Add(new int[] {GetNext(1, 2), -4});
            
            count++;
        }

        public void DrawCars()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                int carX = cars[i][0];
                int carY = cars[i][1];

                if (carY > 20)
                {
                    cars.RemoveAt(i);
                    i--;
                    continue;
                }

                if ((carX == 1 ? 1 : 2) == playerPosition && carY >= 13 && carY <= 19)
                {
                    if (carY > 13) playerPosition = playerPosition == 1 ? 2 : 1;
                    
                    isGameOver = true;
                    carY--;
                    ClearCar(carX == 1 ? 2 : 5, carY - 1);
                    DrawCar(carX == 1 ? 2 : 5, carY);
                    break;
                    
                }
                
                ClearCar(carX == 1 ? 2 : 5, carY - 1);
                DrawCar(carX == 1 ? 2 : 5, carY);

                cars[i][1]++;
            }
        }

        public void AddScore()
        {
            score += 10;
            if (score % 100 == 0 && speed > 1)
            {
                speed--;
                SetSpeed(speed);
            }
            
            SetScore(score);
        }

        public void Right()
        {
            if (playerPosition == 1) AddScore();
            ClearCar(x, y);
            x = 5;
            playerPosition = 2;
            DrawCar(x, y);
        }

        public void Left()
        {
            if (playerPosition == 2) AddScore();
            ClearCar(x, y);
            x = 2;
            playerPosition = 1;
            DrawCar(x, y);
        }

        public void PrepareBorder(ref int[] border)
        {
            for (int i = 0; i < 20; i++)
            {
                border[i] = GetNext(0, 1);
            }
        }

        public void DrawBorders()
        {
            for (int i = 0; i < 20; i++)
            {
                field.SetValueAtPosition(0, i, leftBorder[i]);
                field.SetValueAtPosition(9, i, rightBorder[i]);
            }
        }

        public void MoveBorders()
        {
            for (int i = 18; i >= 0; i--)
            {
                leftBorder[i + 1] = leftBorder[i];
                rightBorder[i + 1] = rightBorder[i];
            }
            leftBorder[0] = GetNext(0, 1);
            rightBorder[0] = GetNext(0, 1);
            DrawBorders();
        }

        public void DrawCar(int x, int y)
        {
            if (y > 19) return;
            
            if (y == 17)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 17, car[i - x][0]);
                    field.SetValueAtPosition(i, 18, car[i - x][1]);
                    field.SetValueAtPosition(i, 19, car[i - x][2]);
                }
            }

            if (y == 18)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 18, car[i - x][0]);
                    field.SetValueAtPosition(i, 19, car[i - x][1]);
                }
            }

            if (y == 19)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 19, car[i - x][0]);
                }
            }
            
            if (y == -4) return;
            
            if (y == -3)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 0, car[i - x][3]);
                }
            }

            if (y == -2)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 1, car[i - x][3]);
                    field.SetValueAtPosition(i, 0, car[i - x][2]);
                }
            }

            if (y == -1)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 2, car[i - x][3]);
                    field.SetValueAtPosition(i, 1, car[i - x][2]);
                    field.SetValueAtPosition(i, 0, car[i - x][1]);
                }
            }

            if (y >= 0 && y <= 16)
            {
                for (int i = x; i < x + 3; i++)
                {
                    for (int j = y; j < y + 4; j++)
                    {
                        field.SetValueAtPosition(i, j, car[i - x][j - y]);
                    }
                }
            }
        }

        public void ClearCar(int x, int y)
        {
            if (y > 19) return;

            if (y == 17)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 16, 0);
                    field.SetValueAtPosition(i, 17, 0);
                    field.SetValueAtPosition(i, 18, 0);
                    field.SetValueAtPosition(i, 19, 0);
                }
            }

            if (y == 18)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 16, 0);
                    field.SetValueAtPosition(i, 17, 0);
                    field.SetValueAtPosition(i, 18, 0);
                    field.SetValueAtPosition(i, 19, 0);
                }
            }

            if (y == 19)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 16, 0);
                    field.SetValueAtPosition(i, 17, 0);
                    field.SetValueAtPosition(i, 18, 0);
                    field.SetValueAtPosition(i, 19, 0);
                }
            }
            
            if (y == -4) return;

            if (y == -3)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 0, 0);
                }
            }

            if (y == -2)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 0, 0);
                    field.SetValueAtPosition(i, 1, 0);
                }
            }

            if (y == -1)
            {
                for (int i = x; i < x + 3; i++)
                {
                    field.SetValueAtPosition(i, 0, 0);
                    field.SetValueAtPosition(i, 1, 0);
                    field.SetValueAtPosition(i, 2, 0);
                }
            }

            if (y >= 0 && y <= 16)
            {
                for (int i = x; i < x + 3; i++)
                {
                    for (int j = y; j < y + 4; j++)
                    {
                        field.SetValueAtPosition(i, j, 0);
                    }
                }
            }
        }

        public override string SplashScreen()
        {
            return "carsrun.sph";
        }

        private int GetNext(int min, int max)
        { 
            return _random.Next(min - 1, max + 1);
        }
    }
}