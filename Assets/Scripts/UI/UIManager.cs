using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiveMinutesFarmer.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("UI References")]
        [SerializeField] private GameObject player;
        [SerializeField] private StartPanel startPanel;
        [SerializeField] private GameplayHUD gameplayHUD;
        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private GameOverUI gameOverUI;
        [SerializeField] private InventoryMenu inventoryMenu;
        public GameTimer gameTimer;
        public bool isCaught = false;

        private void Start()
        {
            gameplayHUD.Hide();
            pauseMenu.Hide();
            gameOverUI.Hide();

            startPanel.Show();
            startPanel.startButton.onClick.AddListener(StartGame);
        }

        public void StartGame()
        {
            Time.timeScale = 1f;
            player.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioType.UI_Click);
            startPanel.Hide();
            gameplayHUD.Show();
            // Subscribe to ScoreManager
            ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
            InputManager.Instance.OnPause += pauseMenu.TogglePauseMenu;
            CurrencyManager.Instance.OnCurrencyChanged += UpdateCoinText;

            // Get TimerManager & subscribe
            gameTimer = TimerManager.Instance.StartTimer(300f); // 5 min

            gameTimer.OnTick += UpdateTimerUI;
            gameTimer.OnCompleted += ShowGameOver;
        }

        private void UpdateScoreUI(int score)
        {
            gameplayHUD.UpdateScore(score);
        }
        public void UpdateCoinText(int coins)
        {
            gameplayHUD.UpdateCoins(coins);
        }

        private void UpdateTimerUI(float remaining)
        {
            int minutes = Mathf.FloorToInt(remaining / 60);
            int seconds = Mathf.FloorToInt(remaining % 60);
            gameplayHUD.UpdateTimer(minutes, seconds);
        }

        public void ShowGameOver()
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreUI;
            InputManager.Instance.OnPause -= pauseMenu.TogglePauseMenu;
            CurrencyManager.Instance.OnCurrencyChanged -= UpdateCoinText;
            pauseMenu.ResumeGame();

            gameOverUI?.Show(ScoreManager.Instance.GetScore(), CurrencyManager.Instance.GetEarnedCurrency(), isCaught);
        }

        public void ToggleInventory()
        {
            if (inventoryMenu.inventoryPanel.activeSelf)
                inventoryMenu.Hide();
            else
                inventoryMenu.Show();
        }
    }
}