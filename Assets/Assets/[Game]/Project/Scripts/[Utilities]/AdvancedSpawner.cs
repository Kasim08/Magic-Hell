using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
public class AdvancedSpawner : MonoBehaviour
{
    public bool activespawn;
    public enum PoolType // pool türlerini belirten bir enum
    {
        QueuePool, // kuyruk pool
        StackPool // jenerik pool
    }

    public PoolType poolType; // seçilen pool türü
    public float spawnTime = 2.5f; // nesne oluþturma süresi
    public List<Transform> waypointList; // olasý konumlarýn listesi
    public bool inChild; // konumlarýn bu objenin altýnda mý yoksa baþka bir objenin altýnda mý olduðunu belirten deðiþken
    public string poolTag; // havuzun etiketi
    private IEnumerator Spawn()
    { 
        while (true)
        {
            GameObject obj = null; // havuzdan alýnacak nesne
            switch (poolType) // seçilen pool türüne göre
            {
                case PoolType.QueuePool: // eðer kuyruk pool ise
                    obj = CreatePoolsQueue.Instance.GetObject(poolTag); // kuyruk pool'dan bir nesne al
                    obj.SetActive(true); // nesneyi aktif yap
                    break;
                case PoolType.StackPool: // eðer yýðýn pool ise
                    obj = CreatePools.Instance.GetObject(poolTag); // jenerik pool'dan bir nesne al
                    break;
            }
            // eðer null deðilse
            if (obj != null)
            {
                // rastgele bir konuma taþý
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