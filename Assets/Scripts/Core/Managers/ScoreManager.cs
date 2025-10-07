using System;
using UnityEngine;

namespace FiveMinutesFarmer.UI
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public event Action<int> OnScoreChanged;

        private int score;

        public void AddScore(int amount)
        {
            score += amount;
            OnScoreChanged?.Invoke(score);
        }

        public void ResetScore()
        {
            score = 0;
            OnScoreChanged?.Invoke(score);
        }

        public int GetScore() => score;
    }
}