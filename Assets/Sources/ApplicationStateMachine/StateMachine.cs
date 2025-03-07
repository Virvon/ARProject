using System;
using System.Collections.Generic;

namespace Assets.Sources.ApplicationStateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _currentState;

        public StateMachine() =>
            _states = new();

        public void Enter<TState>()
            where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void RegisterStates<TState>(params TState[] states)
            where TState : IExitableState
        {
            foreach (TState state in states)
                _states.Add(typeof(TState), state);
        }

        private TState GetState<TState>()
            where TState : class, IExitableState =>
                _states[typeof(TState)] as TState;

        private TState ChangeState<TState>()
            where TState : class, IExitableState
        {
            if (_currentState != null)
                _currentState.Exit();

            TState state = GetState<TState>();

            _currentState = state;

            return state;
        }
    }
}
