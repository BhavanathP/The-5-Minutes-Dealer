using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FiveMinutesFarmer.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameoverPanel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text finalScoreText;
        [SerializeField] private TMP_Text finalCoinsText;
        [SerializeField] private Button restartButton;


        internal void Show()
        {
            gameoverPanel.SetActive(true);
        }
        internal void Hide()
        {
            gameoverPanel.SetActive(false);
        }

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        public void Show(int finalScore, int finalCoins, bool isCaught = false)
        {
            if (isCaught)
            {
                AudioManager.Instance.PlaySFX(AudioType.GameOverCaught);
                titleText.SetText("You got caught!");
            }
            else
            {
                AudioManager.Instance.PlaySFX(AudioType.GameOver);
                titleText.SetText("Game Over!");
            }
            gameoverPanel.SetActive(true);
            Time.timeScale = 0f;
            finalScoreText.SetText($"Final Score: {finalScore}");
            finalCoinsText.SetText($"Coins Earned: {finalCoins}");
        }

        private void RestartGame()
        {
            Time.timeScale = 1f;
            AudioManager.Instance.PlaySFX(AudioType.UI_Click);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}