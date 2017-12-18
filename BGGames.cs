namespace BrickGameEmulator
{
    public class BGGames
    {
        private readonly Game[] _games =
        {
            new CarRunGame(),
            new SampleGame(),
            new Tanki()
        };

        public int GetCount()
        {
            return _games.Length;
        }

        public Game GetGame(int position)
        {
            var game = _games[position];
            return game;
        }
    }
}