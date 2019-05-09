using UnityEngine;

namespace Tetris {
    public class TetrisController : Singleton<TetrisController>
    {

        private int _score;

        [SerializeField] private SpawnerScript spawner;

        [SerializeField] private Transform anchor;

        private enum InputWay { touch, key };
        private IMoves inputWay;

        private MoveController moveController;
        private readonly KeyInputHandler keyControls = new KeyInputHandler();

        [SerializeField] InputWay input;
        [SerializeField] private TouchInputHandler swipeControls;

        private bool canMove = true;

        private bool isGameOver = false;
        private bool isGameStarted = false;

        private string _previewtag;

        private Vector3 previewPosition = new Vector3(0, 0, 0);

        private float _speed = 1f;

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                SpeedChanging(value);
                _score = value;
                UIController.Instance.ScoreText.text = value.ToString();
            }
        }

        public string PreviewTag { get => spawner.PreviewObject.tag; }

        public float Speed { get => _speed; set => _speed = value; }



        private void Awake()
        {
            Time.timeScale = 0f;
            canMove = false;

            MatrixGrid.scaleFactor = anchor.lossyScale.x;

            spawner.Anchor = anchor;
        }

        private void Update()
        {
            MatrixGrid.UpdateScores();          

            if (moveController != null && spawner.NextObject != null)
            {
                moveController.MoveMove();

                inputWay.ReadInput();
            }
        }

        public void Spawn()
        {
            if (!isGameStarted)
            {
                spawner.SpawnFirstObject();
                isGameStarted = true;
            }
            else
                spawner.SpawnNextObject();

            if (input == InputWay.key)
                inputWay = keyControls;
            else
                inputWay = swipeControls;

            moveController = new MoveController
                (
                inputWay,
                spawner.NextObject.transform,
                spawner.NextObject.GetComponent<TetrisObject>().allowRotation,
                spawner.NextObject.GetComponent<TetrisObject>().limitRotation
                );

            UIController.Instance.ChangePreview();
        }


        public void StartByButton()
        {
            Time.timeScale = 1f;

            Spawn();

            canMove = true;

        }

        public void StartStop()
        {
            if (!isGameStarted)
            {
                StartByButton();
            }
            else if (canMove)
            {
                PauseByButton();

                moveController.CanMove = false;
            }
            else if (!canMove)
            {
                ResumeByButton();

                moveController.CanMove = true;
            }

            if (isGameOver)
            {
                RestartGame();
            }
        }

        public void PauseByButton()
        {
            Time.timeScale = 0f;

            canMove = false;
        }

        public void ResumeByButton()
        {
            Time.timeScale = 1f;

            canMove = true;
        }

        public void WhenGameOver()
        {
            isGameOver = true;
            canMove = false;

            moveController.CanMove = false;
            UIController.Instance.GameOver();

            _score = 0;

        }

        public void RestartGame()
        {
            MatrixGrid.ClearWholeMatrix();
            MatrixGrid.Scores = 0;

            spawner.ClearObjects();

            if (isGameStarted)
            {
                Time.timeScale = 0f;
                canMove = false;
                isGameStarted = false;
            }
            else
            {
                Time.timeScale = 1f;
                canMove = true;

                StartByButton();
            }

            isGameOver = false;

        }

        public void StartMoveDown()
        {
            spawner.NextObject.GetComponent<TetrisObject>().TimeSpeed = 0.015f;
        }

        private void SpeedChanging(int scores)
        {
            if (scores < 1000)
            {
                _speed = 1f;
                UIController.Instance.ChangeLevel(1);
            }
            else if (scores < 2000)
            {
                _speed = 0.85f;
                UIController.Instance.ChangeLevel(2);
            }
            else if (scores < 3000)
            {
                _speed = 0.7f;
                UIController.Instance.ChangeLevel(3);
            }
            else if (scores < 4000)
            {
                _speed = 0.6f;
                UIController.Instance.ChangeLevel(4);
            }
            else if (scores < 5000)
            {
                _speed = 0.5f;
                UIController.Instance.ChangeLevel(5);
            }
            else if (scores < 6000)
            {
                _speed = 0.4f;
                UIController.Instance.ChangeLevel(6);
            }
            else if (scores < 8000)
            {
                _speed = 0.33f;
                UIController.Instance.ChangeLevel(7);
            }
            else if (scores < 10000)
            {
                _speed = 0.29f;
                UIController.Instance.ChangeLevel(8);
            }
            else if (scores < 12000)
            {
                _speed = 0.25f;
                UIController.Instance.ChangeLevel(9);
            }
            else if (scores < 15000)
            {
                _speed = 0.21f;
                UIController.Instance.ChangeLevel(10);
            }
            else if (scores < 18000)
            {
                _speed = 0.17f;
                UIController.Instance.ChangeLevel(11);
            }
        }
    }
}
