using FiveMinutesFarmer.UI;
using States;
using UnityEngine;

public class BuyingState : IState
{
    private readonly BuyerController _buyerController;
    public BuyingState(BuyerController buyerController)
    {
        _buyerController = buyerController;
    }

    public void Enter()
    {
        _buyerController.m_animator.Play("Idle");
        PlayerManager.Instance.isSelling = true;
        AudioManager.Instance.PlayMusic(AudioType.Sell);
    }

    public void Update()
    {
        _buyerController.buyingTimer += Time.deltaTime;
        if (_buyerController.buyingTimer >= _buyerController.buyingTime)
        {
            PlayerManager.Instance.isSelling = false;
            PlayerManager.Instance.ShowSuccessEffect();
            _buyerController.timerImage.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioType.CoinGained);
            CurrencyManager.Instance.AddCurrency(DrugsManager.Instance.GetDrugDetails(_buyerController.drugType).sellPrice);
            ScoreManager.Instance.AddScore((int)_buyerController.drugType * 30);
            _buyerController.stateMachine.TransitionTo(_buyerController.returnState);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        PlayerManager.Instance.isSelling = false;
    }
}