using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePools : MonoBehaviour
{
    /*public enum PoolType // pool türlerini belirten bir enum
    {
        StackPool, // Stack pool
        QueuePool // Kuyruk pool
    }
    */

    [System.Serializable] // bu sýnýfý Inspector'da görmek için gerekli
    public class Pool // havuz bilgilerini tutan bir sýnýf
    {
        //public PoolType poolType;
        public string tag; // havuzun etiketi
        public List<GameObject> prefabs; // havuzdaki nesnelerin prefab'larýnýn listesi
        public int size; // havuzdaki nesne sayýsý
        public int capacity;// kapasitenin üstünde nesne yaratýlýp poola eklenmesini engelenmesi
    }

    public static CreatePools Instance; // bu sýnýfýn tek bir örneðini tutan deðiþken

    //public List<Pool> pools; // havuz listesi
    private Dictionary<string, ObjectPool<GameObject>> poolDictionary; // stack havuz sözlüðü
    private void Awake()
    {
        // bu sýnýfýn tek bir örneðini oluþtur
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // havuz sözlüðünü boþ olarak baþlat
        poolDictionary = new Dictionary<string, ObjectPool<GameObject>>();
        /* Awake ile baþlangýçta pool oluþturmak için
        // listedeki her havuz için
        foreach (Pool pool in pools)
        {
                // Kurucu fonksiyonu çaðýrarak havuz oluþtur
                // Sýrayla havuza nesne eklemek için
                // ObjectPool nesnesini baþlat
                // prefab listesinden rastgele bir prefab seçerek nesne oluþturan bir fonksiyon ver
                // nesneyi pasif yapan bir fonksiyon ver
                // nesneyi yok eden bir fonksiyon ver
                // kullaným sürelerini tutan sözlük ver
                // nesne kapasitesini belirleyen deðeri ver
                int index = 0; // prefab listesinin baþlangýç indeksi
                ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(() => {
                    GameObject obj = Instantiate(pool.prefabs[index]); // prefab listesinden sýrayla bir prefab al
                    index++; // indeksi arttýr
                    if (index >= pool.prefabs.Count) index = 0; // eðer indeks listenin sonuna ulaþtýysa indeksi sýfýrla
                    return obj; // oluþturulan nesneyi geri döndür
                }, obj => obj.SetActive(false), obj => obj.SetActive(true), obj => Destroy(obj), new Dictionary<GameObject, DateTime>(), pool.capacity);

                // havuz boyutu kadar nesne oluþtur ve havuza ekle
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = objectPool.Get();
                    objectPool.Release(obj); // nesneyi pasif yap ve havuza geri ver
                }
                // havuz sözlüðüne havuzu ekle
                poolDictionary.Add(pool.tag, objectPool);          
        }
        */
    }

    public void CreatePool(string tag, List<GameObject> prefabs, int size, int capacity)
    {
        // ObjectPool nesnesini baþlat
        // prefab listesinden rastgele bir prefab seçerek nesne oluþturan bir fonksiyon ver
        // nesneyi pasif yapan bir fonksiyon ver
        // nesneyi yok eden bir fonksiyon ver
        // kullaným sürelerini tutan sözlük ver
        // nesne kapasitesini belirleyen deðeri ver

        int index = 0; // prefab listesinin baþlangýç indeksi

        ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(() => {
            GameObject obj = Instantiate(prefabs[index]); // prefab listesinden sýrayla bir prefab al
            index++; // indeksi arttýr
            if (index >= prefabs.Count) index = 0; // eðer indeks listenin sonuna ulaþtýysa indeksi sýfýrla
            return obj; // oluþturulan nesneyi geri döndür
        }, obj => obj.SetActive(false), obj => obj.SetActive(true), obj => Destroy(obj), new Dictionary<GameObject, DateTime>(), capacity);

        // havuz boyutu kadar nesne oluþtur ve havuza ekle
        for (int i = 0; i < size; i++)
        {
            GameObject obj = objectPool.Get();
            objectPool.Release(obj); // nesneyi pasif yap ve havuza geri ver
        }
        // havuz sözlüðüne havuzu ekle
        poolDictionary.Add(tag, objectPool);
    }

    // etikete göre havuzdan bir nesne almak için fonksiyon
    public GameObject GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer sözlükte böyle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
            return null;
        }
        return poolDictionary[tag].Get(); // sözlükten etikete göre havuzu bul ve nesneyi al
    }

    // havuzdan bir nesne almak ve verilen deðerlere göre spawn etmek ve aktif etmek
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer havuz sözlüðünde böyle bir anahtar yoksa
        {
            Debug.LogWarning("Havuz sözlüðünde " + tag + " anahtarý bulunamadý."); // uyarý mesajý ver
            return null; // boþ deðer döndür
        }

        GameObject objectToSpawn = poolDictionary[tag].Get(); // havuzdan bir nesne al

        objectToSpawn.transform.position = position; // nesnenin pozisyonunu ayarla
        objectToSpawn.transform.rotation = rotation; // nesnenin rotasyonunu ayarla

        poolDictionary[tag].Activate(objectToSpawn); // nesneyi aktif yap

        return objectToSpawn; // nesneyi geri döndür
    }

    // etikete göre havuza bir nesne geri vermek için fonksiyon
    public void ReturnObject(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer sözlükte böyle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
            return;
        }
        poolDictionary[tag].Release(obj); // sözlükten etikete göre havuzu bul ve nesneyi geri ver
    }
    public void Destroy(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer sözlükte böyle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            poolDictionary[tag].Destroy(obj); // sözlükten etikete göre havuzu bul ve nesneyi al
        }
    }
    public void Clear(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer sözlükte böyle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            poolDictionary[tag].Clear(); // sözlükten etikete göre havuzu bul ve nesneyi al
        }
    }
    public void TrimExcess(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer sözlükte böyle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            poolDictionary[tag].TrimExcess(); // sözlükten etikete göre havuzu bul ve nesneyi al
        }
    }
    public void Dispose(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer sözlükte böyle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            poolDictionary[tag].Dispose(); // sözlükten etikete göre havuzu bul ve nesneyi al
        }
    }

    //fonksiyon, havuzdaki uzun süre kullanýlmayan nesneleri temizlemek için kullanýlýr.Fonksiyonun parametresi olan maxIdleTime,
    //bir nesnenin kullanýlmadan kalabileceði maksimum süreyi belirtir.
    public void CleanUp(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer sözlükte böyle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            // 10 dakikadan fazla kullanýlmayan nesneleri temizle
            poolDictionary[tag].CleanUp(TimeSpan.FromMinutes(10));
        }
    }
    public void CleanUp(string tag, float time)
    {
        if (!poolDictionary.ContainsKey(tag)) // eðer sözlükte böyle bir etiket yoksa hata ver
        {
            Debug.LogError("No pool with tag " + tag + " exists.");
        }
        else
        {
            // 10 dakikadan fazla kullanýlmayan nesneleri temizle
            poolDictionary[tag].CleanUp(TimeSpan.FromMinutes(time));
        }
    }
}