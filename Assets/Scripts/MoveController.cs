using UnityEngine;

namespace Tetris
{
    public enum Moves
    {
        none, left, right, down, rotate
    }

    public class MoveController
    {
        private readonly IMoves input;
        private readonly Transform transform;

        private Vector3 left = new Vector3(-1, 0, 0);
        private Vector3 right = new Vector3(1, 0, 0);
        private Vector3 down = new Vector3(0, -1, 0);
        private Vector3 up = new Vector3(0, 1, 0);

        private readonly bool allowRotation;
        private readonly bool limitRotation;

        private bool _canMove = true;

        private bool downMove = false;

        public bool CanMove {set => _canMove = value; }


        public MoveController(IMoves input, Transform transform, bool allowRotation, bool limitRotation)
        {
            this.input = input;
            this.transform = transform;
            this.allowRotation = allowRotation;
            this.limitRotation = limitRotation;
        }

        public void MoveMove()
        {
            if (_canMove)
            {
                switch (input.Move)
                {
                    case Moves.left:
                        LeftMove();
                        break;

                    case Moves.right:
                        RightMove();
                        break;

                    case Moves.down:
                        DownMove();
                        break;

                    case Moves.rotate:
                        Rotate();
                        break;
                }
            }

            input.ClearMove();
        }

        public void LeftMove()
        {
            transform.localPosition += left;

            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.localPosition += right;
            }
        }

        public void RightMove()
        {
            transform.localPosition += right;

            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.localPosition += left;
            }
        }

        public void Rotate()
        {
            if (allowRotation)
            {

                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }

                if (IsValidGridPosition())
                {
                    UpdateMatrixGrid();
                }
                else
                {
                    if (limitRotation)
                    {

                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }

            }
        }

        public void DownMove()
        {
            if (!downMove)
            {
                input.ClearMove();

                TetrisController.Instance.StartMoveDown();

                downMove = true;
            }
        }

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
    }
}