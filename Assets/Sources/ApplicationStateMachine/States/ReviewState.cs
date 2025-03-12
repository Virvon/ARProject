using Assets.Sources.BaseLogic;
using Assets.Sources.LoadingTree.SharedBundle;
using Assets.Sources.Services.InputService;
using UnityEngine;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class ReviewState : IState
    {
        private readonly ReviewView _reviewView;
        private readonly StateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly Camera _camera;

        private ReviewModel _review;
        private ReviewPresenter _reviewPresenter;

        public ReviewState(StateMachine stateMachine, SharedBundle sharedBundle)
        {
            _stateMachine = stateMachine;
            _reviewView = sharedBundle.Get<ReviewView>(SharedBundleKeys.ReviewView);
            _inputService = sharedBundle.Get<IInputService>(SharedBundleKeys.InputService);
            _camera = sharedBundle.Get<Camera>(SharedBundleKeys.Camera);
        }

        public void Enter()
        {
            _review = new(_stateMachine, _inputService, _camera);
            _reviewPresenter = new(_review, _reviewView);

            _reviewView.Show();
        }

        public void Exit()
        {
            _reviewPresenter.Dispose();
            _review.Dispose();
            _reviewView.Hide();
        }
    }
}
