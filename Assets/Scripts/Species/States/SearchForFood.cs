using System;
using System.Linq;
using Gatherable;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Species.States
{
    public class SearchForFood : IState
    {
        private const string StateName = "SearchForFood";
        private readonly Species _species;
        public Action OnDepleted;

        public SearchForFood(Species species)
        {
            _species = species;
        }

        public void Tick()
        {
            _species.Target = ChooseOneOfTheNearestFoods(5);
        }

        private GatherableResource ChooseOneOfTheNearestFoods(int pickFromNearest)
        {
            // todo EXTREMELY UNOPTIMIZED
            var chooseOneOfTheNearestFoods = Object.FindObjectsOfType<GatherableResource>()
                .OrderBy(t => Vector3.Distance(_species.transform.position, t.transform.position))
                .Where(t => t.IsDepleted == false)
                .Take(pickFromNearest)
                .OrderBy(t => Random.Range(0, int.MaxValue))
                .FirstOrDefault();
            return chooseOneOfTheNearestFoods;
        }

        public void OnEnter()
        {
            Debug.Log(_species.name + " Entered State: " + StateName);
        }
        public void OnExit()
        {
            OnDepleted?.Invoke();
            Debug.Log(_species.name + " Exited State: " + StateName);
        }

        public override string ToString()
        {
            return _species.name + ": SearchForFood";
        }
    }
}