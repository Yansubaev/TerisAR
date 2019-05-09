using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class UIController : Singleton<UIController>
    {
        [SerializeField] private Text level;
        [SerializeField] private Text _scoreText;

        [SerializeField] private Sprite[] previewSprites;
        [SerializeField] private GameObject previewImage;

        [SerializeField] private Sprite[] startStopSprites;
        [SerializeField] private Image startStopButton;

        [SerializeField] private GameObject gameOverText;
        [SerializeField] private Text gameOverResult;

        private bool isStartSprite = true;

        public Text ScoreText { get => _scoreText; }


        public void StartStop()
        {
            TetrisController.Instance.StartStop();

            previewImage.SetActive(true);

            if (isStartSprite)
            {
                startStopButton.sprite = startStopSprites[0];
                isStartSprite = false;
            }
            else
            {
                startStopButton.sprite = startStopSprites[1];
                isStartSprite = true;
            }
        }

        public void Restart()
        {
            TetrisController.Instance.RestartGame();

            level.text = "1";
            _scoreText.text = "0";

            isStartSprite = true;

            gameOverText.SetActive(false);

            startStopButton.sprite = startStopSprites[1];

            previewImage.SetActive(false);
        }

        public void GameOver()
        {
            gameOverResult.text = TetrisController.Instance.Score.ToString();
            gameOverText.SetActive(true);

            previewImage.SetActive(false);

            startStopButton.sprite = startStopSprites[1];
        }

        public void ChangeLevel(int l)
        {
            level.text = l.ToString();
        }

        public void ChangePreview()
        {
            string tag = TetrisController.Instance.PreviewTag;

            if (tag == "I")
            {
                previewImage.GetComponent<Image>().sprite = previewSprites[0];
            }
            else if (tag == "J")
            {
                previewImage.GetComponent<Image>().sprite = previewSprites[1];
            }
            else if (tag == "L")
            {
                previewImage.GetComponent<Image>().sprite = previewSprites[2];
            }
            else if (tag == "O")
            {
                previewImage.GetComponent<Image>().sprite = previewSprites[3];
            }
            else if (tag == "S")
            {
                previewImage.GetComponent<Image>().sprite = previewSprites[4];
            }
            else if (tag == "T")
            {
                previewImage.GetComponent<Image>().sprite = previewSprites[5];
            }
            else if (tag == "Z")
            {
                previewImage.GetComponent<Image>().sprite = previewSprites[6];
            }
        }

    }
}