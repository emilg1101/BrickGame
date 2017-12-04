using System;

namespace BrickGameEmulator
{
    public class CarRun : Game
    {
        private BGSurface surface;

        public void Create(BGSurface surface)
        {
            this.surface = surface;
        }

        public void Run(ConsoleKey key)
        {
            surface.Render(new BGField());
        }

        public void SplashScreen()
        {
            surface.SetSplash("carsrun.sph");
        }

        public void Start()
        {
            
        }

        public void Pause()
        {
            
        }
    }
}