using System;
using Gatherable;
using Species.States;
using UnityEngine;
using UnityEngine.AI;

namespace Species
{
    public class Species : MonoBehaviour
    {
        public event Action<IState> OnStateChanged;

        // Properties
        public SpeciesProperties Properties { get; set; }

        [SerializeField] public GatherableResource target;
        public bool targetStillActive = true;
        public bool noOtherTargetsLeft;

        // Components
        private Renderer _renderer;
        protected internal StateMachine StateMachine;
        private NavMeshAgent _navMeshAgent;

        private int _foodCount;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            targetStillActive = true;

            StateMachine = new StateMachine();
            StateMachine.OnStateChanged += OnStateChanged;

            var search = new SearchForFood(this);
            var moveToSelected = new MoveToSelectedResource(this, _navMeshAgent);
            var eatFood = new EatFood(this);
            var noFood = new NoFoodLeft(this);
            
            At(search, noFood, NoTargetLeft());
            At(search, moveToSelected, HasTarget());
            At(moveToSelected, eatFood, ReachedResource());
            At(moveToSelected, search, TargetIsDepleted());
            At(eatFood, search, TargetIsDepleted());

            StateMachine.SetState(search);

            void At(IState from, IState to, Func<bool> condition) => 
                StateMachine.AddTransition(from, to, condition);

            Func<bool> HasTarget() => () => target != null;

            Func<bool> TargetIsDepleted() => () => (target == null || target.IsDepleted);

            Func<bool> ReachedResource() => () =>
                targetStillActive && Vector3.Distance(transform.position, target.transform.position) < 4f;

            Func<bool> NoTargetLeft() => () => noOtherTargetsLeft;
        }

        public void CurrentTargetIsDepleted(GatherableResource resource)
        {
            target = resource;
        }

        private void Update()
        {
            StateMachine.Tick();
        }

        public void TakeFromTarget()
        {
            if (!target.Take()) return;
            _foodCount++;
            // OnFoodCountChanged?.Invoke(_foodCount);
        }

        private void OnDestroy()
        {
            StateMachine.OnStateChanged -= OnStateChanged;
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