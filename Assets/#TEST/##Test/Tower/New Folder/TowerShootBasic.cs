using UnityEngine;

public class TowerShootBasic : MonoBehaviour
{
    // Tower �n ate� edece�i nokta
    public Transform firePoint;

    // Tower �n etraf�ndaki d��manlar� arayaca�� yar��ap
    public float radius = 10f;

    // Tower �n ate� edece�i mermi nesnesi
    public GameObject bulletPrefab;

    // Tower �n ate� etme h�z� (saniyede ka� mermi)
    public float fireRate = 2f;

    // Tower �n ate� etme g�c� (mermiye uygulanan kuvvet)
    public float firePower = 10f;

    // En yak�n d��man
    private Transform target;

    // Son ate� etme zaman�
    private float lastFireTime = 0f;

    void Update()
    {
        // Etraftaki t�m d��manlar� bul
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));

        // En yak�n d��man� belirle
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

        // E�er bir d��man varsa, firePoint nesnesini ona do�ru �evir
        if (target != null) 
        {
                aim();

                // E�er bir d��man varsa ve ate� etme zaman� geldiyse, ate� et
                if (target != null && Time.time - lastFireTime > 1f / fireRate) 
                shot();

        }
    }

    public void aim()
    {
            // �evirme a��s�n� hesapla
            Vector3 direction = target.position - firePoint.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            // �evirmeyi uygula
            firePoint.rotation = rotation;

            // Ate� noktas�n�n yukar� do�ru e�imini ayarla
            float pitch = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
            firePoint.Rotate(pitch, 0f, 0f);
    }

    public void shot()
    {
            // Mermi nesnesini olu�tur
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Mermiye kuvvet uygula
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * firePower, ForceMode.Impulse);

            // Son ate� etme zaman�n� g�ncelle
            lastFireTime = Time.time;
    }
}