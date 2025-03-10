using Assets.Sources.BaseLogic;
using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class EndState : IState
    {
        private readonly ReviewPresenter _reviewPresenter;
        private readonly EnvironmentObjectCameraPositioner _creatingEnvironmentObjectPositioner;
        private readonly CreatorPresenter _creatorPresenter;
        private readonly EnvironmentObjectTransformator _environmentObjectTransformator;
        private readonly TransformationPresenter _transformationPresenter;

        public EndState(
            ReviewPresenter reviewPresenter,
            EnvironmentObjectCameraPositioner creatingEnvironmentObjectPositioner,
            CreatorPresenter creatorPresenter,
            EnvironmentObjectTransformator environmentObjectTransformator,
            TransformationPresenter transformationPresenter)
        {
            _reviewPresenter = reviewPresenter;
            _creatingEnvironmentObjectPositioner = creatingEnvironmentObjectPositioner;
            _creatorPresenter = creatorPresenter;
            _environmentObjectTransformator = environmentObjectTransformator;
            _transformationPresenter = transformationPresenter;
        }

        public void Enter()
        {
            _reviewPresenter.Dispose();
            _creatingEnvironmentObjectPositioner.Dispose();
            _creatorPresenter.Dispose();
            _environmentObjectTransformator.Dispose();
            _transformationPresenter.Dispose();
        }

        public void Exit()
        {
        }
    }
}
