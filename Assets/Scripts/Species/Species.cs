using System;
using UnityEngine;

namespace Species
{
    public class Species : MonoBehaviour, ISpecies
    {
        private Renderer _renderer;
        private Rigidbody _rb;
        public SpeciesProperties Properties { get; set; }

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
            Move();
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            _rb.AddForce(new Vector3(1, 0, 0) * Time.deltaTime * Properties.Speed * 100f,ForceMode.VelocityChange);
        }

        private void ChangeColorBySpeed()
        {
            var redValue = GetRedValue(Properties.Speed);
            var materialColor = new Color32(redValue, 100, 0, 100);
            _renderer.material.color = materialColor;
        }

        private static byte GetRedValue(float speed)
        {
            return (byte) (speed * 255f);
        }

        private void ChangeHeight()
        {
            var localScale = transform.localScale;
            localScale = new Vector3(localScale.x, Properties.Height, localScale.z);
            transform.localScale = localScale;
        }
    }
}