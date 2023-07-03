using UnityEngine;

public class StickyBomb : Bomb
{
    public float detonationTime = 3f; // bombanýn patlama süresi

    private float timer; // zamanlayýcý

    private void Start()
    {
        timer = detonationTime; // zamanlayýcýyý baþlat
    }

    private void Update()
    {
        timer -= Time.deltaTime; // zamanlayýcýyý güncelle
        if (timer <= 0) // eðer zamanlayýcý sýfýra ulaþtýysa
        {
            Explode(); // patlama fonksiyonunu çaðýr
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true; // bombanýn fiziksel hareketini durdur
        transform.parent = collision.transform; // bombanýn ebeveynini çarptýðý nesne yap
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
