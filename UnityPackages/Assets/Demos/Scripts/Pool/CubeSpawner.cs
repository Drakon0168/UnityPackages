using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drakon.ObjectPool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ObjectPool<PoolableCube>.Prefab = prefab;
        ObjectPool<PoolableCube>.Instance.SpawnObject(new Vector3(0,5,0), Quaternion.identity);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            ObjectPool<PoolableCube>.Instance.SpawnObject(new Vector3(Random.value, 0, Random.value) * 25.0f - new Vector3(12.5f, -5.0f, 12.5f), Quaternion.identity);
            timer = 0.0f;
        }
    }
}
