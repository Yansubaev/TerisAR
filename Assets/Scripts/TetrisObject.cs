using UnityEngine;

namespace Tetris
{
    public class TetrisObject : MonoBehaviour
    {

        #region TerisObject fields

        float lastFall = 0f;

        private float _timeSpeed = 1.0f;

        public bool allowRotation = true;
        public bool limitRotation = false;

        private readonly Vector3 left = new Vector3(-1, 0, 0);
        private readonly Vector3 right = new Vector3(1, 0, 0);
        private readonly Vector3 down = new Vector3(0, -1, 0);
        private readonly Vector3 up = new Vector3(0, 1, 0);

        public float TimeSpeed { set => _timeSpeed = value; }

        #endregion TetrisObject fields


        private void Start()
        {
            _timeSpeed = TetrisController.Instance.Speed;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) || Time.time - lastFall >= _timeSpeed)
            {
                transform.localPosition += down;

                if (IsValidGridPosition())
                {
                    UpdateMatrixGrid();
                }
                else
                {
                    transform.localPosition += up;

                    MatrixGrid.DeleteWholeRows();

                    if (!CheckIsAboveGrid())
                    {
                        TetrisController.Instance.Spawn();
                    }
                    else
                    {
                        TetrisController.Instance.WhenGameOver();
                    }
                    enabled = false;
                }
                lastFall = Time.time;
            }
        }

        #region Matrix

        public bool IsValidGridPosition()
        {
            foreach (Transform child in transform)
            {
                Vector2 v = MatrixGrid.RoundVector(transform.parent.InverseTransformPoint(child.position));

                if (!MatrixGrid.IsInsideBorder(v))
                    return false;

                if (MatrixGrid.grid[(int)v.x, (int)v.y] != null && MatrixGrid.grid[(int)v.x, (int)v.y].parent != transform)
                    return false;

            }
            return true;
        }

        public void UpdateMatrixGrid()
        {
            for (int y = 0; y < MatrixGrid.column; ++y)
            {
                for (int x = 0; x < MatrixGrid.row; ++x)
                {
                    if (MatrixGrid.grid[x, y] != null)
                    {
                        if (MatrixGrid.grid[x, y].parent == transform)
                        {
                            MatrixGrid.grid[x, y] = null;
                        }
                    }
                }
            }

            foreach (Transform child in transform)
            {
                Vector2 v = MatrixGrid.RoundVector(transform.parent.InverseTransformPoint(child.position));

                MatrixGrid.grid[(int)v.x, (int)v.y] = child;
            }

        }

        public bool CheckIsAboveGrid()
        {
            for (int x = 0; x < MatrixGrid.row; ++x)
            {
                foreach (Transform child in transform)
                {
                    Vector2 pos = MatrixGrid.RoundVector(transform.parent.InverseTransformPoint(child.position));

                    if (pos.y > 19)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion Matrix
    }
}