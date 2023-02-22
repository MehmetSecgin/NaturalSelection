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
            if (Vector3.Distance(_species.transform.position, _lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            _lastPosition = _species.transform.position;
        }

        public void OnEnter()
        {
            TimeStuck = 0f;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_species.Target.transform.position);
            Debug.Log(_species.name + " Entered State: " + StateName);
        }
        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            Debug.Log(_species.name + " Exited State: " + StateName);
        }
    }
}