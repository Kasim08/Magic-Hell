using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLockShot : MonoBehaviour
{
    #region Values

    // Hedef nesne 
    private Transform target;

    // Mermi nesnesi
    public GameObject bulletPrefab;

    public float shotProjectileForceMultiplier = 1;
    public float shotBulletForceMultiplier = 1;
    public float shotForce = 10;
    // Ate� etme oran�
    public float fireRate = 1f;

    // Son ate� etme zaman�
    private float lastFireTime = 0f;

    // Tower �n etraf�ndaki d��manlar� arayaca�� yar��ap
    public float radius = 10f;

    // Yer �ekimi kullan�p kullanmayaca��n� belirleyen bool de�i�keni
    public bool useGravity = false;
    // Ate� edece�i bir d�� transformu varsa bunu belirleyen transform de�i�keni
    public Transform fireTransform;

    // Mermiyi ate�lemek i�in kullanaca��m�z a��
    public float fireAngle = 45f;

    #endregion

    void Update()
    {
        // Etraftaki t�m d��manlar� bul
        enemyTarget();


        // E�er bir hedef varsa, mermiyi ate�le
        if (target != null && Time.time - lastFireTime > 1f / fireRate)
        {

            // Mermiyi olu�tur
            GameObject bullet = Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);

            // Mermiyi hedefe do�ru atacak kuvveti hesapla
            Vector3 force;
            if (useGravity)
            {
                force = CalculateProjectileVelocity(target.position);
                // Mermiyi hedefe do�ru at
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(force, ForceMode.VelocityChange);
            }
            else
            {
                // FireTransformun y�n�n� ve e�imini hedefe do�ru ayarla
                Aim(fireTransform, target.position);
                // Mermiye kuvvet uygula
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(fireTransform.forward * shotForce, ForceMode.Impulse);
            }

            // Son ate� etme zaman�n� g�ncelle
            lastFireTime = Time.time;
        }
    }

    #region EnemiesPhysicsOverlapSphere
    public void enemyTarget()
    {
        // Etraftaki t�m d��manlar� bul
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));

        // En yak�n d��man� belirle.
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
    }
    #endregion

    #region Calculate Fire Force
    // Mermiye uygulanacak kuvveti ve a��y� hesaplayan bir y�ntem (yer �ekimi varsa)
    Vector3 CalculateProjectileVelocity(Vector3 target)
    {
        // Mermiyi ate�leyece�imiz nokta
        Vector3 firePoint = fireTransform.position;

        // Hedef ile ate� noktas� aras�ndaki mesafe
        float distance = Vector3.Distance(firePoint, target);

        // Hedef ile ate� noktas� aras�ndaki y�kseklik fark�
        float height = target.y - firePoint.y;

        // Mermiyi ate�lemek i�in gereken ba�lang�� h�z�n� hesapla
        float velocity = Mathf.Sqrt((distance * Physics.gravity.magnitude) / Mathf.Sin(2 * fireAngle * Mathf.Deg2Rad));

        // Mermiyi ate�lemek i�in gereken yukar� do�ru e�imi hesapla
        float pitch = Mathf.Atan2(height, distance);

        // Mermiyi ate�lemek i�in gereken y�n� hesapla
        Vector3 direction = (target - firePoint).normalized;

        // Mermiyi ate�lemek i�in gereken kuvveti hesapla
        Vector3 force = velocity * direction;
        force.y += velocity * Mathf.Sin(pitch);

        // Mermiyi ate�lemek i�in gereken kuvveti d�nd�r
        return force * shotProjectileForceMultiplier;
    }
    #endregion 

    #region Aim 
    // Bir transformun y�n�n� ve e�imini hedefe do�ru ayarlayan bir y�ntem
    void Aim(Transform t, Vector3 target)
    {
        // �evirme a��s�n� hesapla
        Vector3 direction = target - t.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // �evirmeyi uygula
        t.rotation = rotation;

        // Ate� noktas�n�n yukar� do�ru e�imini ayarla
        float pitch = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
        t.Rotate(pitch, 0f, 0f);
    }
    #endregion
}
