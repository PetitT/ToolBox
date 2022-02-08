using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LowTeeGames
{
    public class Pool : MonoSingleton<Pool>
    {
        public List<GameObject> objectsToPool;
        public int numberToCreateAtStart;

        private Dictionary<GameObject, List<GameObject>> pool = new Dictionary<GameObject, List<GameObject>>();

        private void Awake()
        {
            foreach (var item in objectsToPool)
            {
                AddToPool(item);
            }
        }

        private void AddToPool(GameObject item)
        {
            pool.Add(item, new List<GameObject>());
            for (int i = 0; i < numberToCreateAtStart; i++)
            {
                GameObject newItem = Instantiate(item);
                newItem.SetActive(false);
                newItem.transform.SetParent(transform);
                pool[item].Add(newItem);
            }
        }

        public GameObject GetItemFromPool(GameObject item, Vector2 position)
        {
            if (!pool.ContainsKey(item))
            {
                AddToPool(item);
            }

            GameObject newItem = pool[item].Where(x => !x.activeSelf).FirstOrDefault();

            if (!newItem)
            {
                newItem = Instantiate(item, position, Quaternion.identity, transform);
                pool[item].Add(newItem);
            }
            else
            {
                newItem.transform.position = position;
                newItem.SetActive(true);
            }

            return newItem;
        }
    }
}