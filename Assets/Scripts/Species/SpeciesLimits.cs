using System;
using UnityEngine;

namespace Species
{
    [Serializable]
    public class SpeciesLimits : MonoBehaviour
    {
        [Header("Speed and Height")]
        [Range(0f, 1f)] public float minSpeed;

        [Range(0f, 1f)] public float maxSpeed;

        [Range(0f, 2f)] public float minHeight;

        [Range(0f, 2f)] public float maxHeight;

        [Header("Rotation")]
        [Range(1f, 10f)] public float rotationSpeed;

        [Range(1f, 5f)] public float rotationDuration;
    }
}