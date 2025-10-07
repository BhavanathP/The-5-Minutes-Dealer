using TMPro;
using UnityEngine;

namespace FiveMinutesFarmer.UI
{
    public class GameplayHUD : MonoBehaviour
    {
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text coinsText;
        [SerializeField] private TMP_Text timerText;

        internal void Show()
        {
            gameplayPanel.SetActive(true);
        }
        internal void Hide()
        {
            gameplayPanel.SetActive(false);
        }

        internal void UpdateCoins(int newAmount)
        {
            coinsText.SetText($"{newAmount}");
        }
        internal void UpdateScore(int newScore)
        {
            scoreText.SetText($"Score: {newScore}");
        }
        internal void UpdateTimer(float minutes, float seconds)
        {
            timerText.SetText($"Time: {minutes:00}:{seconds:00}");
        }
    }
}