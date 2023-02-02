using System;
using UnityEngine;

namespace Species
{
    public class Species : MonoBehaviour, ISpecies
    {
        // Properties
        public SpeciesProperties Properties { get; set; }

        // Components
        private Renderer _renderer;
        private Rigidbody _rb;

        // Variables
        [SerializeField] private bool canMove = true;


        private void Awake()
        {
            _renderer = gameObject.GetComponent<MeshRenderer>();
            _rb = gameObject.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            ChangeHeight();
            ChangeColorBySpeed();
        }

        private void ChangeColorBySpeed()
        {
            _renderer.material.color = SpeciesPropertyFunctions.GetColorBySpeed(Properties.Speed);
        }

        private void ChangeHeight()
        {
            transform.localScale = SpeciesPropertyFunctions.GetHeight(Properties.Height);
        }

        private void FixedUpdate()
        {
            if (canMove)
            {
                Move();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Plane"))
            {
                canMove = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Plane"))
            {
                canMove = true;
            }
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            var vector = new Vector3(1, 0, 0);
            _rb.AddForce(vector * Time.deltaTime * Properties.Speed, ForceMode.Acceleration);
        }
    }
}