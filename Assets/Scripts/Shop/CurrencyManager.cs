using System;
using FiveMinutesFarmer.UI;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public event Action<int> OnCurrencyChanged;

    private int currency;
    private int earnedCurrency;

    void Start()
    {
        currency = 100; // Starting currency for testing
        UIManager.Instance.UpdateCoinText(currency);
    }
    public void AddCurrency(int amount)
    {
        currency += amount;
        earnedCurrency += amount;
        OnCurrencyChanged?.Invoke(currency);
    }

    public bool SpendCurrency(int amount)
    {
        if (currency < amount) return false;
        currency -= amount;
        OnCurrencyChanged?.Invoke(currency);
        return true;
    }

    public bool HasEnough(int amount) => currency >= amount;
    public int GetAllCurrency() => currency;
    public int GetEarnedCurrency() => earnedCurrency;
}
