using System;
using System.Threading;

namespace BrickGameEmulator
{
    public class BrickGame
    {
        private bool _isPause;

        private readonly BGLogger _logger;
        private readonly BGDataStorage _storage;
        private readonly BGSurface _surface;
        private readonly BGGames _games;

        private readonly Thread _gameThread;

        private Game _currentGame;
        
        private int _game;

        private bool _startNewGame = true;
        private bool _previewStopped;

        public BrickGame()
        {
            _logger = new BGLogger(50, 0);
            _storage = new BGDataStorage("game_data");
            _surface = new BGSurface(0, 0, _storage);
            _games = new BGGames();
          
            _gameThread = new Thread(Update);
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
            _gameThread.Start();
        }

        public void Update()
        {
            while (true)
            {
                if (_previewStopped)
                {
                    if (_startNewGame)
                    {
                        _currentGame = _games.GetGame(_game);
                     
                        _currentGame.SetSurface(_surface);
                        _currentGame.Create();
                        _surface.SetSplash(_currentGame.SplashScreen());
                        _surface.InitGame(_currentGame);
                        _startNewGame = false;
                    }
                }
                else
                {
                    _surface.SetSplash("preview.sph");
                }
                
                ConsoleKey key = ReadKey();
                
                if (key != ConsoleKey.NoName) _logger.Log("key", key.ToString());
                
                if (!_surface.SplashIsPlaying && !_currentGame.IsPause())
                {
                    _surface.Render(_currentGame.Run(key));
                }
                else
                {
                    if (key != ConsoleKey.NoName)
                    {
                        _logger.Log("debug", "StopSplash()");
                        _previewStopped = true;
                        _surface.StopSplash();
                    }
                    
                    _surface.Render(null);
                }

                if (key == ConsoleKey.P)
                {
                    if (_currentGame.IsPause()) _currentGame.Start();
                    else _currentGame.Pause();
                }
                
                if (key == ConsoleKey.PageUp) ChangeUpGame();
                if (key == ConsoleKey.PageDown) ChangeDownGame();
                if (key == ConsoleKey.Z)
                {
                    _currentGame.Destroy(_storage);
                    break;
                }
                        
            }
        }

        private void ChangeUpGame()
        {
            _currentGame.Destroy(_storage);
            _game++;
            if (_game == _games.GetCount()) _game = 0;
            _startNewGame = true;
        }
        
        private void ChangeDownGame()
        {
            _currentGame.Destroy(_storage);
            _game--;
            if (_game == -1) _game = _games.GetCount() - 1;
            _startNewGame = true;
        }
    }
}