using System;
using Gatherable;
using Species.States;
using UnityEngine;
using UnityEngine.AI;

namespace Species
{
    public class Species : MonoBehaviour
    {
        public event Action<int> OnFoodCountChanged;

        // Properties
        public SpeciesProperties Properties { get; set; }
        public GatherableResource Target { get; set; }

        // Components
        private Renderer _renderer;

        
        private StateMachine _stateMachine;
        private NavMeshAgent _navMeshAgent;

        private int _foodCount = 0;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            
            _stateMachine = new StateMachine();

            var search = new SearchForFood(this);
            var moveToSelected = new MoveToSelectedResource(this, _navMeshAgent);
            var eatFood = new EatFood(this);

            At(search, moveToSelected, HasTarget());
            At(moveToSelected, search, StuckForOverASecond());
            At(moveToSelected, eatFood, ReachedResource());
            At(eatFood, search, TargetIsDepleted());

            _stateMachine.SetState(search);

            void At(IState from, IState to, Func<bool> condition) =>
                _stateMachine.AddTransition(from, to, condition);

            Func<bool> HasTarget() => () => Target != null;
            Func<bool> StuckForOverASecond() => () => moveToSelected.TimeStuck > 1f;
            Func<bool> ReachedResource() => () =>
                Target != null && Vector3.Distance(transform.position, Target.transform.position) < 3f;
            Func<bool> TargetIsDepleted() => () => (Target == null || Target.IsDepleted);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        public void TakeFromTarget()
        {
            if (!Target.Take()) return;
            _foodCount++;
            OnFoodCountChanged?.Invoke(_foodCount);
        }


        private void ChangeColorBySpeed()
        {
            _renderer.material.color = SpeciesPropertyFunctions.GetColorBySpeed(Properties.Speed);
        }

        private void ChangeHeight()
        {
            transform.localScale = SpeciesPropertyFunctions.GetHeight(Properties.Height);
        }
    }
}