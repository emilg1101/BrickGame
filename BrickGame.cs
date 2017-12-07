using System;
using System.Threading;

namespace BrickGameEmulator
{
    public class BrickGame
    {
        private bool isPause;

        private readonly BGLogger logger;
        private readonly BGSurface surface;

        private readonly Thread gameThread;

        private readonly Game[] games;

        private int game = 0;

        private bool startNewGame = true;
        private bool previewStopped = false;

        public BrickGame()
        {
            logger = new BGLogger(50, 0);
            surface = new BGSurface(0, 0);
            gameThread = new Thread(new ThreadStart(Update));
            games = new Game[]
            {
                new CarRun(),
                new Tanki(),
                new SampleGame()
            };
        }
        
        public ConsoleKey ReadKey()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                while (Console.KeyAvailable) { Console.ReadKey(true); }
                
                return keyPressed.Key;
            }
            
            return ConsoleKey.NoName;
        }

        public void Start()
        {
            gameThread.Start();
        }

        public void Update()
        {
            while (true)
            {
                if (previewStopped)
                {
                    if (startNewGame)
                    {
                        games[game].Create(surface);
                        games[game].SplashScreen();
                        startNewGame = false;
                    }
                }
                else
                {
                    surface.SetSplash("preview.sph");
                }
                
                ConsoleKey key = ReadKey();
                if (key != ConsoleKey.NoName) logger.Log("key", key.ToString());
                if (!surface.SplashIsPlaying)
                {
                    games[game].Run(key);
                }
                else
                {
                    if (key != ConsoleKey.NoName)
                    {
                        logger.Log("debug", "StopSplash()");
                        previewStopped = true;
                        surface.StopSplash();
                    }
                    
                    surface.Render(null);
                }

                if (key == ConsoleKey.P)
                {
                    if (isPause)
                    {
                        isPause = false;
                        surface.Pause(isPause);
                        games[game].Start();
                    }
                    else
                    {
                        isPause = true;
                        surface.Pause(isPause);
                        games[game].Pause();
                    }
                    
                }
                if (key == ConsoleKey.PageUp) ChangeUpGame();
                if (key == ConsoleKey.PageDown) ChangeDownGame();
                if (key == ConsoleKey.S) games[game].SplashScreen();
                if (key == ConsoleKey.Z) break;
                        
            }
        }

        private void ChangeUpGame()
        {
            game++;
            if (game == games.Length) game = 0;
            startNewGame = true;
        }
        
        private void ChangeDownGame()
        {
            game--;
            if (game == -1) game = games.Length - 1;
            startNewGame = true;
        }
    }
}