using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace BrickGameEmulator
{
    public class CarRun : Game
    {
        private BGSurface surface;

        private BGField field;

        private bool pause;
        private bool isGameOver = false;

        private int x = 2;
        private int y = 16;

        private int playerPosition = 1;

        private int count;
        private int time;
        private int speed;
        private int carsOut;

        private List<int[]> cars;
        
        int[] leftBorder = new int[20];
        int[] rightBorder = new int[20];

        private int[][] car = {
            new[] {0, 1, 0, 1},
            new[] {1, 1, 1, 0},
            new[] {0, 1, 0, 1}
        };

        public void Create(BGSurface surface)
        {
            this.surface = surface;
            field = new BGField();
            cars = new List<int[]>();

            x = 2;
            speed = 10;
            carsOut = 0;
            playerPosition = 1;
            
            surface.SetSpeed(speed);
            surface.Score = 0;
            
            PrepareBorder(ref leftBorder);
            PrepareBorder(ref rightBorder);
            DrawCar(x, y);
            ClearCar(x == 2 ? 5 : 2, y);
        }

        public void Run(ConsoleKey key)
        {
            if (isGameOver)
            {
                if (key == ConsoleKey.RightArrow ||
                    key == ConsoleKey.LeftArrow ||
                    key == ConsoleKey.UpArrow ||
                    key == ConsoleKey.DownArrow ||
                    key == ConsoleKey.Spacebar)
                {
                    Create(surface);
                    isGameOver = false;
                    return;
                }
                else
                {
                    DrawCar(playerPosition == 1 ? 2 : 5, y);
                    surface.Render(field);
                    return;
                }
            }
            
            if (pause || isGameOver) return;
            
            if (key == ConsoleKey.RightArrow)
            {
                Right();
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                Left();
            }
            else
            {
                DrawCar(playerPosition == 1 ? 2 : 5, y);
            }
            
            if (time % speed == 0) Draw();
            
            surface.Render(field);
            time++;
        }

        public void Draw()
        {
            if (isGameOver) return;
            MoveBorders();
            DrawCars();
            
            if ((count / 4) % 2 == 0 && count % 4 == 1) cars.Add(new int[] {GetNext(1, 2), -4});
            
            count++;
        }

        public void DrawCars()
        {
            for (int i = carsOut; i < cars.Count; i++)
            {
                int carX = cars[i][0];
                int carY = cars[i][1];

                if ((carX == 1 ? 1 : 2) == playerPosition && carY >= 13 && carY <= 19)
                {
                    if (carY != 13) playerPosition = playerPosition == 1 ? 2 : 1;
                    
                    isGameOver = true;
                    //playerPosition = playerPosition == 1 ? 2 : 1;
                    //ClearCar(carX == 1 ? 2 : 5, carY - 1);
                    //DrawCar(carX == 1 ? 2 : 5, carY);
                    return;
                }
                
                ClearCar(carX == 1 ? 2 : 5, carY - 1);
                DrawCar(carX == 1 ? 2 : 5, carY);

                cars[i][1]++;
            }
        }

        public void AddScore()
        {
            surface.Score += 10;
            if (surface.Score % 100 == 0 && speed > 1)
            {
                speed--;
                surface.SetSpeed(speed);
            }
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
            if (y > 19)
            {
                //if (position > -1) cars.RemoveAt(position);
                return;
            }
            
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
            if (y > 19)
            {
                carsOut++;
                return;
            }

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

        public void SplashScreen()
        {
            surface.SetSplash("carsrun.sph");
        }

        public void Start()
        {
            pause = false;
        }

        public void Pause()
        {
            pause = true;
        }

        public void Destroy(BGDataStorage storage)
        {
            storage.Commit();
        }

        private int GetNext(int min, int max)
        { 
            return new Random().Next(min - 1, max + 1);
        }
    }
}