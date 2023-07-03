using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Pools : MonoBehaviour
{
    #region ListPoolUI
    public List<CreatePools.Pool> _pools;

    public List<CreatePoolsQueue.Pool> _poolsQ;
    #endregion
    //buradaki hata public List<CreatePoolsQueue.Pool> _pools; 

    #region CreatePool
    [ContextMenu("Create Pool")]
    public void CreatePool()
    {
        if (_pools.Count > 0)
        {
            foreach (var pool in _pools)
            {
                CreatePools.Instance.CreatePool(pool.tag,pool.prefabs,pool.size,pool.capacity);
                
            }
        }
        if (_poolsQ.Count > 0)
        {
            foreach(var pool in _poolsQ)
            {
                CreatePoolsQueue.Instance.CreatePool(pool.tag, pool.prefabs, pool.size, pool.capacity);
            }
        }
    }
    #endregion

    private void Start()
    {
        CreatePool();
    }
}