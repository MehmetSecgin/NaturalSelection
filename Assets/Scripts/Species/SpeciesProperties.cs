using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Species
{
    [Serializable]
    public class SpeciesProperties
    {
        [SerializeField] private float speed;
        [SerializeField] private float height;
        private SpeciesLimits _limits;

        private const float SpeedMultiplier = 10f;

        public SpeciesProperties(SpeciesLimits limits)
        {
            _limits = limits;
            Speed = GetRandomValue(_limits.minSpeed, _limits.maxSpeed);
            Height = GetRandomValue(_limits.minHeight, _limits.maxHeight);
        }

        public float Speed
        {
            get => speed;
            private set => speed = value * SpeedMultiplier;
        }

        public float Height
        {
            get => height;
            private set => height = value;
        }

        private float GetRandomValue(float minValue, float maxValue)
        {
            return Random.Range(minValue, maxValue);
        }
    }
}