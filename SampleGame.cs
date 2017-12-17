using System;

namespace BrickGameEmulator
{
    public class SampleGame : Game
    {
        private BGField field;

        private int x = 0;
        private int y = 0;
        
        public override void Create()
        {
            field = new BGField();
        }

        public override BGField Run(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow) Up();
            if (key == ConsoleKey.DownArrow) Down();
            if (key == ConsoleKey.RightArrow) Right();
            if (key == ConsoleKey.LeftArrow) Left();
            return field;
        }
        
        private void Up()
        {
            field.SetValueAtPosition(x, y, 0);
            if (y != 0) y--;
            field.SetValueAtPosition(x, y, 1);
        }

        private void Down()
        {
            field.SetValueAtPosition(x, y, 0);
            if (y != field.GetHeight() - 1) y++;
            field.SetValueAtPosition(x, y, 1);
        }
        
        private void Right()
        {
            field.SetValueAtPosition(x, y, 0);
            if (x != field.GetWidth() - 1) x++;
            field.SetValueAtPosition(x, y, 1);
        }
        
        private void Left()
        {
            field.SetValueAtPosition(x, y, 0);
            if (x != 0) x--;
            field.SetValueAtPosition(x, y, 1);
        }

        public override string SplashScreen()
        {
            return "sample_game.sph";
        }
    }
}