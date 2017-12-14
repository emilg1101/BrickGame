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
        private Game sampleGame;
        
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
                tankiGame,
                sampleGame
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
                        if (game == 0) games[0] = new CarRun();
                        if (game == 1) games[1] = new Tanki();
                        if (game == 2) games[2] = new SampleGame();
                     
                        games[game].Create(surface);
                        games[game].SplashScreen();
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
                if (key == ConsoleKey.Z)
                {
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