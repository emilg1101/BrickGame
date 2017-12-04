namespace BrickGameEmulator
{
    public class BGField
    {
        private int[][] gameField;

        private int fieldWidth = BGConstants.FIELD_WIDTH;
        private int fieldHeight = BGConstants.FIELD_HEIGHT;

        public BGField(int[][] gameField)
        {
            this.gameField = gameField;
        }

        public BGField()
        {
            gameField = new int[fieldWidth][];
            for (int i = 0; i < fieldWidth; i++)
            {
                gameField[i] = new int[fieldHeight];
            }
        }

        public int GetWidth()
        {
            return fieldWidth;
        }

        public int GetHeight()
        {
            return fieldHeight;
        }

        public int GetValueByPosition(int x, int y)
        {
            return gameField[x][y];
        }

        public void SetValueAtPosition(int x, int y, int value)
        {
            gameField[x][y] = value;
        }
    }
}