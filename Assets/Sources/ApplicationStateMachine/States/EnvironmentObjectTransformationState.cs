using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class EnvironmentObjectTransformationState : IPayloadState<EnvironmentObject>
    {
        private readonly EnvironmentObjectTransformator _transformator;

        public EnvironmentObjectTransformationState(EnvironmentObjectTransformator transformator)
        {
            _transformator = transformator;
        }

        public void Enter(EnvironmentObject environmentObject)
        {
            _transformator.SetObject(environmentObject);
        }

        public void Exit()
        {
            _transformator.Dispose();
        }
    }
}
