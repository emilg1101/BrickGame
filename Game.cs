using System;

namespace ConsoleTetrisTanki
{
    public abstract class Game
    {
        public abstract void SplashScreen(BGSurface surface);
        
        public abstract void Start(BGSurface surface);
        
        public abstract void Run(BGSurface surface, ConsoleKey key);

        public abstract void Pause(BGSurface surface);
    }
}