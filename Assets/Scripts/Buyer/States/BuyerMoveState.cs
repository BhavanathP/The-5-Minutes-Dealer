using States;
using UnityEngine;

public class BuyerMoveState : IState
{
    private readonly BuyerController _buyerController;

    public BuyerMoveState(BuyerController buyerController)
    {
        _buyerController = buyerController;
    }

    public void Enter()
    {
        _buyerController.m_animator.Play("Move");
    }

    public void Update()
    {
        var targetPosition = _buyerController.targetPoint.position;
        var currentPosition = _buyerController.transform.position;
        currentPosition.y = 0;
        targetPosition.y = 0;
        var direction = (targetPosition - currentPosition).normalized;
        float distance = Vector3.Distance(currentPosition, targetPosition);
        _buyerController.m_animator.SetFloat("Direction", direction.x > 0 ? 1 : -1);

        if (distance > 0.1f)
        {
            _buyerController.m_rigidbody2D.MovePosition(currentPosition + direction * _buyerController.MoveSpeed * Time.deltaTime);
        }
        else
        {
            _buyerController.waitingState._waitTimer = 0f;
            _buyerController.stateMachine.TransitionTo(_buyerController.waitingState);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        _buyerController.m_animator.SetFloat("Direction", 0);
    }
}