using Assets.Sources.BaseLogic;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class ReviewState : IState
    {
        private readonly Review _review;

        public ReviewState(Review review) =>
            _review = review;

        public void Enter() =>
            _review.SetActive(true);

        public void Exit()
        {        
        }
    }
}
