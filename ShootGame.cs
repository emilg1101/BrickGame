using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ShootGameNamespace;
using BrickGameEmulator;

namespace ShootGameNamespace
{
    class Arrow
    {
        private int left, top;
        public Arrow(int left_, int top_)
        {
            left = left_;
            top = top_;
        }
        public bool update(BGField field)
        {
            if (top == 0)
            {
                field.SetValueAtPosition(left, top, 0);
                return true;
            }
            if(field.GetValueByPosition(left,top - 1)== 2)
            {
                field.SetValueAtPosition(left, top - 1, 0);
                field.SetValueAtPosition(left, top, 0);
                return true;
            }
            else
            {
                field.SetValueAtPosition(left, top, 0);
                top--;
                field.SetValueAtPosition(left, top, 1);
                return false;
            }

        }
    }
    class Player
    {
        private int left, top;
        private void Print(BGField field, int value)
        {
            if (left == 0)
            {
                field.SetValueAtPosition(left, top, value);
                field.SetValueAtPosition(left, top - 1, value);
                field.SetValueAtPosition(left + 1, top, value);
            }
            if (left == 9)
            {
                field.SetValueAtPosition(left, top, value);
                field.SetValueAtPosition(left, top - 1, value);
                field.SetValueAtPosition(left - 1, top, value);
            }
            if (left > 0 && left < 9)
            {
                field.SetValueAtPosition(left, top, value);
                field.SetValueAtPosition(left, top - 1, value);
                field.SetValueAtPosition(left + 1, top, value);
                field.SetValueAtPosition(left - 1, top, value);
            }
        }
        public void Move(int dir, BGField field, List<Arrow> Arrows)
        {
            Print(field, 0);
            if (dir == 1 && left!=0)
                 left--;
            if (dir == 2 && left !=9)
                 left++;
            Print(field, 1);

        }
        public bool update(ConsoleKey key, BGField field, List<Arrow> Arrows)
        {

            int direction = 0;

            if (key == ConsoleKey.LeftArrow)
                direction = 1;
            if (key == ConsoleKey.RightArrow)
                direction = 2;

            if (direction == 1 && field.GetValueByPosition(left, top - 1) == 2)
                return true;
            Move(direction, field, Arrows);
            return false;
        }
        public void Fire(List<Arrow>Arrows)
        {
            Arrows.Add(new Arrow(left, top - 2));
        }
        public Player(int left_)
        {
            left = left_;
            top = 19;
        }
    }
    class Wall
    {
        private List<int> buffer;
        Random rand;
        private void ReCreateBuf()
        {
            int val = rand.Next(10000, 99999);
            while (val > 0)
            {
                buffer.Add(val % 10);
                val /= 10;
            }
            val = rand.Next(10000, 99999);
            while (val > 0)
            {
                buffer.Add(val % 10);
                val /= 10;
            }
        }
        private void PrintWall(BGField field)
        {
            int left = buffer[buffer.Count - 1], top = 0;
            buffer.RemoveAt(buffer.Count - 1);
            while(field.GetValueByPosition(left,top) == 2)
            {
                top++;
            }
            field.SetValueAtPosition(left, top, 2);
        }
        public Wall()
        {
            rand = new Random();
            buffer = new List<int>();
            ReCreateBuf();
        }
        
        public void update(BGField field)
        {
            if(buffer.Count <= 0)
            {
                ReCreateBuf();
            }
            PrintWall(field);
        }
    }
}
namespace BrickGameEmulator
{
    
    class ShootGame : Game
    {
        private int Level;
        int PlCont = 0, WallPrint = 0, WallPrintMax, PlContMax, counter = 0;
        private Player player;
        private BGField field;
        private List<Arrow> Arrows;
        private Wall wall;
        private void updateArrows()
        {
            for(int i = 0;i < Arrows.Count;i++)
            {
                if (Arrows[i].update(field))
                {
                    Arrows.RemoveAt(i);
                }
            }
        }
        public override void Create()
        {
            field = new BGField();
            player = new Player(5);
            Arrows = new List<Arrow>();
            wall = new Wall();
            PlContMax = 1;
            WallPrintMax = 8;
            Level = 1;
        }
        public override BGField Run(ConsoleKey key)
        {
            updateArrows();
            if(key == ConsoleKey.Spacebar)
            {
                player.Fire(Arrows);
            }
            if (PlCont == PlContMax)
            {
                if (player.update(key, field, Arrows))
                {
                    Thread.Sleep(100);
                    Create();
                }

                PlCont = 0;
            }
            if (WallPrint == WallPrintMax - Level)
            {
                wall.update(field);
                WallPrint = 0;
                counter++;
                SetScore((Level - 1) * 100 + counter);
                if(counter==100)
                {
                    Level++;
                    SetLevel(Level);
                    if (Level >= WallPrintMax)
                        Level = WallPrintMax - 1;
                    counter = 0;
                }
            }
            PlCont++;
            WallPrint++;
            if (WallPrint > WallPrintMax)
                WallPrint = WallPrintMax;
            for(int i = 0;i<10;i++)
            {
                if(field.GetValueByPosition(i,19) == 2)
                {
                    Thread.Sleep(100);
                    Create();
                }
            }
            return field;
        }

        public override string SplashScreen()
        {
            return "ShootGame.sph";
        }
    }
}