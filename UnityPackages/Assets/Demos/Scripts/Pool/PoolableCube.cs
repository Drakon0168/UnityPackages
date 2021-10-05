using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drakon.ObjectPool;

public class PoolableCube : MonoBehaviour, IPoolable
{
    [SerializeField]
    private float objectData = 0.0f;

    public int Index { get; set; }

    public bool Spawned { get; set; }

    public event System.Action OnSpawn;
    public event System.Action OnDespawn;

    private void Update()
    {
        objectData += Time.deltaTime;

        if(objectData > 15)
        {
            ObjectPool<PoolableCube>.Instance.DespawnObject(this);
        }
    }

    #region Object Pooling

    public void Despawn()
    {
        Debug.Log($"Object {name} was despawned after {objectData} seconds.");

        OnSpawn?.Invoke();
    }

    public void Spawn()
    {
        Debug.Log($"Spawned {name}");

        objectData = 0.0f;
        OnDespawn?.Invoke();
    }

    #endregion
}
