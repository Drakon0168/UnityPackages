using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drakon.ObjectPool
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private static ObjectPool<T> instance;

        /// <summary>
        /// The prefab to spawn when spawning from this pool
        /// </summary>
        public static GameObject Prefab { get; set; } = null;

        /// <summary>
        /// The singleton instance of the pool
        /// </summary>
        public static ObjectPool<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjectPool<T>();
                }

                return instance;
            }
        }

        /// <summary>
        /// The pool of objects;
        /// </summary>
        public static List<T> Pool { get; private set; } = new List<T>();

        /// <summary>1
        /// Spawns an instance of the object
        /// </summary>
        /// <param name="position">The position to spawn at</param>
        /// <param name="orientation">The orientation to spawn at</param>
        /// <returns>The spawned object</returns>
        public T SpawnObject(Vector3 position, Quaternion orientation)
        {
            if (Prefab == null)
            {
                Debug.LogError($"Failed to instantiate object, no prefab set. ({typeof(T)})");
                return null;
            }

            for(int i = 0; i < Pool.Count; i++)
            {
                if (!Pool[i].Spawned)
                {
                    Pool[i].transform.position = position;
                    Pool[i].transform.rotation = orientation;
                    Pool[i].gameObject.SetActive(true);

                    Pool[i].Index = i;
                    Pool[i].Spawned = true;
                    Pool[i].Spawn();
                    return Pool[i];
                }
            }

            Pool.Add(Object.Instantiate(Prefab, position, orientation, null).GetComponent<T>());

            Pool[Pool.Count - 1].Index = Pool.Count - 1;
            Pool[Pool.Count - 1].Spawned = true;
            Pool[Pool.Count - 1].Spawn();
            return Pool[Pool.Count - 1];
        }

        /// <summary>
        /// Despawns the specified object
        /// </summary>
        /// <param name="value">The object to despawn</param>
        public void DespawnObject(T value)
        {
            Pool[value.Index].Despawn();

            Pool[value.Index].gameObject.SetActive(false);
            Pool[value.Index].Spawned = false;
            Debug.Log($"Pool Size: {Pool.Count}");
        }
    }
}