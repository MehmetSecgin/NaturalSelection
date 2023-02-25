using UnityEngine;

namespace Species.States
{
    class NoFoodLeft : IState
    {
        private const string StateName = "NoFoodLeft";
        private readonly Species _species;
        public NoFoodLeft(Species species)
        {
            _species = species;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log(_species.name + " knows that no targets left");
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