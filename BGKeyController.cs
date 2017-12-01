using System;

namespace ConsoleTetrisTanki
{
    
    public class BGKeyController
    {
        public enum HotKeys
        {
            Pause,
            Start,
            Empty
        }
        
        private bool pause = false;
        
        public HotKeys ReadKey()
        {
            //Console.WriteLine("key");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.P:
                    Console.WriteLine("p");
                    if (pause)
                    {
                        pause = false;
                        return HotKeys.Start;
                    }
                    pause = true;
                    return HotKeys.Pause;
            }

            return HotKeys.Empty;
        }
    }
}