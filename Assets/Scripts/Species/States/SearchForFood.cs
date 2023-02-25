using System.Linq;
using Gatherable;
using UnityEngine;

namespace Species.States
{
    public class SearchForFood : IState
    {
        private const string StateName = "SearchForFood";
        private readonly Species _species;

        public SearchForFood(Species species)
        {
            _species = species;
        }

        public void Tick()
        {
            var resource = ChooseOneOfTheNearestFoods(5);
            if (resource != null)
            {
                _species.target = resource;
                _species.targetStillActive = true;
            }
            else
            {
                _species.noOtherTargetsLeft = true;
            }
        }

        private GatherableResource ChooseOneOfTheNearestFoods(int pickFromNearest)
        {
            var gatherableResources = Object.FindObjectsOfType<GatherableResource>()
                .Where(t => t.enabled && t.IsDepleted == false)
                .ToList();
            var nearestFoods =
                gatherableResources.OrderBy(t => Vector3.Distance(_species.transform.position, t.transform.position))
                    .Take(pickFromNearest)
                    .FirstOrDefault();
            return nearestFoods;
        }

        public void OnEnter()
        {
        }
        public void OnExit()
        {
        }

        public override string ToString()
        {
            return StateName;
        }
    }
}