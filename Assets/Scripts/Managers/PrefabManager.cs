using UnityEngine;

namespace Managers
{
    public class PrefabManager : MonoBehaviour
    {
        [SerializeField] private GameObject speciesPrefab;

        public GameObject GetSpeciesPrefab()
        {
            return speciesPrefab;
        }
    }
}