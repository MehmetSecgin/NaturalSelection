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
            private set => _rotationSpeed = value * Speed;
        }

        public float RotationDuration
        {
            get =>
                Random.Range(_rotationDuration - RotationDurationRandomnessThreshold,
                    _rotationDuration + RotationDurationRandomnessThreshold);
            private set => _rotationDuration = value * (5 - Speed);
        }

        public SpeciesProperties(SpeciesLimits limits)
        {
            _limits = limits;
            Speed = GetRandomValue(_limits.minSpeed, _limits.maxSpeed);
            Height = GetRandomValue(_limits.minHeight, _limits.maxHeight);
            RotationSpeed = _limits.rotationSpeed;
            RotationDuration = _limits.rotationDuration;
        }

        private float GetRandomValue(float minValue, float maxValue)
        {
            return Random.Range(minValue, maxValue);
        }

        public override string ToString()
        {
            return "MoveSpeed: " + Speed +
                   " Height: " + Height +
                   " Rotation Duration: " + RotationDuration +
                   " Rotation Speed: " + RotationSpeed;
        }
    }
}