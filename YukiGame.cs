using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrickGameEmulator
{
    public class YukiGame : Game
    {
        private BGField field;
        private bool a = false;

        public override void Create()
        {
            field = new BGField();
        }

        public override string SplashScreen()
        {
            return "YukiGame.sph";
        }

        public override BGField Run(ConsoleKey key)
        {
            if (key == ConsoleKey.DownArrow) Draw(9, 19, 1);
            if (!a)
            {
                Draw(1, 1, 1);
                a = true;
            }
            else
            {
                Draw(1, 1, 0);
                a = false;
            }
        
            Thread.Sleep(100);
            return field;
        }

        public void Draw(int x, int y, int value)
        {
            field.SetValueAtPosition(x, y, value);
        }
    }
}
