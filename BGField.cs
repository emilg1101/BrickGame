namespace BrickGameEmulator
{
    public class BGField
    {
        private readonly int[][] _gameField;

        private readonly int _fieldWidth = BGConstants.FIELD_WIDTH;
        private readonly int _fieldHeight = BGConstants.FIELD_HEIGHT;

        public BGField(int[][] gameField)
        {
            _gameField = gameField;
        }

        public BGField()
        {
            _gameField = new int[_fieldWidth][];
            for (int i = 0; i < _fieldWidth; i++)
            {
                _gameField[i] = new int[_fieldHeight];
            }
        }

        public int GetWidth()
        {
            return _fieldWidth;
        }

        public int GetHeight()
        {
            return _fieldHeight;
        }

        public int GetValueByPosition(int x, int y)
        {
            return _gameField[x][y];
        }

        public void SetValueAtPosition(int x, int y, int value)
        {
            _gameField[x][y] = value;
        }
    }
}