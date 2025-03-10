using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using System;

namespace Assets.Sources.BaseLogic
{
    public class Review
    {
        private readonly StateMachine _stateMachine;

        public Review(StateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public event Action<bool> ActiveChanged;

        public void SetActive(bool isActive) =>
            ActiveChanged?.Invoke(isActive);

        public void MoveNextState()
        {
            SetActive(false);
            _stateMachine.Enter<EnvironmentObjectCreationState>();
        }
    }
}
