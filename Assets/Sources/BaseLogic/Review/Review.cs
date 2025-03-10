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

        public void MoveNextState() =>
            _stateMachine.Enter<EnvironmentObjectCreationState>();
    }
}
