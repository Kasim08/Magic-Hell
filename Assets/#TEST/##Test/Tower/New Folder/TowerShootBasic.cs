using UnityEngine;

public class TowerShootBasic : MonoBehaviour
{
    // Tower ýn ateþ edeceði nokta
    public Transform firePoint;

    // Tower ýn etrafýndaki düþmanlarý arayacaðý yarýçap
    public float radius = 10f;

    // Tower ýn ateþ edeceði mermi nesnesi
    public GameObject bulletPrefab;

    // Tower ýn ateþ etme hýzý (saniyede kaç mermi)
    public float fireRate = 2f;

    // Tower ýn ateþ etme gücü (mermiye uygulanan kuvvet)
    public float firePower = 10f;

    // En yakýn düþman
    private Transform target;

    // Son ateþ etme zamaný
    private float lastFireTime = 0f;

    void Update()
    {
        // Etraftaki tüm düþmanlarý bul
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));

        // En yakýn düþmaný belirle
        float minDistance = Mathf.Infinity;
        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemy.transform;
            }
        }

        // Eðer bir düþman varsa, firePoint nesnesini ona doðru çevir
        if (target != null) 
        {
                aim();

                // Eðer bir düþman varsa ve ateþ etme zamaný geldiyse, ateþ et
                if (target != null && Time.time - lastFireTime > 1f / fireRate) 
                shot();

        }
    }

    public void aim()
    {
            // Çevirme açýsýný hesapla
            Vector3 direction = target.position - firePoint.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Çevirmeyi uygula
            firePoint.rotation = rotation;

            // Ateþ noktasýnýn yukarý doðru eðimini ayarla
            float pitch = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
            firePoint.Rotate(pitch, 0f, 0f);
    }

    public void shot()
    {
            // Mermi nesnesini oluþtur
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Mermiye kuvvet uygula
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * firePower, ForceMode.Impulse);

            // Son ateþ etme zamanýný güncelle
            lastFireTime = Time.time;
    }
}