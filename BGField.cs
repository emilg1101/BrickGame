namespace ConsoleTetrisTanki
{
    public class BGField
    {
        private int[][] gameField;

        public BGField(int[][] gameField)
        {
            this.gameField = gameField;
        }

        public BGField()
        {
            gameField = new int[BGConstants.gameFieldWidth][];
            for (int i = 0; i < gameField.Length; i++)
            {
                gameField[i] = new int[BGConstants.gameFieldHeight];
            }
        }

        public int GetWidth()
        {
            return gameField.Length;
        }

        public int GetHeight()
        {
            return gameField[0].Length;
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