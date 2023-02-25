using UnityEngine;

namespace Species
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _cameraTransform.position);
        }
    }
}