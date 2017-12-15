using System;
using System.Threading;

namespace BrickGameEmulator
{
    public class BrickGame
    {
        private bool isPause;

        private readonly BGLogger logger;
        private readonly BGSurface surface;
        private readonly BGDataStorage storage;

        private readonly Thread gameThread;

        private readonly Game[] games;

        private Game carGame;
        private Game tankiGame;
        
        private int game = 0;

        private bool startNewGame = true;
        private bool previewStopped = false;

        public BrickGame()
        {
            storage = new BGDataStorage("game_data");
            logger = new BGLogger(50, 0);
            surface = new BGSurface(0, 0, storage);
            gameThread = new Thread(Update);
            games = new[]
            {
                carGame,
                tankiGame
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
                        if (game == 0) games[game] = new CarRunGame();
                        if (game == 1) games[game] = new Tanki();
                     
                        games[game].SetSurface(surface);
                        games[game].Create();
                        surface.SetSplash(games[game].SplashScreen());
                        surface.InitGame(games[game]);
                        startNewGame = false;
                    }
                }
                else
                {
                    surface.SetSplash("preview.sph");
                }
                
                ConsoleKey key = ReadKey();
                if (key != ConsoleKey.NoName) logger.Log("key", key.ToString());
                if (!surface.SplashIsPlaying && !games[game].IsPause())
                {
                    surface.Render(games[game].Run(key));
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
                        games[game].Start();
                    }
                    else
                    {
                        isPause = true;
                        games[game].Pause();
                    }
                    
                }
                if (key == ConsoleKey.PageUp) ChangeUpGame();
                if (key == ConsoleKey.PageDown) ChangeDownGame();
                if (key == ConsoleKey.Z)
                {
                    games[game].Destroy(storage);
                    break;
                }
                        
            }
        }

        private void ChangeUpGame()
        {
            games[game].Destroy(storage);
            game++;
            if (game == games.Length) game = 0;
            startNewGame = true;
        }
        
        private void ChangeDownGame()
        {
            games[game].Destroy(storage);
            game--;
            if (game == -1) game = games.Length - 1;
            startNewGame = true;
        }
    }
}