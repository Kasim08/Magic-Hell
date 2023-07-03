using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
public class AdvancedSpawner : MonoBehaviour
{
    public bool activespawn;
    public enum PoolType // pool t�rlerini belirten bir enum
    {
        QueuePool, // kuyruk pool
        StackPool // jenerik pool
    }

    public PoolType poolType; // se�ilen pool t�r�
    public float spawnTime = 2.5f; // nesne olu�turma s�resi
    public List<Transform> waypointList; // olas� konumlar�n listesi
    public bool inChild; // konumlar�n bu objenin alt�nda m� yoksa ba�ka bir objenin alt�nda m� oldu�unu belirten de�i�ken
    public string poolTag; // havuzun etiketi
    private IEnumerator Spawn()
    { 
        while (true)
        {
            GameObject obj = null; // havuzdan al�nacak nesne
            switch (poolType) // se�ilen pool t�r�ne g�re
            {
                case PoolType.QueuePool: // e�er kuyruk pool ise
                    obj = CreatePoolsQueue.Instance.GetObject(poolTag); // kuyruk pool'dan bir nesne al
                    obj.SetActive(true); // nesneyi aktif yap
                    break;
                case PoolType.StackPool: // e�er y���n pool ise
                    obj = CreatePools.Instance.GetObject(poolTag); // jenerik pool'dan bir nesne al
                    break;
            }
            // e�er null de�ilse
            if (obj != null)
            {
                // rastgele bir konuma ta��
                obj.transform.position = Utils.GetRandomItem(waypointList).transform.position;  
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    [ContextMenu("Update Waypoint List")]
    public void getList()
    {
        waypointList.Clear();
        waypointList = Utils.GetChildren<Transform>(this.gameObject, inChild);
    }

    private void Awake()
    {
        getList();
    }
    private void Start()
    {
        if (activespawn) { StartCoroutine(Spawn()); }
    }
    public void startSpawn()
    {
        StartCoroutine(Spawn());
    }
    public void stopSpawn()
    {
        StopCoroutine(Spawn());
    }
}