using Assets.Sources.BaseLogic.ColorSelection;
using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.Services.InputService;
using UnityEngine;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class ColorSelectionState : IPayloadState<EnvironmentObject>
    {
        private readonly IInputService _inputService;
        private readonly ColorSelectionView _colorSelectionView;
        private readonly StateMachine _stateMachine;

        private ColorSelectionPresenter _colorSelectionPresenter;

        public ColorSelectionState(IInputService inputService, ColorSelectionView colorSelectionView, StateMachine stateMachine)
        {
            _inputService = inputService;
            _colorSelectionView = colorSelectionView;
            _stateMachine = stateMachine;
        }

        public void Enter(EnvironmentObject environmentObject)
        {
            Debug.Log("Enter color selection state");

            ColorSelection colorSelection = new(environmentObject, _stateMachine);
            _colorSelectionPresenter = new(colorSelection, _colorSelectionView);

            _inputService.SetActive(false);
            _colorSelectionView.Show();
        }

        public void Exit()
        {
            _colorSelectionPresenter.Dispose();
            _inputService.SetActive(true);
            _colorSelectionView.Hide();
        }
    }
}
