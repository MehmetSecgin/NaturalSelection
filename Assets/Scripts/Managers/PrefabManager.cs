using UnityEngine;

namespace Managers
{
    public class PrefabManager : MonoBehaviour
    {
        [SerializeField] private GameObject speciesPrefab;
        [SerializeField] private GameObject foodPrefab;

        public GameObject GetSpeciesPrefab()
        {
            return speciesPrefab;
        }

        public GameObject GetFoodPrefab()
        {
            return foodPrefab;
        }
    }
}