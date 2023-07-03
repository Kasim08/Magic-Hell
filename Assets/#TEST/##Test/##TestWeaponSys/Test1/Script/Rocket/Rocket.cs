using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage = 10f; // roketin verdiði hasar
    public float explosionRadius = 5f; // roketin patlama yarýçapý
    public GameObject explosionEffect; // roketin patlama efekti
    public float speed = 20f; // roketin hýzý
    public float smoothTime = 0.5f; // roketin yumuþatma süresi
    // diðer deðiþkenler ve fonksiyonlar

    private Rigidbody rb; // roketin rigidbody'si
    private Vector3 targetPosition; // roketin hedef pozisyonu
    private Vector3 currentVelocity; // roketin akým hýzý

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // rigidbody'yi al
    }

    private void Start()
    {
        targetPosition = transform.position + transform.forward * 1000f; // hedef pozisyonu ileri doðru belirle
    }

    private void FixedUpdate()
    {
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime, speed); // roketin pozisyonunu yumuþat
        rb.MovePosition(smoothPosition); // roketin pozisyonunu fiziksel olarak deðiþtir
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Rocket" && collision.gameObject.tag != "FirePoint") // eðer çarpýþan nesne roket veya ateþleme noktasý deðilse
        {
            Explode(); // patla
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation); // patlama efektini oluþtur
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); // patlama yarýçapýndaki tüm colliderlarý al
        foreach (Collider collider in colliders) // her collider için
        {
            Enemy enemy = collider.GetComponent<Enemy>(); // düþman var mý kontrol et
            if (enemy != null) // eðer varsa
            {
                enemy.TakeDamage(damage); // düþmana hasar ver
                enemy.KnockBack(transform.position, explosionRadius); // düþmaný geri it
            }
        }
        Destroy(gameObject); // kendini yok et
    }
}