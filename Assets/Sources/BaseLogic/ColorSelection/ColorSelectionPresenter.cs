using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.ColorSelection
{
    public class ColorSelectionPresenter : IDisposable
    {
        private readonly ColorSelection _model;
        private readonly ColorSelectionView _view;

        public ColorSelectionPresenter(ColorSelection colorSelection, ColorSelectionView colorSelectionView)
        {
            _model = colorSelection;
            _view = colorSelectionView;

            _view.SetColor(_model.EnvironmentObject.ColorChanger.Color);

            _view.ColorChanged += OnColorChanged;
            _view.HideButtonClicked += OnHideButtonClicked;
        }

        public void Dispose()
        {
            _view.ColorChanged -= OnColorChanged;
            _view.HideButtonClicked -= OnHideButtonClicked;
        }

        private void OnColorChanged(Color color) =>
            _model.SetColor(color);

        private void OnHideButtonClicked() =>
            _model.MoveNextState();
    }
}
