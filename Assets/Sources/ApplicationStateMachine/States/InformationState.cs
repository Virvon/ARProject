using Assets.Sources.BaseLogic.Information;
using Assets.Sources.SharedBundle;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class InformationState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly InformationView _informationView;

        private InformationPresenter _informationPresenter;

        public InformationState(StateMachine stateMachine, SharedBundle.SharedBundle sharedBundle)
        {
            _stateMachine = stateMachine;
            _informationView = sharedBundle.Get<InformationView>(SharedBundleKeys.InformationView);
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
