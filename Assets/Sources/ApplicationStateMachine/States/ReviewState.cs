using Assets.Sources.BaseLogic;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class ReviewState : IState
    {
        private readonly ReviewView _reviewView;
        private readonly StateMachine _stateMachine;

        private Review _review;
        private ReviewPresenter _reviewPresenter;

        public ReviewState(ReviewView reviewView, StateMachine stateMachine)
        {
            _reviewView = reviewView;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _review = new(_stateMachine);
            _reviewPresenter = new(_review, _reviewView);

            _reviewView.Show();
        }

        public void Exit()
        {
            _reviewPresenter.Dispose();
            _reviewView.Hide();
        }
    }
}
