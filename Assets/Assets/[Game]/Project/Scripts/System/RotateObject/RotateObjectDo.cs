using UnityEngine;
using DG.Tweening;

public class RotateObjectDo : MonoBehaviour
{
    // Objeyi d�nd�recek script

    // Objeyi d�nd�recek merkez
    public Vector3 center;

    // Objeyin d�n�� s�resi
    public float duration;

    // Objeyin d�n�� h�z�
    private float speed;

    // Dairenin yar��ap�
    private float radius;

    // Nesnenin yerel konumunu sin�s ve kosin�s ile hesapla
    float angle = 0f; // Ba�lang�� a��s�

    // Manager scriptine eri�mek i�in de�i�ken
    private ObjectManager manager;

    void Awake()
    {
        // Manager scriptine eri�
        manager = FindObjectOfType<ObjectManager>();

        // Objeyi listeye ekle
        manager.AddObject(gameObject);
        speed = manager.speed * manager.speedMultiplier;
        radius = manager.radius;
    }
    private void OnDestroy()
    {
        manager.RemoveObject(gameObject);
    }

    void Update()
    {
        angle += speed * Time.deltaTime; // A��y� h�zla artt�r
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius; // x koordinat�n� hesapla
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius; // y koordinat�n� hesapla
        Vector3 position = new Vector3(x, y, 0); // Yerel konumu olu�tur
        transform.DOLocalMove(center + position, 0.1f); // Nesneyi yerel konuma do�ru hareket ettir
    }
}
