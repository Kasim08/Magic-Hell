using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Objeyi d�nd�recek script

    // Objeyi d�nd�recek merkez
    private Vector3 center;

    // Objeyin d�n�� h�z�
    private float speed;

    // Manager scriptine eri�mek i�in de�i�ken
    private ObjectManager manager;

    void Awake()
    {
        // Manager scriptine eri�
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
        // Objeyi merkezin etraf�nda d�nd�r
        transform.RotateAround(center, Vector3.up, speed * Time.deltaTime * Mathf.Deg2Rad);
    }

}
