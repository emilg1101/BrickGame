using System;

namespace ConsoleTetrisTanki
{
    public class Tanki : Game
    {
        private BGField bgField;
        private SplashReader splashReader;

        private int x = 0;
        private int y = 0;
        
        public Tanki()
        {
            bgField = new BGField(); 
            splashReader = new SplashReader();
        }

        public override void SplashScreen(BGSurface surface)
        {
            BGField[] frames = splashReader.Read("tanki.sph");
            surface.SetSplash(frames);
            surface.StartSplash();
        }

        public override void Start(BGSurface surface)
        {
            
        }

        public override void Run(BGSurface surface, ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow) Up();
            if (key == ConsoleKey.DownArrow) Down();
            if (key == ConsoleKey.RightArrow) Right();
            if (key == ConsoleKey.LeftArrow) Left();
            if (key == ConsoleKey.Enter) surface.Score++;
            if (key == ConsoleKey.B) surface.Score = 0;
            surface.Render(bgField);
        }

        public override void Pause(BGSurface surface)
        {
            
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