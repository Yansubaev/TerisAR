using UnityEngine;

namespace Tetris
{
    public class KeyInputHandler : IMoves
    {
        public Moves Move { get; set; }

        public void ClearMove()
        {
            Move = Moves.none;
        }

        public void OnLeftArrowDown()
        {
            Move = Moves.left;

            Debug.Log("LEFT MOVE");
        }

        public void OnRightArrowDown()
        {
            Move = Moves.right;

            Debug.Log("RIGHT MOVE");
        }

        public void ReadInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move = Moves.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move = Moves.right;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move = Moves.rotate;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move = Moves.down;
            }
        }
    }
}