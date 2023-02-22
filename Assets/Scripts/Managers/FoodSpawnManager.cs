using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class FoodSpawnManager : MonoBehaviour
    {
        // Prefab Settings
        private PrefabManager _prefabManager;
        private GameObject _foodPrefab;

        // Spawner Settings
        [HeaderAttribute("Spawner Settings")]
        [SerializeField] private PlaneProperties planeProperties;

        [SerializeField] private int foodToSpawn = 100;

        public List<GameObject> FoodList { get; } = new();

        private void Awake()
        {
            _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
            _foodPrefab = _prefabManager.GetFoodPrefab();
        }
        private void Start()
        {
            SpawnAllSpecies();
        }

        private void SpawnAllSpecies()
        {
            for (var i = 0; i < foodToSpawn; i++) SpawnSpecies();
        }

        private void SpawnSpecies()
        {
            var speciesObject = Instantiate(_foodPrefab, GetRandomSpawnLocation(), transform.rotation);
            Debug.Log(speciesObject.transform.position);
            FoodList.Add(speciesObject);
        }

        private Vector3 GetRandomSpawnLocation()
        {
            var x = Random.Range(-planeProperties.XBounds, planeProperties.XBounds);
            var z = Random.Range(-planeProperties.YBounds, planeProperties.YBounds);

            var randomSpawnLocation = new Vector3(x, 1, z);
            Debug.Log(randomSpawnLocation);
            return randomSpawnLocation;
        }
    }
}