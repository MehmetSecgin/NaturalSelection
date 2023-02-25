using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Gatherable
{
    class Bush : GatherableResource
    {
        private readonly List<GameObject> _berryList = new();
        private readonly Random _random = new();

        private void Awake()
        {
            var berries = FindChildren(transform, "Berry");
            foreach (var berry in berries)
            {
                _berryList.Add(berry.gameObject);
            }
        }

        protected override void OnEnable()
        {
            TotalAvailable = _berryList.Count;
            base.OnEnable();
        }


        public override void UpdateSize()
        {
            if (Available > 0)
            {
                var randomNumber = _random.Next(0, _berryList.Count - 1);
                _berryList[randomNumber].gameObject.SetActive(false);
                _berryList.RemoveAt(randomNumber);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private static IEnumerable<Transform> FindChildren(Component transform, string name)
        {
            return transform.GetComponentsInChildren<Transform>().Where(t => t.name == name).ToArray();
        }
    }
}