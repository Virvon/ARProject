using Assets.Sources.BaseLogic.ColorSelection;
using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.Services.InputService;
using Assets.Sources.SharedBundle;
using UnityEngine;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class ColorSelectionState : IPayloadState<EnvironmentObject>
    {
        private readonly IInputService _inputService;
        private readonly ColorSelectionView _colorSelectionView;
        private readonly StateMachine _stateMachine;
        private readonly Pointer _pointer;

        private ColorSelectionPresenter _colorSelectionPresenter;

        public ColorSelectionState(StateMachine stateMachine, SharedBundle sharedBundle)
        {
            _stateMachine = stateMachine;
            _inputService = sharedBundle.Get<IInputService>(SharedBundleKeys.InputService);
            _colorSelectionView = sharedBundle.Get<ColorSelectionView>(SharedBundleKeys.ColorSelectionView);
            _pointer = sharedBundle.Get<Pointer>(SharedBundleKeys.Pointer);
        }

        public void Enter(EnvironmentObject environmentObject)
        {
            ColorSelection colorSelection = new(environmentObject, _stateMachine);
            _colorSelectionPresenter = new(colorSelection, _colorSelectionView);

            _inputService.SetActive(false);
            _colorSelectionView.Show();
            _pointer.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _colorSelectionPresenter.Dispose();
            _inputService.SetActive(true);
            _colorSelectionView.Hide();
            _pointer.gameObject.SetActive(false);
        }
    }
}
