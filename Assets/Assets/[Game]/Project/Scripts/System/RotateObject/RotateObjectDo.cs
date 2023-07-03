using UnityEngine;
using DG.Tweening;

public class RotateObjectDo : MonoBehaviour
{
    // Objeyi döndürecek script

    // Objeyi döndürecek merkez
    public Vector3 center;

    // Objeyin dönüþ süresi
    public float duration;

    // Objeyin dönüþ hýzý
    private float speed;

    // Dairenin yarýçapý
    private float radius;

    // Nesnenin yerel konumunu sinüs ve kosinüs ile hesapla
    float angle = 0f; // Baþlangýç açýsý

    // Manager scriptine eriþmek için deðiþken
    private ObjectManager manager;

    void Awake()
    {
        // Manager scriptine eriþ
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
        angle += speed * Time.deltaTime; // Açýyý hýzla arttýr
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius; // x koordinatýný hesapla
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius; // y koordinatýný hesapla
        Vector3 position = new Vector3(x, y, 0); // Yerel konumu oluþtur
        transform.DOLocalMove(center + position, 0.1f); // Nesneyi yerel konuma doðru hareket ettir
    }
}
