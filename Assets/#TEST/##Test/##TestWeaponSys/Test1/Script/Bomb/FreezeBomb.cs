using UnityEngine;

public class FreezeBomb : Bomb
{
    public float freezeTime = 5f; // bombanýn dondurma süresi

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation); // patlama efektini oluþtur
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); // patlama yarýçapýndaki tüm colliderlarý al
        foreach (Collider collider in colliders) // her collider için
        {
            Enemy enemy = collider.GetComponent<Enemy>(); // düþman var mý kontrol et
            if (enemy != null) // eðer varsa
            {
                enemy.Freeze(freezeTime); // düþmaný dondur
            }
        }
        Destroy(gameObject); // kendini yok et
    }
}
