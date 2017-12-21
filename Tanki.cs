using System;

namespace BrickGameEmulator
{
    public class Tanki : Game
    {
        private BGField bgField;

        private bool isPause = false;

        private int x = 5;
        private int y = 5;
        private int score = 0;

        private int testY = 0;

        public override void Create()
        {
            bgField = new BGField(); 
            bgField.SetValueAtPosition(x, y, 1);
        }

        public override BGField Run(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow) Up();
            if (key == ConsoleKey.DownArrow) Down();
            if (key == ConsoleKey.RightArrow) Right();
            if (key == ConsoleKey.LeftArrow) Left();
            if (key == ConsoleKey.Enter) SetScore(score++);
            if (key == ConsoleKey.B)
            {
                score = 0;
                SetScore(score);
            }
            Test();
            return bgField;
        }

        public void Test()
        {
            bgField.SetValueAtPosition(0, testY, 0);
            if (testY != 19)
            {
                testY++;
            }
            else
            {
                testY = 0;
            }
            bgField.SetValueAtPosition(0, testY, 1);
        }
        
        public override string SplashScreen()
        {
            return "tanki.sph";
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