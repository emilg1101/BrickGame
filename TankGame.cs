using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BrickGameEmulator;
using TankGameNamespace;

namespace TankGameNamespace
{
    class Arrow
    {
        private int left, top, dir;
        private bool type;
        public Arrow(int left_,int top_,int dir_,BGField field,bool type_ = false)
        {
            left = left_;
            top = top_;
            dir = dir_;
            type = type_;
            field.SetValueAtPosition(left, top, 3);
        }
        public bool update(BGField field)
        {
            if (field.GetValueByPosition(left, top) != 3)
                return true;
            field.SetValueAtPosition(left, top, 0);
            if (dir == 1)
                top--;
            if (dir == 2)
                top++;
            if (dir == 3)
                left--;
            if (dir == 4)
                left++;
            if (top == 20 || top == -1 || left == 10 || left == -1)
                return true;
            field.SetValueAtPosition(left, top, 3);
            return false;
        }
        public void delete(BGField field)
        {
            field.SetValueAtPosition(left, top, 0);
        }
        public int getLeft()
        {
            return left;
        }
        public int getTop()
        {
            return top;
        }
        public bool getType()
        {
            return type;
        }
    }
    class Tank
    {
        private int left, top, dir, olddir; // dir : 1 - up, 2 - down, 3 - left, 4 - right
        private bool type; // true is player, false is ai
        public Tank(int left_,int top_, int dir_ ,bool type_ = false)
        {
            left = left_;
            top = top_;
            dir = dir_;
            olddir = dir;
            type = type_;
        }
        public bool IsCorrupted(BGField field)
        {
            if (field.GetValueByPosition(left, top) == 3) return true;
            if (field.GetValueByPosition(left, top + 1) == 3) return true;
            if (field.GetValueByPosition(left, top - 1) == 3) return true;
            if (field.GetValueByPosition(left + 1, top) == 3) return true;
            if (field.GetValueByPosition(left + 1, top + 1) == 3) return true;
            if (field.GetValueByPosition(left + 1, top - 1) == 3) return true;
            if (field.GetValueByPosition(left - 1, top) == 3) return true;
            if (field.GetValueByPosition(left - 1, top + 1) == 3) return true;
            if (field.GetValueByPosition(left - 1, top - 1) == 3) return true;
            //for(int i = top - 1;i<=top+1;i++)
            //{
            //    for(int j = left - 1;j<=left+1;j++)
            //    {
            //        if (field.GetValueByPosition(left, top) == 3)
            //            return true;
            //    }
            //}
            return false;
        }
        private void Draw(BGField field, int dir_, int value)
        {
            field.SetValueAtPosition(left, top, value);
            field.SetValueAtPosition(left + 1, top, value);
            field.SetValueAtPosition(left - 1, top, value);
            field.SetValueAtPosition(left, top + 1, value);
            field.SetValueAtPosition(left, top - 1, value);
            switch (dir_)
            {
                case 1:
                    field.SetValueAtPosition(left - 1, top + 1, value);
                    field.SetValueAtPosition(left + 1, top + 1, value);
                    break;
                case 2:
                    field.SetValueAtPosition(left - 1, top - 1, value);
                    field.SetValueAtPosition(left + 1, top - 1, value);
                    break;
                case 3:
                    field.SetValueAtPosition(left + 1, top - 1, value);
                    field.SetValueAtPosition(left + 1, top + 1, value);
                    break;
                case 4:
                    field.SetValueAtPosition(left - 1, top - 1, value);
                    field.SetValueAtPosition(left - 1, top + 1, value);
                    break;
            }
        }
        private void Move(BGField field)
        {
            Draw(field, olddir, 0);
            if (dir == olddir)
            {
                if (dir == 1 && top - 1 != 0)
                {
                    if (top - 1 >= 1 && field.GetValueByPosition(left, top - 2) != 1 && field.GetValueByPosition(left + 1, top - 2) != 1 && field.GetValueByPosition(left - 1, top - 2) != 1)
                        top--;
                }
                if (dir == 2 && top + 1 != 19)
                {
                    if (top + 1 <= 18 && field.GetValueByPosition(left, top + 2) != 1 && field.GetValueByPosition(left + 1, top + 2) != 1 && field.GetValueByPosition(left - 1, top + 2) != 1)
                        top++;
                }
                if (dir == 3 && left - 1 != 0)
                {
                    if (left - 1 >= 1 && field.GetValueByPosition(left - 2, top) != 1 && field.GetValueByPosition(left - 2, top + 1) != 1 && field.GetValueByPosition(left - 2, top - 1) != 1)
                        left--;
                }
                if (dir == 4 && left + 1 != 9)
                {
                    if (left + 1 <= 18 && field.GetValueByPosition(left + 2, top) != 1 && field.GetValueByPosition(left + 2, top + 1) != 1 && field.GetValueByPosition(left + 2, top - 1) != 1)
                        left++;
                }
                Draw(field, dir, 1);
            }
            if (dir == 5)
            { 
                Draw(field, olddir, 1);
                return;
            }
            Draw(field, dir, 1);
        }
        private void Fire(BGField field,List<Arrow>Arrows)
        {
            int count = 0;
            for(int i = 0;i<Arrows.Count;i++)
            {
                if (Arrows[i].getType())
                    count++;
            }
            if (type && count == TankGame.Level)
                return;
            if (olddir == 1 && top - 2 != -1)
                Arrows.Add(new Arrow(left, top - 2, olddir, field, type));
            if (olddir == 2 && top + 2 != 20)
                Arrows.Add(new Arrow(left, top + 2, olddir, field, type));
            if (olddir == 3 && left - 2 != -1)
                Arrows.Add(new Arrow(left - 2, top, olddir, field, type));
            if (olddir == 4 && left + 2 != 10)
                Arrows.Add(new Arrow(left + 2, top, olddir, field, type));
        }
        public bool update(BGField field,ConsoleKey key,List<Arrow>Arrows)
        {
            if (IsCorrupted(field))
                return true;
            if (dir != olddir && dir != 5 && dir != 0)
                olddir = dir;
            if (ConsoleKey.NoName == key)
                dir = 0;
            if (ConsoleKey.UpArrow == key)
                dir = 1;
            if (ConsoleKey.DownArrow == key)
                dir = 2;
            if (ConsoleKey.LeftArrow == key)
                dir = 3;
            if (ConsoleKey.RightArrow == key)
                dir = 4;
            if (ConsoleKey.Spacebar == key)
                dir = 5;
            if (dir == 5)
                Fire(field, Arrows);
            if(dir != 0)
                Move(field);
            return false;
        }
        public void delete(BGField field)
        {
            field.SetValueAtPosition(left, top, 0);
            field.SetValueAtPosition(left, top + 1, 0);
            field.SetValueAtPosition(left, top - 1, 0);
            field.SetValueAtPosition(left + 1, top, 0);
            field.SetValueAtPosition(left + 1, top + 1, 0);
            field.SetValueAtPosition(left + 1, top - 1, 0);
            field.SetValueAtPosition(left - 1, top, 0);
            field.SetValueAtPosition(left - 1, top + 1, 0);
            field.SetValueAtPosition(left - 1, top - 1, 0);
        }
        public int getDx(int left_)
        {
            return (left - left_);
        }
        public int getDy(int top_)
        {
            return (top - top_);
        }
        public int getLeft()
        {
            return left;
        }
        public int getTop()
        {
            return top;
        }
        static public bool IsCreateble(BGField field, int left, int top)
        {
            for (int i = left; i < left + 5; i++)
            {
                for (int j = top; j < top + 5; j++)
                {
                    if (field.GetValueByPosition(i, j) != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
namespace BrickGameEmulator
{
    class TankGame : Game
    {
        public static int Level;
        private long AiLoop, AiLoopMax, AiCreate, AiCreateMax, ArrowLoop, ArrowLoopMax;
        private int Score;
        private BGField field;
        private Tank Player;
        private List<Arrow> Arrows;
        private List<Tank> Enemy;
        private System.Diagnostics.Stopwatch timer;
        private void updateArrows()
        {
            if (timer.ElapsedMilliseconds - ArrowLoop > ArrowLoopMax)
            {
                ArrowLoop = timer.ElapsedMilliseconds;
                for (int i = 0; i < Arrows.Count; i++)
                {
                    if (Arrows[i].update(field))
                    {
                        Arrows.RemoveAt(i);
                        continue;
                    }
                    for (int j = 0; j < Arrows.Count; j++)
                    {
                        if (i != j)
                        {
                            if (Arrows[i].getLeft() == Arrows[j].getLeft() && Arrows[i].getTop() == Arrows[j].getTop())
                            {
                                if (j > i)
                                {
                                    Arrows[j].delete(field);
                                    Arrows.RemoveAt(j);
                                    Arrows[i].delete(field);
                                    Arrows.RemoveAt(i);
                                }
                                else
                                {
                                    Arrows[i].delete(field);
                                    Arrows.RemoveAt(i);
                                    Arrows[j].delete(field);
                                    Arrows.RemoveAt(j);
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < Enemy.Count; i++)
                {
                    if (Enemy[i].IsCorrupted(field))
                    {
                        Enemy[i].delete(field);
                        Enemy.RemoveAt(i);
                        Score += 10;
                        SetScore(Score);
                        if(Score % 100 == 0)
                        {
                            Level++;
                            SetLevel(Level);
                        }
                    }
                }
            }
        }
        private void updateAi()
        {
            if (timer.ElapsedMilliseconds - AiLoop + Level * 100 > AiLoopMax)
            {
                AiLoop = timer.ElapsedMilliseconds;
                int dx, dy;
                for(int i = 0;i<Enemy.Count;i++)
                {
                    dx = Enemy[i].getDx(Player.getLeft());
                    dy = Enemy[i].getDy(Player.getTop());
                    if(dx <= 1 && dx >= -1)
                    {
                        if(dy > 0)
                        {
                            Enemy[i].update(field, ConsoleKey.UpArrow, Arrows);
                            Enemy[i].update(field, ConsoleKey.Spacebar, Arrows);
                        }
                        else
                        {
                            Enemy[i].update(field, ConsoleKey.DownArrow, Arrows);
                            Enemy[i].update(field, ConsoleKey.Spacebar, Arrows);
                        }
                        return;
                    }
                    if(dy <= 1 && dy >= -1)
                    {
                        if(dx > 0)
                        {
                            Enemy[i].update(field, ConsoleKey.LeftArrow, Arrows);
                            Enemy[i].update(field, ConsoleKey.Spacebar, Arrows);
                        }
                        else
                        {
                            Enemy[i].update(field, ConsoleKey.RightArrow, Arrows);
                            Enemy[i].update(field, ConsoleKey.Spacebar, Arrows);
                        }
                        return;
                    }
                    if(dx > dy)
                    {
                        if (dy > 0)
                            Enemy[i].update(field, ConsoleKey.UpArrow, Arrows);
                        else
                            Enemy[i].update(field, ConsoleKey.DownArrow, Arrows);
                        return;
                    }
                    if(dx < dy)
                    {
                        if(dx > 0)
                            Enemy[i].update(field, ConsoleKey.LeftArrow, Arrows);
                        else
                            Enemy[i].update(field, ConsoleKey.RightArrow, Arrows);
                        return;
                    }
                }
            }
        }
        private void AiCreator()
        {
            if(timer.ElapsedMilliseconds - AiCreate + Level * 500 > AiCreateMax)
            {
                AiCreate = timer.ElapsedMilliseconds;
                if (Tank.IsCreateble(field, 0, 0))
                {
                    Enemy.Add(new Tank(1, 1, 3, false));
                    return;
                }
                if (Tank.IsCreateble(field, 5, 15))
                {
                    Enemy.Add(new Tank(8, 18, 3, false));
                    return;
                }
            }
        }
        public override void Create()
        {
            field = new BGField();
            Player = new Tank(5, 5, 1, true);
            Arrows = new List<Arrow>();
            Enemy = new List<Tank>();
            Enemy.Add(new Tank(6, 18, 1));
            timer = new System.Diagnostics.Stopwatch();
            AiLoop = 0;
            AiLoopMax = 1000;
            AiCreate = 0;
            AiCreateMax = 10000;
            ArrowLoop = 0;
            ArrowLoopMax = 100;
            Level = 1;
            Score = 0;
            SetLevel(Level);
            SetScore(Score);
            timer.Start();
        }
        public override BGField Run(ConsoleKey key)
        {
            updateArrows();
            if (Player.update(field, key, Arrows))
                Create();           
            updateAi();
            AiCreator();
            return field;
        }
        public override string SplashScreen()
        {
            return "tanki.sph";
        }
        public override void Destroy(BGDataStorage storage)
        {
            base.Destroy(storage);
        }
    }
}