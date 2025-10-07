using UnityEngine;
using States;

namespace Player
{
    public class StateMachine
    {
        public IState _currentState;

        public void TransitionTo(IState nextState)
        {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}
