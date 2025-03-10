using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class EnvironmentObjectTransformationState : IState
    {
        private readonly EnvironmentObjectTransformator _transformator;
        private readonly EnvironmentObjectHandlerPositioner _positioner;
        private readonly TransformationView _transformationView;

        public EnvironmentObjectTransformationState(EnvironmentObjectTransformator transformator, EnvironmentObjectHandlerPositioner positioner, TransformationView transformationView)
        {
            _transformator = transformator;
            _positioner = positioner;
            _transformationView = transformationView;
        }

        public void Enter()
        {
            _transformator.SetActive(true);
            _positioner.SetActive(true);
            _transformationView.Show();
        }

        public void Exit()
        {
            _transformator.SetActive(false);
            _positioner.SetActive(false);
            _transformationView.Hide();
        }
    }
}
