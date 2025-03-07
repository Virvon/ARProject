using Assets.Sources.BaseLogic.EnvironmentObjectCreation;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class EnvironmentObjectCreationState : IState
    {
        private readonly EnvironmentObjectCreator _creator;

        public EnvironmentObjectCreationState(EnvironmentObjectCreator creator)
        {
            _creator = creator;
        }

        public void Enter()
        {
            _creator.Create();
        }

        public void Exit()
        {
            
        }
    }
}
