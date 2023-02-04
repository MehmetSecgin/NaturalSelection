using System;
using Random = UnityEngine.Random;

namespace Species
{
    [Serializable]
    public class SpeciesProperties
    {
        private float _moveSpeed;
        private float _height;

        private float _rotationSpeed;
        private float _rotationDuration;

        private SpeciesLimits _limits;

        private const float SpeedMultiplier = 5f;
        private const float RotationDurationRandomnessThreshold = .5f;

        public float Speed
        {
            get => _moveSpeed;
            private set => _moveSpeed = value * SpeedMultiplier;
        }

        public float Height
        {
            get => _height;
            private set => _height = value;
        }

        public float RotationSpeed
        {
            get => _rotationSpeed;
            private set => _rotationSpeed = value;
        }

        public float RotationDuration
        {
            get =>
                Random.Range(_rotationDuration - RotationDurationRandomnessThreshold,
                    _rotationDuration + RotationDurationRandomnessThreshold);
            private set => _rotationDuration = value;
        }

        public SpeciesProperties(SpeciesLimits limits)
        {
            _limits = limits;
            RotationSpeed = _limits.rotationSpeed;
            RotationDuration = _limits.rotationDuration;
            Speed = GetRandomValue(_limits.minSpeed, _limits.maxSpeed);
            Height = GetRandomValue(_limits.minHeight, _limits.maxHeight);
        }

        private float GetRandomValue(float minValue, float maxValue)
        {
            return Random.Range(minValue, maxValue);
        }
    }
}