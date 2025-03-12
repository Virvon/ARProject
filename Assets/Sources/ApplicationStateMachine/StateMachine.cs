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
            TState state = Change<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadState<TPayload>
        {
            TState state = Change<TState>();
            state.Enter(payload);
        }

        public void Register<TState>(TState state)
            where TState : IExitableState =>
            _states.Add(typeof(TState), state);

        private TState Get<TState>()
            where TState : class, IExitableState =>
                _states[typeof(TState)] as TState;

        private TState Change<TState>()
            where TState : class, IExitableState
        {
            if (_currentState != null)
                _currentState.Exit();

            TState state = Get<TState>();

            _currentState = state;

            return state;
        }
    }
}
