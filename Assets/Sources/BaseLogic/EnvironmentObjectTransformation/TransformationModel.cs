using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObjectTransformation
{
    public class TransformationModel
    {
        private const string AnimationTrigger = "Animate";

        public readonly bool IsAnimated;

        private readonly StateMachine _stateMachine;
        private readonly EnvironmentObject.EnvironmentObject _environmentObject;
        private readonly Animator _animator;

        public TransformationModel(StateMachine stateMachine, EnvironmentObject.EnvironmentObject environmentObject)
        {
            _stateMachine = stateMachine;
            _environmentObject = environmentObject;

            _animator = _environmentObject.GetComponentInChildren<Animator>();
            IsAnimated = _animator != null;
        }

        public void MoveReviewState() =>
            _stateMachine.Enter<ReviewState>();

        public void MoveColorSelectionState() =>
            _stateMachine.Enter<ColorSelectionState, EnvironmentObject.EnvironmentObject>(_environmentObject);

        public void DestroyEnvironmentObject()
        {
            _environmentObject.Destroy();
            _stateMachine.Enter<ReviewState>();
        }

        public void ShowAnimation() =>
            _animator.SetTrigger(AnimationTrigger);
    }
}
