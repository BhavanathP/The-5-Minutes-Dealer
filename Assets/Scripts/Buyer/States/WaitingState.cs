using States;
using UnityEngine;

public class WaitingState : IState
{
    private readonly BuyerController _buyerController;
    public float _waitTimer = 0f;
    public WaitingState(BuyerController buyerController)
    {
        _buyerController = buyerController;
    }

    public void Enter()
    {
        _buyerController.m_animator.Play("Idle");
        if (_buyerController.drugType == DrugType.None)
        {
            var drugs = InventoryManager.Instance.GetAllDrugDetails();
            var drug = drugs[Random.Range(0, drugs.Count)];
            _buyerController.buyingTime = DrugsManager.Instance.GetDrugDetails(drug.drugType).duration;
            _buyerController.drugType = drug.drugType;
            _buyerController.drugImage.sprite = DrugsManager.Instance.GetDrugSprite(drug.drugType);
        }
    }

    public void Update()
    {
        _buyerController.drugImageObject.SetActive(_buyerController.timerImage.activeSelf == false);

        if (_waitTimer >= _buyerController.waitTime)
        {
            AudioManager.Instance.PlaySFX(AudioType.LostBuyer);
            _buyerController.stateMachine.TransitionTo(_buyerController.returnState);
        }

        if (_buyerController.buyingTimer > 0f)
        {
            _buyerController.buyingTimer -= Time.deltaTime / 4f;
        }
        else
        {
            _waitTimer += Time.deltaTime;
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        _buyerController.drugImageObject.SetActive(false);
    }
}