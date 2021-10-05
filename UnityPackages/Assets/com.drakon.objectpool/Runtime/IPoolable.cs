using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drakon.ObjectPool
{
    public interface IPoolable
    {
        /// <summary>
        /// The index of this poolable instance in the pool
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Whether or not the object is currently spawned
        /// </summary>
        bool Spawned { get; set; }

        /// <summary>
        /// Resets the object to it's spawn state
        /// </summary>
        void Spawn();

        /// <summary>
        /// Removes the object returning it to the pool
        /// </summary>
        void Despawn();
    }
}