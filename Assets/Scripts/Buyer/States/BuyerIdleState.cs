using States;

public class BuyerIdleState : IState
{
    private readonly BuyerController _buyerController;

    public BuyerIdleState(BuyerController buyerController)
    {
        _buyerController = buyerController;
    }

    public void Enter()
    {
        _buyerController.m_animator.Play("Idle");
    }

    public void Update()
    {
        if (_buyerController.targetPoint != null)
        {
            _buyerController.stateMachine.TransitionTo(_buyerController.moveState);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
    }
}