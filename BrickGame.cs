using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleTetrisTanki
{
    public class BrickGame
    {
        private bool pause = false;

        private BGLogger _logger;
        private BGKeyController _bgKey;
        private BGSurface _bgSurface;

        private Thread gameThread;
        private Thread keyThread;

        private ConsoleKey key;
        
        Game[] games = new Game[] {new Tanki()};

        public BrickGame()
        {
            _logger = new BGLogger(50, 0);
            _bgSurface = new BGSurface(0, 0);
            gameThread = new Thread(new ThreadStart(Update));
        }

        public ConsoleKey ReadKey()
        {
           return Console.ReadKey(true).Key; 
        }

        public void Start()
        {
            gameThread.Start();
        }

        public void Update()
        {
            games[0].SplashScreen(_bgSurface);
            while (true)
            {
                ConsoleKey key = ReadKey();
                _logger.Log("key", key.ToString());
                if (!_bgSurface.SplashIsPlaying)
                {
                    games[0].Run(_bgSurface, key);
                }
                else
                {
                    //Console.WriteLine(key);
                    //if (key > 0) _bgSurface.StopSplash();
                    _bgSurface.Render(null);
                }
                
               
                //games[0].SplashScreen(_bgSurface);
                
                /*if (key > 0)
                {
                    _bgSurface.ShowSplash(null, false, 0);
                    games[0].Run(_bgSurface, key);
                }*/
                if (key == ConsoleKey.P) continue;
                if (key == ConsoleKey.Z) break;
                Thread.Sleep(25); // min 25mls 
               // _logger.Log("BG", "update()");

                //if (key == BGKeyController.HotKeys.Pause) SetPause();

            }
        }
    }
}