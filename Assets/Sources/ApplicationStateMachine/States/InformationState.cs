using Assets.Sources.BaseLogic.Information;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class InformationState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly InformationView _informationView;

        private InformationPresenter _informationPresenter;

        public InformationState(StateMachine stateMachine, InformationView informationView)
        {
            _stateMachine = stateMachine;
            _informationView = informationView;
        }

        public void Enter()
        {
            InformationModel informationModel = new(_stateMachine);
            _informationPresenter = new(informationModel, _informationView);

            _informationView.Show();
        }

        public void Exit()
        {
            _informationPresenter.Dispose();

            _informationView.Hide();
        }
    }
}
