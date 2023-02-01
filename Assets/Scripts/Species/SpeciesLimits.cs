using System;
using UnityEngine;

namespace Species
{
    [Serializable]
    public class SpeciesLimits : MonoBehaviour
    {
        [Range(0f, 1f)] public float minSpeed;

        [Range(0f, 1f)] public float maxSpeed;

        [Range(0f, 2f)] public float minHeight;

        [Range(0f, 2f)] public float maxHeight;
    }
}