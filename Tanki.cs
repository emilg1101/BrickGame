using System;

namespace BrickGameEmulator
{
    public class Tanki : Game
    {
        private BGField bgField;
        private BGSurface surface;

        private bool isPause = false;

        private int x = 5;
        private int y = 5;

        public void Create(BGSurface surface)
        {
            this.surface = surface;
            bgField = new BGField(); 
            bgField.SetValueAtPosition(x, y, 1);
        }

        public void Run(ConsoleKey key)
        {
            if (isPause) return;
            if (key == ConsoleKey.UpArrow) Up();
            if (key == ConsoleKey.DownArrow) Down();
            if (key == ConsoleKey.RightArrow) Right();
            if (key == ConsoleKey.LeftArrow) Left();
            if (key == ConsoleKey.Enter) surface.Score++;
            if (key == ConsoleKey.B) surface.Score = 0;
            surface.Render(bgField);
        }
        
        public void SplashScreen()
        {
            surface.SetSplash("tanki.sph");
        }
        
        public void Start()
        {
            isPause = false;
        }

        public void Pause()
        {
            isPause = true;
        }

        public void Destroy(BGDataStorage storage)
        {
            storage.Commit();
        }

        public void Up()
        {
            bgField.SetValueAtPosition(x, y, 0);
            if (y != 0) y--;
            bgField.SetValueAtPosition(x, y, 1);
        }

        public void Down()
        {
            bgField.SetValueAtPosition(x, y, 0);
            if (y != bgField.GetHeight() - 1) y++;
            bgField.SetValueAtPosition(x, y, 1);
        }
        
        public void Right()
        {
            bgField.SetValueAtPosition(x, y, 0);
            if (x != bgField.GetWidth() - 1) x++;
            bgField.SetValueAtPosition(x, y, 1);
        }
        
        public void Left()
        {
            bgField.SetValueAtPosition(x, y, 0);
            if (x != 0) x--;
            bgField.SetValueAtPosition(x, y, 1);
        }
    }
}