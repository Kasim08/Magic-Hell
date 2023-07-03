using UnityEngine;
using System.Collections.Generic; // Liste oluþturmak için bu kütüphaneyi ekle

public class ObjectManager : MonoBehaviour
{
    // Objeleri tutacak liste
    public List<GameObject> objects;

    // Dairenin merkezi
    public Vector3 center;

    // Dairenin yarýçapý
    public float radius;

    // Objeyin dönüþ hýzý
    public float speed;
    public float speedMultiplier;

    void Start()
    {
        // Liste yoksa oluþtur
        if (objects == null) objects = new List<GameObject>();
    }

    // Objeleri daire üzerinde yerleþtiren fonksiyon
    public void DistributeObjects()
    {
        // Objelerin sayýsýný al
        int count = objects.Count;

        // Her obje için daire üzerindeki açýyý hesapla
        float angle = 360f / count;

        // Her obje için döngü
        for (int i = 0; i < count; i++)
        {
            // Objeyi al
            GameObject obj = objects[i];

            // Objeyi daire üzerindeki koordinata yerleþtir
            float x = center.x + radius * Mathf.Cos(angle * i * Mathf.Deg2Rad);
            float z = center.z + radius * Mathf.Sin(angle * i * Mathf.Deg2Rad);
            obj.transform.position = new Vector3(x, center.y, z);

            // Objeyi merkeze doðru bakacak þekilde döndür
            obj.transform.LookAt(center, Vector3.up);
        }
    }

    // Objeyi listeye ekleyen fonksiyon
    public void AddObject(GameObject obj)
    {
        objects.Add(obj);

        // Objeleri yeniden daðýt
        DistributeObjects();
    }

    // Objeyi listeden çýkaran fonksiyon
    public void RemoveObject(GameObject obj)
    {
        objects.Remove(obj);

        // Objeleri yeniden daðýt
        DistributeObjects();
    }
}
