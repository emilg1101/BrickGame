using System;

namespace BrickGameEmulator
{
    public class CarRun : Game
    {
        private BGSurface surface;

        private BGField field;

        private bool pause;

        private int x = 2;
        private int y = 16;

        private int[][] car = {
            new[] {0, 1, 0, 1},
            new[] {1, 1, 1, 0},
            new[] {0, 1, 0, 1}
        };

        public void Create(BGSurface surface)
        {
            this.surface = surface;
            field = new BGField();
            DrawCar(x, y);
        }

        public void Run(ConsoleKey key)
        {
            if (pause) return;
            if (key == ConsoleKey.RightArrow) Right();
            if (key == ConsoleKey.LeftArrow) Left();
            surface.Render(field);
        }

        public void Right()
        {
            if (x + 1 < 7)
            {
                ClearCar(x, y);
                x++;
                DrawCar(x, y);
            }
        }

        public void Left()
        {
            if (x - 1 > 0)
            {
                ClearCar(x, y);
                x--;
                DrawCar(x, y);
            }
        }

        public void DrawCar(int x, int y)
        {
            for (int i = x; i < x + 3; i++)
            {
                for (int j = y; j < y + 4; j++)
                {
                    field.SetValueAtPosition(i, j, car[i - x][j - y]);
                }
            }
        }

        public void ClearCar(int x, int y)
        {
            for (int i = x; i < x + 3; i++)
            {
                for (int j = y; j < y + 4; j++)
                {
                    field.SetValueAtPosition(i, j, 0);
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
    }
}