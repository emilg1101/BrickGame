using System;

namespace BrickGameEmulator
{
    public interface IGame
    {   
        void Create();   
        
        BGField Run(ConsoleKey key);
        
        string SplashScreen();
        
        void Start();

        void Pause();

        void Destroy(BGDataStorage storage);
    }
}