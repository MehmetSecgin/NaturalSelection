using Gatherable;
using UnityEngine;
using UnityEngine.AI;

namespace Species.States
{
    public class MoveToSelectedResource : IState
    {
        private const string StateName = "MoveToSelectedResource";
        
        private readonly Species _species;
        private readonly NavMeshAgent _navMeshAgent;

        private Vector3 _lastPosition = Vector3.zero;

        public float TimeStuck;
        
        public MoveToSelectedResource(Species species, NavMeshAgent navMeshAgent)
        {
            _species = species;
            _navMeshAgent = navMeshAgent;
        }

        public void Tick()
        {
            if (!_species.targetStillActive) return;
            if (Vector3.Distance(_species.transform.position, _lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            _lastPosition = _species.transform.position;
        }

        public void OnEnter()
        {
            GatherableResource.OnDepleted += FoodNotActiveAnymore;
            TimeStuck = 0f;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_species.target.transform.position);
        }

        public void OnExit()
        {
            GatherableResource.OnDepleted -= FoodNotActiveAnymore;
            _navMeshAgent.enabled = false;
        }

        private void FoodNotActiveAnymore(GatherableResource resource)
        {
            _species.targetStillActive = false;
            Debug.Log(resource.name + " is depleted");
        }

        public override string ToString()
        {
            return StateName;
        }
    }
}