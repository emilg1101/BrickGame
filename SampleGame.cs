using System;

namespace BrickGameEmulator
{
    public class SampleGame : Game
    {
        private BGSurface surface;

        private bool pause;
        
        public void Create(BGSurface surface)
        {
            this.surface = surface;
        }

        public void Run(ConsoleKey key)
        {
            if (pause) return;
            
            surface.Render(new BGField());
        }

        public void SplashScreen()
        {
            surface.SetSplash("sample_game.sph");
        }

        public void Start()
        {
            pause = false;
        }

        public void Pause()
        {
            pause = true;
        }

        public void Destroy(BGDataStorage storage)
        {
            storage.Commit();
        }
    }
}