using UnityEngine;
using System.Collections.Generic; // Liste olu�turmak i�in bu k�t�phaneyi ekle

public class ObjectManager : MonoBehaviour
{
    // Objeleri tutacak liste
    public List<GameObject> objects;

    // Dairenin merkezi
    public Vector3 center;

    // Dairenin yar��ap�
    public float radius;

    // Objeyin d�n�� h�z�
    public float speed;
    public float speedMultiplier;

    void Start()
    {
        // Liste yoksa olu�tur
        if (objects == null) objects = new List<GameObject>();
    }

    // Objeleri daire �zerinde yerle�tiren fonksiyon
    public void DistributeObjects()
    {
        // Objelerin say�s�n� al
        int count = objects.Count;

        // Her obje i�in daire �zerindeki a��y� hesapla
        float angle = 360f / count;

        // Her obje i�in d�ng�
        for (int i = 0; i < count; i++)
        {
            // Objeyi al
            GameObject obj = objects[i];

            // Objeyi daire �zerindeki koordinata yerle�tir
            float x = center.x + radius * Mathf.Cos(angle * i * Mathf.Deg2Rad);
            float z = center.z + radius * Mathf.Sin(angle * i * Mathf.Deg2Rad);
            obj.transform.position = new Vector3(x, center.y, z);

            // Objeyi merkeze do�ru bakacak �ekilde d�nd�r
            obj.transform.LookAt(center, Vector3.up);
        }
    }

    // Objeyi listeye ekleyen fonksiyon
    public void AddObject(GameObject obj)
    {
        objects.Add(obj);

        // Objeleri yeniden da��t
        DistributeObjects();
    }

    // Objeyi listeden ��karan fonksiyon
    public void RemoveObject(GameObject obj)
    {
        objects.Remove(obj);

        // Objeleri yeniden da��t
        DistributeObjects();
    }
}
