using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePools : MonoBehaviour
{
    /*public enum PoolType // pool t�rlerini belirten bir enum
    {
        StackPool, // Stack pool
        QueuePool // Kuyruk pool
    }
    */

    [System.Serializable] // bu s�n�f� Inspector'da g�rmek i�in gerekli
    public class Pool // havuz bilgilerini tutan bir s�n�f
    {
        //public PoolType poolType;
        public string tag; // havuzun etiketi
        public List<GameObject> prefabs; // havuzdaki nesnelerin prefab'lar�n�n listesi
        public int size; // havuzdaki nesne say�s�
        public int capacity;// kapasitenin �st�nde nesne yarat�l�p poola eklenmesini engelenmesi
    }

    public static CreatePools Instance; // bu s�n�f�n tek bir �rne�ini tutan de�i�ken

    //public List<Pool> pools; // havuz listesi
    private Dictionary<string, ObjectPool<GameObject>> poolDictionary; // stack havuz s�zl���
    private void Awake()
    {
        // bu s�n�f�n tek bir �rne�ini olu�tur
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // havuz s�zl���n� bo� olarak ba�lat
        poolDictionary = new Dictionary<string, ObjectPool<GameObject>>();
        /* Awake ile ba�lang��ta pool olu�turmak i�in
        // listedeki her havuz i�in
        foreach (Pool pool in pools)
        {
                // Kurucu fonksiyonu �a��rarak havuz olu�tur
                // S�rayla havuza nesne eklemek i�in
                // ObjectPool nesnesini ba�lat
                // prefab listesinden rastgele bir prefab se�erek nesne olu�turan bir fonksiyon ver
                // nesneyi pasif yapan bir fonksiyon ver
                // nesneyi yok eden bir fonksiyon ver
                // kullan�m s�relerini tutan s�zl�k ver
                // nesne kapasitesini belirleyen de�eri ver
                int index = 0; // prefab listesinin ba�lang�� indeksi
                ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(() => {
                    GameObject obj = Instantiate(pool.prefabs[index]); // prefab listesinden s�rayla bir prefab al
                    index++; // indeksi artt�r
                    if (index >= pool.prefabs.Count) index = 0; // e�er indeks listenin sonuna ula�t�ysa indeksi s�f�rla
                    return obj; // olu�turulan nesneyi geri d�nd�r
                }, obj => obj.SetActive(false), obj => obj.SetActive(true), obj => Destroy(obj), new Dictionary<GameObject, DateTime>(), pool.capacity);

                // havuz boyutu kadar nesne olu�tur ve havuza ekle
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = objectPool.Get();
                    objectPool.Release(obj); // nesneyi pasif yap ve havuza geri ver
                }
                // havuz s�zl���ne havuzu ekle
                poolDictionary.Add(pool.tag, objectPool);          
        }
        */
    }

    public void CreatePool(string tag, List<GameObject> prefabs, int size, int capacity)
    {
        // ObjectPool nesnesini ba�lat
        // prefab listesinden rastgele bir prefab se�erek nesne olu�turan bir fonksiyon ver
        // nesneyi pasif yapan bir fonksiyon ver
        // nesneyi yok eden bir fonksiyon ver
        // kullan�m s�relerini tutan s�zl�k ver
        // nesne kapasitesini belirleyen de�eri ver

        int index = 0; // prefab listesinin ba�lang�� indeksi

        ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(() => {
            GameObject obj = Instantiate(prefabs[index]); // prefab listesinden s�rayla bir prefab al
            index++; // indeksi artt�r
            if (index >= prefabs.Count) index = 0; // e�er indeks listenin sonuna ula�t�ysa indeksi s�f�rla
            return obj; // olu�turulan nesneyi geri d�nd�r
        }, obj => obj.SetActive(false), obj => obj.SetActive(true), obj => Destroy(obj), new Dictionary<GameObject, DateTime>(), capacity);

        // havuz boyutu kadar nesne olu�tur ve havuza ekle
        for (int i = 0; i < size; i++)
        {
            GameObject obj = objectPool.Get();
            objectPool.Release(obj); // nesneyi pasif yap ve havuza geri ver
        }
        // havuz s�zl���ne havuzu ekle
        poolDictionary.Add(tag, objectPool);
    }

    // etikete g�re havuzdan bir nesne almak i�in fonksiyon
    public GameObject GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er s�zl�kte b�yle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
            return null;
        }
        return poolDictionary[tag].Get(); // s�zl�kten etikete g�re havuzu bul ve nesneyi al
    }

    // havuzdan bir nesne almak ve verilen de�erlere g�re spawn etmek ve aktif etmek
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er havuz s�zl���nde b�yle bir anahtar yoksa
        {
            Debug.LogWarning("Havuz s�zl���nde " + tag + " anahtar� bulunamad�."); // uyar� mesaj� ver
            return null; // bo� de�er d�nd�r
        }

        GameObject objectToSpawn = poolDictionary[tag].Get(); // havuzdan bir nesne al

        objectToSpawn.transform.position = position; // nesnenin pozisyonunu ayarla
        objectToSpawn.transform.rotation = rotation; // nesnenin rotasyonunu ayarla

        poolDictionary[tag].Activate(objectToSpawn); // nesneyi aktif yap

        return objectToSpawn; // nesneyi geri d�nd�r
    }

    // etikete g�re havuza bir nesne geri vermek i�in fonksiyon
    public void ReturnObject(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er s�zl�kte b�yle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
            return;
        }
        poolDictionary[tag].Release(obj); // s�zl�kten etikete g�re havuzu bul ve nesneyi geri ver
    }
    public void Destroy(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er s�zl�kte b�yle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            poolDictionary[tag].Destroy(obj); // s�zl�kten etikete g�re havuzu bul ve nesneyi al
        }
    }
    public void Clear(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er s�zl�kte b�yle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            poolDictionary[tag].Clear(); // s�zl�kten etikete g�re havuzu bul ve nesneyi al
        }
    }
    public void TrimExcess(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er s�zl�kte b�yle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            poolDictionary[tag].TrimExcess(); // s�zl�kten etikete g�re havuzu bul ve nesneyi al
        }
    }
    public void Dispose(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er s�zl�kte b�yle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            poolDictionary[tag].Dispose(); // s�zl�kten etikete g�re havuzu bul ve nesneyi al
        }
    }

    //fonksiyon, havuzdaki uzun s�re kullan�lmayan nesneleri temizlemek i�in kullan�l�r.Fonksiyonun parametresi olan maxIdleTime,
    //bir nesnenin kullan�lmadan kalabilece�i maksimum s�reyi belirtir.
    public void CleanUp(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er s�zl�kte b�yle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            // 10 dakikadan fazla kullan�lmayan nesneleri temizle
            poolDictionary[tag].CleanUp(TimeSpan.FromMinutes(10));
        }
    }
    public void CleanUp(string tag, float time)
    {
        if (!poolDictionary.ContainsKey(tag)) // e�er s�zl�kte b�yle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            // 10 dakikadan fazla kullan�lmayan nesneleri temizle
            poolDictionary[tag].CleanUp(TimeSpan.FromMinutes(time));
        }
    }
}