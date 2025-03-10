using Assets.Sources.BaseLogic;
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

        private Review _review;
        private ReviewPresenter _reviewPresenter;

        public ReviewState(ReviewView reviewView, StateMachine stateMachine, IInputService inputService, Camera camera)
        {
            _reviewView = reviewView;
            _stateMachine = stateMachine;
            _inputService = inputService;
            _camera = camera;
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
