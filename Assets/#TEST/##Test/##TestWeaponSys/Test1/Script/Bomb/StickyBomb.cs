using UnityEngine;

public class StickyBomb : Bomb
{
    public float detonationTime = 3f; // bomban�n patlama s�resi

    private float timer; // zamanlay�c�

    private void Start()
    {
        timer = detonationTime; // zamanlay�c�y� ba�lat
    }

    private void Update()
    {
        timer -= Time.deltaTime; // zamanlay�c�y� g�ncelle
        if (timer <= 0) // e�er zamanlay�c� s�f�ra ula�t�ysa
        {
            Explode(); // patlama fonksiyonunu �a��r
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true; // bomban�n fiziksel hareketini durdur
        transform.parent = collision.transform; // bomban�n ebeveynini �arpt��� nesne yap
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
