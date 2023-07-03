using UnityEngine;

public class FreezeBomb : Bomb
{
    public float freezeTime = 5f; // bomban�n dondurma s�resi

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation); // patlama efektini olu�tur
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); // patlama yar��ap�ndaki t�m colliderlar� al
        foreach (Collider collider in colliders) // her collider i�in
        {
            Enemy enemy = collider.GetComponent<Enemy>(); // d��man var m� kontrol et
            if (enemy != null) // e�er varsa
            {
                enemy.Freeze(freezeTime); // d��man� dondur
            }
        }
        Destroy(gameObject); // kendini yok et
    }
}
