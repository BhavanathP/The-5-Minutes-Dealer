using UnityEngine;

namespace States
{
    public class IdleState : IState
    {
        private readonly PlayerController _playerController;

        public IdleState(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Enter()
        {
            _playerController.Animator.Play("Idle");
        }

        public void Update()
        {
            // Check for input to move
            if (PlayerManager.Instance.Movement.movementInput.x != 0)
            {
                _playerController.StateMachine.TransitionTo(_playerController.MoveState);
            }
        }

        public void FixedUpdate()
        {
        }

        public void Exit()
        {
        }
    }
}
