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

        private void ChangeColorBySpeed()
        {
            var redValue = GetRedValue(Properties.Speed);
            var materialColor = new Color32(redValue, 100, 0, 100);
            _renderer.material.color = materialColor;
        }

        private static byte GetRedValue(float speed)
        {
            return (byte) ((1 - speed) * 255f);
        }

        private void ChangeHeight()
        {
            var localScale = transform.localScale;
            localScale = new Vector3(localScale.x, Properties.Height, localScale.z);
            transform.localScale = localScale;
        }
    }
}