using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage = 10f; // roketin verdi�i hasar
    public float explosionRadius = 5f; // roketin patlama yar��ap�
    public GameObject explosionEffect; // roketin patlama efekti
    public float speed = 20f; // roketin h�z�
    public float smoothTime = 0.5f; // roketin yumu�atma s�resi
    // di�er de�i�kenler ve fonksiyonlar

    private Rigidbody rb; // roketin rigidbody'si
    private Vector3 targetPosition; // roketin hedef pozisyonu
    private Vector3 currentVelocity; // roketin ak�m h�z�

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // rigidbody'yi al
    }

    private void Start()
    {
        targetPosition = transform.position + transform.forward * 1000f; // hedef pozisyonu ileri do�ru belirle
    }

    private void FixedUpdate()
    {
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime, speed); // roketin pozisyonunu yumu�at
        rb.MovePosition(smoothPosition); // roketin pozisyonunu fiziksel olarak de�i�tir
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Rocket" && collision.gameObject.tag != "FirePoint") // e�er �arp��an nesne roket veya ate�leme noktas� de�ilse
        {
            Explode(); // patla
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation); // patlama efektini olu�tur
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); // patlama yar��ap�ndaki t�m colliderlar� al
        foreach (Collider collider in colliders) // her collider i�in
        {
            Enemy enemy = collider.GetComponent<Enemy>(); // d��man var m� kontrol et
            if (enemy != null) // e�er varsa
            {
                enemy.TakeDamage(damage); // d��mana hasar ver
                enemy.KnockBack(transform.position, explosionRadius); // d��man� geri it
            }
        }
        Destroy(gameObject); // kendini yok et
    }
}