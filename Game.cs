using System;

namespace BrickGameEmulator
{
    public interface Game
    {
        void Create(BGSurface surface);   
        
        void Run(ConsoleKey key);
        
        void SplashScreen();
        
        void Start();

        void Pause();
    }
}