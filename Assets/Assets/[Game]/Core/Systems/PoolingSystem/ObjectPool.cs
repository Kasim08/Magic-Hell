using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEditor;
using UnityEngine;

/* NOTE
Bu sizin tercihinize baðlý. Stack kullanýrsanýz, en son oluþturulan GameObject’i geri alýrsýnýz. 
Bu, GameObject’in daha az bellek kullanmasý anlamýna gelebilir. Queue kullanýrsanýz, en önce oluþturulan GameObject’i geri alýrsýnýz. 
Bu, GameObject’in daha az CPU zamaný kullanmasý anlamýna gelebilir.
*/

#region ObjectPoolStack
public class ObjectPool<T> // jenerik bir sýnýf
{
    private Func<T> createFunc; // nesne oluþturan bir fonksiyon
    private Action<T> releaseAction; // nesneyi pasif yapan bir fonksiyon
    private Action<T> activateAction; // nesneyi aktif yapan bir fonksiyon
    private Action<T> destroyAction; // nesneyi yok eden bir fonksiyon
    private Dictionary<T, DateTime> usageTimes;
    private int capacity;
    private Stack<T> pool; // havuz


    public ObjectPool(Func<T> createFunc, Action<T> releaseAction, Action<T> activateAction, Action<T> destroyAction, Dictionary<T, DateTime> usageTimes, int capacity) // kurucu fonksiyon
    {
        this.createFunc = createFunc;
        this.releaseAction = releaseAction;
        this.activateAction = activateAction;
        this.destroyAction = destroyAction;
        this.capacity = capacity;
        pool = new Stack<T>(); // havuzu boþ olarak baþlat 
        this.usageTimes = new Dictionary<T, DateTime>(); // kullaným sürelerini boþ olarak baþlat
    }

    public T Get() // havuzdan bir nesne almak için fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            if (pool.Count > 0) // eðer havuzda nesne varsa
            {
                T obj = pool.Pop(); // havuzun en üstündeki nesneyi al
                usageTimes[obj] = DateTime.Now; // kullaným süresini güncelle
                return obj; // nesneyi geri döndür
            }
            else // eðer havuzda nesne yoksa
            {
                T obj = createFunc(); // yeni bir nesne oluþtur
                usageTimes[obj] = DateTime.Now; // kullaným süresini güncelle
                return obj; // nesneyi geri döndür
            }
        }
    }

    public void Release(T obj) // havuza bir nesne geri vermek için fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            if (pool.Count < capacity) // eðer havuz dolu deðilse
            {
                releaseAction(obj); // nesneyi pasif yap
                pool.Push(obj); // havuzun en üstüne ekle
            }
            else // eðer havuz doluysa
            {
                destroyAction(obj); // nesneyi yok et
                usageTimes.Remove(obj); // kullaným süresini sil
            }
        }
    }

    public void Activate(T obj) // havuzdaki bir nesneyi aktif yapmak için fonksiyon // yeni eklendi
    {
        lock (pool)
        {
            activateAction(obj); // nesneyi aktif yap
        }
    }

    public void Destroy(T obj) // havuzdan bir nesneyi yok etmek için fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            destroyAction(obj); // nesneyi yok et
            pool.Pop(); // havuzdan çýkar
            usageTimes.Remove(obj); // kullaným süresini sil
        }
    }

    public void Dispose() // havuzu komple silmek için fonksiyon
    {
        lock (pool)
        {
            while (pool.Count > 0) // havuz boþalana kadar
            {
                Destroy(pool.Pop()); // havuzdaki her nesneyi yok et ve çýkar
            }
        }
    }

    public void TrimExcess() // havuzdaki boþ alanlarý silmek için fonksiyon
    {
        lock (pool)
        {
            pool.TrimExcess(); // havuzu optimize et
        }
    }

    public void Clear() // havuzdaki tüm nesneleri yok etmek için fonksiyon
    {
        lock (pool)
        {
            foreach (T obj in pool) // havuzdaki her nesne için
            {
                destroyAction(obj); // nesneyi yok et
            }
            pool.Clear(); // havuzu temizle
        }
    }

    public void CleanUp(TimeSpan maxIdleTime) // havuzdaki uzun süre kullanýlmayan nesneleri temizlemek için fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            foreach (var pair in usageTimes) // her kullaným süresi için 
            {
                if (DateTime.Now - pair.Value > maxIdleTime) // eðer belirli bir süreden fazla kullanýlmamýþsa 
                {
                    Destroy(pair.Key); // nesneyi yok et 
                }
            }
        }
    }
}
#endregion

#region ObjectPoolQueue
public class ObjectPoolQueue<T> // jenerik bir sýnýf
{
    private Func<T> createFunc; // nesne oluþturan bir fonksiyon
    private Action<T> releaseAction; // nesneyi pasif yapan bir fonksiyon
    private Action<T> activateAction; // nesneyi aktif yapan bir fonksiyon
    private Action<T> destroyAction; // nesneyi yok eden bir fonksiyon
    private Dictionary<T, DateTime> usageTimes;
    private int capacity;
    private Queue<T> pool; // havuz // stack yerine queue kullanýldý


    public ObjectPoolQueue(Func<T> createFunc, Action<T> releaseAction, Action<T> activateAction, Action<T> destroyAction, Dictionary<T, DateTime> usageTimes, int capacity) // kurucu fonksiyon
    {
        this.createFunc = createFunc;
        this.releaseAction = releaseAction;
        this.activateAction = activateAction;
        this.destroyAction = destroyAction;
        this.capacity = capacity;
        pool = new Queue<T>(); // havuzu boþ olarak baþlat // stack yerine queue kullanýldý
        usageTimes = new Dictionary<T, DateTime>(); // kullaným sürelerini boþ olarak baþlat
    }

    public T Get() // havuzdan bir nesne almak için fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            if (pool.Count > 0) // eðer havuzda nesne varsa
            {
                T obj = pool.Dequeue(); // havuzun en üstündeki nesneyi al
                usageTimes[obj] = DateTime.Now; // kullaným süresini güncelle
                return obj; // nesneyi geri döndür
            }
            else // eðer havuzda nesne yoksa
            {
                T obj = createFunc(); // yeni bir nesne oluþtur
                usageTimes[obj] = DateTime.Now; // kullaným süresini güncelle
                return obj; // nesneyi geri döndür
            }
        }
    }

    public void Release(T obj) // havuza bir nesne geri vermek için fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            if (pool.Count < capacity) // eðer havuz dolu deðilse
            {
                releaseAction(obj); // nesneyi pasif yap
                pool.Enqueue(obj); // havuzun en üstüne ekle
            }
            else // eðer havuz doluysa
            {
                destroyAction(obj); // nesneyi yok et
                usageTimes.Remove(obj); // kullaným süresini sil
            }
        }
    }

    public void Activate(T obj) // havuzdaki bir nesneyi aktif yapmak için fonksiyon
    {
        lock (pool)
        {
            activateAction(obj); // nesneyi aktif yap
        }
    }

    public void Destroy(T obj) // havuzdan bir nesneyi yok etmek için fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            destroyAction(obj); // nesneyi yok et
            pool.Dequeue(); // havuzdan çýkar
            usageTimes.Remove(obj); // kullaným süresini sil
        }
    }

    public void Dispose() // havuzu komple silmek için fonksiyon
    {
        lock (pool)
        {
            while (pool.Count > 0) // havuz boþalana kadar
            {
                Destroy(pool.Dequeue()); // havuzdaki her nesneyi yok et ve çýkar
            }
        }
    }

    public void TrimExcess() // havuzdaki boþ alanlarý silmek için fonksiyon
    {
        lock (pool)
        {
            pool.TrimExcess(); // havuzu optimize et
        }
    }

    public void Clear() // havuzdaki tüm nesneleri yok etmek için fonksiyon
    {
        lock (pool)
        {
            foreach (T obj in pool) // havuzdaki her nesne için
            {
                destroyAction(obj); // nesneyi yok et
            }
            pool.Clear(); // havuzu temizle
        }
    }

    public void CleanUp(TimeSpan maxIdleTime) // havuzdaki uzun süre kullanýlmayan nesneleri temizlemek için fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            foreach (var pair in usageTimes) // her kullaným süresi için 
            {
                if (DateTime.Now - pair.Value > maxIdleTime) // eðer belirli bir süreden fazla kullanýlmamýþsa 
                {
                    Destroy(pair.Key); // nesneyi yok et 
                }
            }
        }
    }
}
#endregion