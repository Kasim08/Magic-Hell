using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEditor;
using UnityEngine;

/* NOTE
Bu sizin tercihinize ba�l�. Stack kullan�rsan�z, en son olu�turulan GameObject�i geri al�rs�n�z. 
Bu, GameObject�in daha az bellek kullanmas� anlam�na gelebilir. Queue kullan�rsan�z, en �nce olu�turulan GameObject�i geri al�rs�n�z. 
Bu, GameObject�in daha az CPU zaman� kullanmas� anlam�na gelebilir.
*/

#region ObjectPoolStack
public class ObjectPool<T> // jenerik bir s�n�f
{
    private Func<T> createFunc; // nesne olu�turan bir fonksiyon
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
        pool = new Stack<T>(); // havuzu bo� olarak ba�lat 
        this.usageTimes = new Dictionary<T, DateTime>(); // kullan�m s�relerini bo� olarak ba�lat
    }

    public T Get() // havuzdan bir nesne almak i�in fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            if (pool.Count > 0) // e�er havuzda nesne varsa
            {
                T obj = pool.Pop(); // havuzun en �st�ndeki nesneyi al
                usageTimes[obj] = DateTime.Now; // kullan�m s�resini g�ncelle
                return obj; // nesneyi geri d�nd�r
            }
            else // e�er havuzda nesne yoksa
            {
                T obj = createFunc(); // yeni bir nesne olu�tur
                usageTimes[obj] = DateTime.Now; // kullan�m s�resini g�ncelle
                return obj; // nesneyi geri d�nd�r
            }
        }
    }

    public void Release(T obj) // havuza bir nesne geri vermek i�in fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            if (pool.Count < capacity) // e�er havuz dolu de�ilse
            {
                releaseAction(obj); // nesneyi pasif yap
                pool.Push(obj); // havuzun en �st�ne ekle
            }
            else // e�er havuz doluysa
            {
                destroyAction(obj); // nesneyi yok et
                usageTimes.Remove(obj); // kullan�m s�resini sil
            }
        }
    }

    public void Activate(T obj) // havuzdaki bir nesneyi aktif yapmak i�in fonksiyon // yeni eklendi
    {
        lock (pool)
        {
            activateAction(obj); // nesneyi aktif yap
        }
    }

    public void Destroy(T obj) // havuzdan bir nesneyi yok etmek i�in fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            destroyAction(obj); // nesneyi yok et
            pool.Pop(); // havuzdan ��kar
            usageTimes.Remove(obj); // kullan�m s�resini sil
        }
    }

    public void Dispose() // havuzu komple silmek i�in fonksiyon
    {
        lock (pool)
        {
            while (pool.Count > 0) // havuz bo�alana kadar
            {
                Destroy(pool.Pop()); // havuzdaki her nesneyi yok et ve ��kar
            }
        }
    }

    public void TrimExcess() // havuzdaki bo� alanlar� silmek i�in fonksiyon
    {
        lock (pool)
        {
            pool.TrimExcess(); // havuzu optimize et
        }
    }

    public void Clear() // havuzdaki t�m nesneleri yok etmek i�in fonksiyon
    {
        lock (pool)
        {
            foreach (T obj in pool) // havuzdaki her nesne i�in
            {
                destroyAction(obj); // nesneyi yok et
            }
            pool.Clear(); // havuzu temizle
        }
    }

    public void CleanUp(TimeSpan maxIdleTime) // havuzdaki uzun s�re kullan�lmayan nesneleri temizlemek i�in fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            foreach (var pair in usageTimes) // her kullan�m s�resi i�in 
            {
                if (DateTime.Now - pair.Value > maxIdleTime) // e�er belirli bir s�reden fazla kullan�lmam��sa 
                {
                    Destroy(pair.Key); // nesneyi yok et 
                }
            }
        }
    }
}
#endregion

#region ObjectPoolQueue
public class ObjectPoolQueue<T> // jenerik bir s�n�f
{
    private Func<T> createFunc; // nesne olu�turan bir fonksiyon
    private Action<T> releaseAction; // nesneyi pasif yapan bir fonksiyon
    private Action<T> activateAction; // nesneyi aktif yapan bir fonksiyon
    private Action<T> destroyAction; // nesneyi yok eden bir fonksiyon
    private Dictionary<T, DateTime> usageTimes;
    private int capacity;
    private Queue<T> pool; // havuz // stack yerine queue kullan�ld�


    public ObjectPoolQueue(Func<T> createFunc, Action<T> releaseAction, Action<T> activateAction, Action<T> destroyAction, Dictionary<T, DateTime> usageTimes, int capacity) // kurucu fonksiyon
    {
        this.createFunc = createFunc;
        this.releaseAction = releaseAction;
        this.activateAction = activateAction;
        this.destroyAction = destroyAction;
        this.capacity = capacity;
        pool = new Queue<T>(); // havuzu bo� olarak ba�lat // stack yerine queue kullan�ld�
        usageTimes = new Dictionary<T, DateTime>(); // kullan�m s�relerini bo� olarak ba�lat
    }

    public T Get() // havuzdan bir nesne almak i�in fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            if (pool.Count > 0) // e�er havuzda nesne varsa
            {
                T obj = pool.Dequeue(); // havuzun en �st�ndeki nesneyi al
                usageTimes[obj] = DateTime.Now; // kullan�m s�resini g�ncelle
                return obj; // nesneyi geri d�nd�r
            }
            else // e�er havuzda nesne yoksa
            {
                T obj = createFunc(); // yeni bir nesne olu�tur
                usageTimes[obj] = DateTime.Now; // kullan�m s�resini g�ncelle
                return obj; // nesneyi geri d�nd�r
            }
        }
    }

    public void Release(T obj) // havuza bir nesne geri vermek i�in fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            if (pool.Count < capacity) // e�er havuz dolu de�ilse
            {
                releaseAction(obj); // nesneyi pasif yap
                pool.Enqueue(obj); // havuzun en �st�ne ekle
            }
            else // e�er havuz doluysa
            {
                destroyAction(obj); // nesneyi yok et
                usageTimes.Remove(obj); // kullan�m s�resini sil
            }
        }
    }

    public void Activate(T obj) // havuzdaki bir nesneyi aktif yapmak i�in fonksiyon
    {
        lock (pool)
        {
            activateAction(obj); // nesneyi aktif yap
        }
    }

    public void Destroy(T obj) // havuzdan bir nesneyi yok etmek i�in fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            destroyAction(obj); // nesneyi yok et
            pool.Dequeue(); // havuzdan ��kar
            usageTimes.Remove(obj); // kullan�m s�resini sil
        }
    }

    public void Dispose() // havuzu komple silmek i�in fonksiyon
    {
        lock (pool)
        {
            while (pool.Count > 0) // havuz bo�alana kadar
            {
                Destroy(pool.Dequeue()); // havuzdaki her nesneyi yok et ve ��kar
            }
        }
    }

    public void TrimExcess() // havuzdaki bo� alanlar� silmek i�in fonksiyon
    {
        lock (pool)
        {
            pool.TrimExcess(); // havuzu optimize et
        }
    }

    public void Clear() // havuzdaki t�m nesneleri yok etmek i�in fonksiyon
    {
        lock (pool)
        {
            foreach (T obj in pool) // havuzdaki her nesne i�in
            {
                destroyAction(obj); // nesneyi yok et
            }
            pool.Clear(); // havuzu temizle
        }
    }

    public void CleanUp(TimeSpan maxIdleTime) // havuzdaki uzun s�re kullan�lmayan nesneleri temizlemek i�in fonksiyon
    {
        lock (pool) // havuzu kilitle
        {
            foreach (var pair in usageTimes) // her kullan�m s�resi i�in 
            {
                if (DateTime.Now - pair.Value > maxIdleTime) // e�er belirli bir s�reden fazla kullan�lmam��sa 
                {
                    Destroy(pair.Key); // nesneyi yok et 
                }
            }
        }
    }
}
#endregion