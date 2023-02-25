using System;
using UnityEngine;
using UnityEngine.AI;

namespace Gatherable
{
    [Serializable]
    public abstract class GatherableResource : MonoBehaviour
    {
        public static event Action<GatherableResource> OnDepleted;

        protected int TotalAvailable;
        [SerializeField] protected int Available;
        public bool IsDepleted => Available <= 0;

        protected virtual void OnEnable()
        {
            Available = TotalAvailable;
        }

        public bool Take()
        {
            if (Available <= 0)
            {
                return false;
            }

            Available--;
            UpdateSize();
            return true;
        }

        public virtual void UpdateSize()
        {
            var scale = (float) Available / TotalAvailable;
            if (scale is > 0 and < 1f)
            {
                var localTransform = transform;
                var vectorScale = localTransform.localScale * scale;
                localTransform.localScale = vectorScale;
            }
            else if (scale <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            Debug.Log(name + " is depleted");
            OnDepleted?.Invoke(this);
        }

        [ContextMenu("Snap")]
        public void Snap()
        {
            if (NavMesh.SamplePosition(transform.position, out var hit, 5f, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
        }

        public void SetAvailable(int amount) => Available = amount;
    }
}