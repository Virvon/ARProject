using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.Services.TickService
{
    public class TickService : ITickable
    {
        private List<ITickable> _tickables;
        private List<ITickable> _updatedTickables;

        private bool _isTickablesUpdated;

        public TickService()
        {
            _tickables = new();
            _updatedTickables = new();

            _isTickablesUpdated = false;
        }

        public void Tick()
        {
            foreach (ITickable tickable in _tickables)
                tickable.Tick();

            if(_isTickablesUpdated)
            {
                _tickables = _updatedTickables;
                _updatedTickables = new();
                _isTickablesUpdated = false;
                Debug.Log("Ticables count changed " + _tickables.Count);
            }
        }

        public void Register(ITickable tickable)
        {
            Debug.Log("start tickable register");
            _updatedTickables.AddRange(_tickables);
            _updatedTickables.Add(tickable);
            _isTickablesUpdated = true;
            Debug.Log("finish tickable register");
        }

        public void Remove(ITickable tickable)
        {
            Debug.Log("start ticable removed " + (_tickables.Any(value => value == tickable)));
            _updatedTickables.AddRange(_tickables);
            _updatedTickables.Remove(tickable);
            _isTickablesUpdated = true;
            Debug.Log("finish ticlable removed");
        }
    }
}
