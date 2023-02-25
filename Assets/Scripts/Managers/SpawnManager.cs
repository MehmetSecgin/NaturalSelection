using System.Collections.Generic;
using Species;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        // Prefab Settings
        private PrefabManager _prefabManager;
        private GameObject _speciesPrefab;

        // Species Property Limits
        private SpeciesLimits _speciesLimits;

        // Spawner Settings
        [HeaderAttribute("Spawner Settings")] 
        [SerializeField] private PlaneProperties planeProperties;
        [SerializeField] private int speciesToSpawn = 100;

        public List<Species.Species> SpeciesList { get; } = new();

        private void Awake()
        {
            _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
            _speciesPrefab = _prefabManager.GetSpeciesPrefab();

            _speciesLimits = GetComponent<SpeciesLimits>();
        }

        private void Start()
        {
            SpawnAllSpecies();
        }

        private void SpawnAllSpecies()
        {
            for (var i = 0; i < speciesToSpawn; i++) SpawnSpecies();
        }

        private void SpawnSpecies()
        {
            // Initialize properties
            var properties = new SpeciesProperties(_speciesLimits);
            // Instantiate the object, assign its 
            var speciesObject = Instantiate(_speciesPrefab, GetRandomSpawnLocation(), transform.rotation)
                .GetComponent<Species.Species>();
            speciesObject.Properties = properties;
            SpeciesList.Add(speciesObject);
        }

        private Vector3 GetRandomSpawnLocation()
        {
            var x = Random.Range(-planeProperties.XBounds, planeProperties.XBounds);
            var z = Random.Range(-planeProperties.YBounds, planeProperties.YBounds);

            return new Vector3(x, 0.1f, z);
        }
    }
}