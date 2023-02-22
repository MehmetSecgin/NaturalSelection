using UnityEngine;

namespace Species.States
{
    public class EatFood : IState
    {
        private const string StateName = "EatFood";
        private readonly Species _species;
        private const float ResourcesPerSecond = 3f;
        private float _nextTakeResourceTime;

        public EatFood(Species species)
        {
            _species = species;
        }

        public void Tick()
        {
            if (_species.Target == null) return;

            if (!(_nextTakeResourceTime <= Time.time)) return;

            _nextTakeResourceTime = Time.time + (1f / ResourcesPerSecond);
            _species.TakeFromTarget();
        }

        public void OnEnter()
        {
            Debug.Log(_species.name + " Entered State: " + StateName);
        }
        public void OnExit()
        {
            Debug.Log(_species.name + " Exited State: " + StateName);
        }

        public override string ToString()
        {
            return _species.name + ": EatFood";
        }
    }
}