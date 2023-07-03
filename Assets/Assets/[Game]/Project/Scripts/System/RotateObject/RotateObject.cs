using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Objeyi döndürecek script

    // Objeyi döndürecek merkez
    private Vector3 center;

    // Objeyin dönüþ hýzý
    private float speed;

    // Manager scriptine eriþmek için deðiþken
    private ObjectManager manager;

    void Awake()
    {
        // Manager scriptine eriþ
        manager = FindObjectOfType<ObjectManager>();

        // Objeyi listeye ekle
        manager.AddObject(gameObject);
        center = manager.center;
        speed = manager.speed * manager.speedMultiplier;
    }

    private void OnDestroy()
    {
        manager.RemoveObject(gameObject);
    }

    void Update()
    {
        // Objeyi merkezin etrafýnda döndür
        transform.RotateAround(center, Vector3.up, speed * Time.deltaTime * Mathf.Deg2Rad);
    }

}
