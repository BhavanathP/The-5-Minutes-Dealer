using States;
using UnityEngine;

public class BuyerReturnState : IState
{
    private readonly BuyerController _buyerController;
    Transform closestStartPoint = null;

    public BuyerReturnState(BuyerController buyerController)
    {
        _buyerController = buyerController;
    }
    public void Enter()
    {
        _buyerController.m_animator.Play("Move");
        // Find the closest start point to the buyer's current position

        float minDistance = float.MaxValue;
        Vector3 buyerPosition = _buyerController.transform.position;

        foreach (var startPoint in BuyerManager.Instance.startPoints)
        {
            float distance = Vector3.Distance(buyerPosition, startPoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestStartPoint = startPoint;
            }
        }
    }

    public void Update()
    {
        var targetPosition = closestStartPoint.position;
        var currentPosition = _buyerController.transform.position;
        currentPosition.y = 0;
        targetPosition.y = 0;
        var direction = (targetPosition - currentPosition).normalized;
        float distance = Vector3.Distance(currentPosition, targetPosition);

        _buyerController.m_animator.SetFloat("Direction", direction.x);

        if (distance > 0.1f)
        {
            _buyerController.m_rigidbody2D.MovePosition(currentPosition + direction * _buyerController.MoveSpeed * Time.deltaTime);
        }
        else
        {
            BuyerManager.Instance.ReleaseBuyer(_buyerController);
            // _buyerController.stateMachine.TransitionTo(_buyerController.waitingState);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
    }
}