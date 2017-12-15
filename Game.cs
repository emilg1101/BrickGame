using System;

namespace BrickGameEmulator
{
    public class Game : IGame
    {
        private BGSurface _surface;

        private bool _pause;

        public void SetSurface(BGSurface surface)
        {
            _surface = surface;
        }

        public void SetScore(int score)
        {
            _surface.Score = score;
        }

        public void SetLevel(int level)
        {
            _surface.Level = level;
        }

        public void SetSpeed(int speed)
        {
            _surface.Speed = 11 - speed;
        }

        public bool IsPause()
        {
            return _pause;
        }

        public virtual void Create()
        {
            
        }

        public virtual BGField Run(ConsoleKey key)
        {
            return new BGField();
        }

        public virtual string SplashScreen()
        {
            return "";
        }

        public virtual void Start()
        {
            _pause = false;
            _surface.Pause(_pause);
        }

        public virtual void Pause()
        {
            _pause = true;
            _surface.Pause(_pause);
        }

        public virtual void Destroy(BGDataStorage storage)
        {
            storage.Commit();
        }
    }
}