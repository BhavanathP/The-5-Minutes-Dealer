using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FiveMinutesFarmer.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        private bool isPaused = false;

        void Awake()
        {
            resumeButton.onClick.AddListener(ResumeGame);
            restartButton.onClick.AddListener(RestartGame);
        }

        public void Show()
        {
            var remaining = UIManager.Instance.gameTimer.Remaining;
            int minutes = Mathf.FloorToInt(remaining / 60);
            int seconds = Mathf.FloorToInt(remaining % 60);
            timerText.SetText($"{minutes:D2}:{seconds:D2}");
            pauseMenuPanel.SetActive(true);
        }
        public void Hide()
        {
            pauseMenuPanel.SetActive(false);
        }

        internal void TogglePauseMenu()
        {
            isPaused = !isPaused;
            if (isPaused) Show();
            else Hide();
            Time.timeScale = isPaused ? 0f : 1f;
        }

        internal void ResumeGame()
        {
            AudioManager.Instance.PlaySFX(AudioType.UI_Click);
            isPaused = false;
            pauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }

        private void RestartGame()
        {
            AudioManager.Instance.PlaySFX(AudioType.UI_Click);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}