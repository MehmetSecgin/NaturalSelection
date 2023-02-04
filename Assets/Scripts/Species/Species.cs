using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Species
{
    public class Species : MonoBehaviour, ISpecies
    {
        // Properties
        public SpeciesProperties Properties { get; set; }

        // Components
        private Renderer _renderer;

        // Variables

        // Move Variables
        private Quaternion _targetRotation;
        private bool _rotating;
        private Vector3 _moveDirection;


        private void Awake()
        {
            _renderer = gameObject.GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            ChangeHeight();
            ChangeColorBySpeed();

            UpdateState(SpeciesState.Rotating);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void UpdateState(SpeciesState newState)
        {
            switch (newState)
            {
                case SpeciesState.Idle:
                    HandleIdle();
                    break;
                case SpeciesState.Rotating:
                    HandleRotating();
                    break;
                case SpeciesState.Searching:
                    HandleSearching();
                    break;
                case SpeciesState.FoundFood:
                    HandleFoundFood();
                    break;
                case SpeciesState.TakingFood:
                    HandleTakingFood();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void HandleRotating()
        {
            RotateAndMoveToRandom();
        }

        private void HandleTakingFood()
        {
        }

        private void HandleFoundFood()
        {
        }

        private void HandleSearching()
        {
        }

        private void HandleIdle()
        {
        }

        private void ChangeColorBySpeed()
        {
            _renderer.material.color = SpeciesPropertyFunctions.GetColorBySpeed(Properties.Speed);
        }

        private void ChangeHeight()
        {
            transform.localScale = SpeciesPropertyFunctions.GetHeight(Properties.Height);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Plane"))
            {
                RotateAndMoveToCenter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Plane"))
            {
                // canMove = true;
            }
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            transform.position += _moveDirection * (Properties.Speed * Time.deltaTime);

            // Rotate towards the target rotation
            if (!_rotating) return;
            transform.rotation =
                Quaternion.Slerp(transform.rotation, _targetRotation, Properties.RotationSpeed * Time.deltaTime);

            // Check if rotation is complete
            if (!(Quaternion.Angle(transform.rotation, _targetRotation) < 0.1f)) return;
            _rotating = false;
            StartCoroutine(ChangeDirection());
        }

        private IEnumerator ChangeDirection()
        {
            yield return new WaitForSeconds(Properties.RotationDuration);

            // Move in a new random direction
            UpdateState(SpeciesState.Rotating);
        }

        private void RotateAndMoveToRandom()
        {
            _moveDirection = RandomDirection();
            _targetRotation = Quaternion.LookRotation(_moveDirection);
            _rotating = true;
        }

        private static Vector3 RandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }

        private void RotateAndMoveToCenter()
        {
            _rotating = false;
            _moveDirection = (Vector3.zero - transform.position).normalized;
            _targetRotation = Quaternion.LookRotation(_moveDirection);
            _rotating = true;
        }
    }
}