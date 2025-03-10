using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using UnityEngine;

namespace Assets.Sources.BaseLogic.ColorSelection
{
    public class ColorSelection
    {
        public readonly EnvironmentObject.EnvironmentObject EnvironmentObject;

        private readonly StateMachine _stateMachine;

        public ColorSelection(EnvironmentObject.EnvironmentObject environmentObject, StateMachine stateMachine)
        {
            EnvironmentObject = environmentObject;
            _stateMachine = stateMachine;
        }

        public void SetColor(Color color) =>
            EnvironmentObject.SetColor(color);

        public void MoveNextState() =>
            _stateMachine.Enter<EnvironmentObjectTransformationState, EnvironmentObject.EnvironmentObject>(EnvironmentObject);
    }
}
