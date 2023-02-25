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
            if (_species.target == null)
            {
                _species.targetStillActive = false;
                return;
            }

            if (!(_nextTakeResourceTime <= Time.time)) return;

            _nextTakeResourceTime = Time.time + 1f / ResourcesPerSecond;
            _species.TakeFromTarget();
        }

        public void OnEnter()
        {
        }
        public void OnExit()
        {
        }

        public override string ToString()
        {
            return StateName;
        }
    }
}