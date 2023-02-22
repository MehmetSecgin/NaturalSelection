using System.Collections;
using UnityEngine;

namespace Species
{
    public class FoodDetector : MonoBehaviour
    {
        public bool FoodInRange => _detectedFood != null;
        private Food _detectedFood;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Food>())
            {
                _detectedFood = other.GetComponent<Food>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Food>())
            {
                StartCoroutine(ClearDetectedFoodAfterDelay());
            }
        }

        private IEnumerator ClearDetectedFoodAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            _detectedFood = null;
        }

        public Vector3 GetNearestFoodPosition()
        {
            return _detectedFood?.transform.position ?? Vector3.zero;
        }
    }

    internal class Food : MonoBehaviour
    {
    }
}