using UnityEngine;
using UnityEngine.EventSystems;

namespace Tetris
{
    public class TouchInputHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IMoves
    {
        [SerializeField] private float delayTime = 0.2f;
        private float lastSwipe = 0f;

        private float multiplier = 0f;
        private float tapPoint = 0;

        private float downTapTime = 0f;
        private float upTapTime = 0f;

        public Moves Move { get; set; }

        private void Awake()
        {
            multiplier = Screen.width / 15;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y))
            {
                if (eventData.delta.y < 0)
                {
                    Move = Moves.down;
                }
            }
            lastSwipe = Time.time;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if ((eventData.position.x - tapPoint) > multiplier)
            {
                Move = Moves.right;

                tapPoint = eventData.position.x;
            }
            else if ((eventData.position.x - tapPoint) < -multiplier)
            {
                Move = Moves.left;

                tapPoint = eventData.position.x;
            }
            lastSwipe = Time.time;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if ((Time.time - lastSwipe) > delayTime && (upTapTime - downTapTime) < delayTime)
            {
                Move = Moves.rotate;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            tapPoint = eventData.position.x;

            downTapTime = Time.time;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            tapPoint = 0;

            upTapTime = Time.time;
        }

        public void ClearMove()
        {
            Move = Moves.none;
        }

        public void ReadInput()
        {
            //Да ничего можно не делать
        }
    }
}