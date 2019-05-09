using UnityEngine;

namespace Tetris
{
    public class MatrixGrid
    {

        #region MatrixGrid fields

        public static int row = 10;
        public static int column = 24;

        public static Transform[,] grid = new Transform[row, column];

        private static int _scores = 0;

        private static int scoreOneLine = 100;
        private static int scoreTwoLine = 300;
        private static int scoreThreeLine = 700;
        private static int scoreForeLine = 1500;

        private static int numberOfLines = 0;

        private static float rightBound = 10f;
        private static float leftBound = 0f;
        private static float bottomBound = 0f;

        public static float scaleFactor = 1f;

        public static int Scores {set => _scores = value; }


        #endregion MatrixGrid fields

        public static Vector2 RoundVector(Vector2 v)
        {
            return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
        }

        public static bool IsInsideBorder(Vector2 pos)
        {
            return ((int)pos.x >= 0 && (int)pos.x < row && (int)pos.y >= 0);
        }

        public static void DeleteRow(int y)
        {
            for (int x = 0; x < row; ++x)
            {
                if (grid[x, y] != null)
                {
                    Object.Destroy(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
            }
        }

        public static void DecreaseRow(int y)
        {
            for (int x = 0; x < row; ++x)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;

                    grid[x, y - 1].position += new Vector3(0, -1 * scaleFactor, 0);
                }
            }
        }

        public static void DecreaseRowsAbove(int y)
        {
            for (int i = y; i < column; ++i)
            {
                DecreaseRow(i);
            }
        }

        public static bool IsRowFull(int y)
        {
            for (int x = 0; x < row; ++x)
            {
                if (grid[x, y] == null)
                    return false;
            }

            numberOfLines++;

            return true;
        }

        public static void DeleteWholeRows()
        {
            for (int y = 0; y < column; ++y)
            {
                if (IsRowFull(y))
                {
                    DeleteRow(y);
                    DecreaseRowsAbove(y + 1);
                    --y;
                }
            }
        }

        public static void UpdateScores()
        {
            if (numberOfLines > 0)
            {
                if (numberOfLines == 1)
                {
                    _scores += scoreOneLine;
                }
                else if (numberOfLines == 2)
                {
                    _scores += scoreTwoLine;
                }
                else if (numberOfLines == 3)
                {
                    _scores += scoreThreeLine;
                }
                else if (numberOfLines == 4)
                {
                    _scores += scoreForeLine;
                }

                TetrisController.Instance.Score = _scores;

                numberOfLines = 0;
            }
        }

        public static void ClearWholeMatrix()
        {
            for (int y = 0; y < column; ++y)
            {
                DeleteRow(y);
            }
        }
    }
}