using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAdvancedShot : MonoBehaviour
{
    #region Values

    // Hedef nesne 
    private Transform target;

    // Mermi nesnesi
    public GameObject bulletPrefab;

    public float shotProjectileForceMultiplier = 1;
    public float shotBulletForceMultiplier = 1;
    public float shotForce = 10;
    // Ateþ etme oraný
    public float fireRate = 1f;

    // Son ateþ etme zamaný
    private float lastFireTime = 0f;

    // Tower ýn etrafýndaki düþmanlarý arayacaðý yarýçap
    public float radius = 10f;

    // Yer çekimi kullanýp kullanmayacaðýný belirleyen bool deðiþkeni
    public bool useGravity = false;
    // Ateþ edeceði bir dýþ transformu varsa bunu belirleyen transform deðiþkeni
    public Transform fireTransform;
    //Dönüþ hýzý
    public float turnSpeed = 1f;
    // Üst gövde veya namlunun dönüp dönmeyeceðini belirleyen bool deðiþkeni
    public bool rotateUpperBody = true;
    // Üst gövde veya namlu transformu
    public Transform upperBody;

    // Kule komple dönüp dönmemesi gerektiðini belirleyen bool deðiþkeni
    public bool rotateTower = false;
    // Kule transformu
    public Transform towerBody;

    // Mermiyi ateþlemek için kullanacaðýmýz açý
    public float fireAngle = 45f;

    #endregion
    
    void Update()
    {
        // Etraftaki tüm düþmanlarý bul
        enemyTarget();
       

        // Eðer bir hedef varsa, mermiyi ateþle
        if (target != null && Time.time - lastFireTime > 1f / fireRate)
        {
            // FireTransformun yönünü ve eðimini hedefe doðru ayarla
            Aim(fireTransform, target.position);

            // Mermiyi oluþtur
            GameObject bullet = Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);

            // Mermiyi hedefe doðru atacak kuvveti hesapla
            Vector3 force;
            if (useGravity)
            {
                force = CalculateProjectileVelocity(target.position);
            }
            else
            {
                force = CalculateBulletVelocity(target.position);
            }

            // Mermiyi hedefe doðru at
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(force, ForceMode.VelocityChange);

            // Son ateþ etme zamanýný güncelle
            lastFireTime = Time.time;

            #region Tower Rotate
            if (upperBody != null && rotateUpperBody)
            {
                // Eðer kule komple dönmesi gerekiyorsa, kulenin yönünü hedefe doðru ayarla
                if (rotateTower && towerBody != null)
                {
                    RotateToTarget(towerBody, target.position);

                    // Eðer üst gövde veya namlu varsa ve dönmesi gerekiyorsa, üst gövde veya namlunun yönünü ve eðimini hedefe doðru ayarla
                    AdjustPitch(upperBody);
                }
                else Aim(upperBody, target.position);
            }
            else if (rotateTower && towerBody != null) RotateToTarget(towerBody, target.position);
            #endregion
        }
    }


    #region EnemiesPhysicsOverlapSphere
    public void enemyTarget()
    {
        // Etraftaki tüm düþmanlarý bul
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));

        // En yakýn düþmaný belirle.
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
    // Mermiye uygulanacak kuvveti ve açýyý hesaplayan bir yöntem (yer çekimi varsa)
    Vector3 CalculateProjectileVelocity(Vector3 target)
    {
        // Mermiyi ateþleyeceðimiz nokta
        Vector3 firePoint = fireTransform.position;

        // Hedef ile ateþ noktasý arasýndaki mesafe
        float distance = Vector3.Distance(firePoint, target);

        // Hedef ile ateþ noktasý arasýndaki yükseklik farký
        float height = target.y - firePoint.y;

        // Mermiyi ateþlemek için gereken baþlangýç hýzýný hesapla
        float velocity = Mathf.Sqrt((distance * Physics.gravity.magnitude) / Mathf.Sin(2 * fireAngle * Mathf.Deg2Rad));

        // Mermiyi ateþlemek için gereken yukarý doðru eðimi hesapla
        float pitch = Mathf.Atan2(height, distance);

        // Mermiyi ateþlemek için gereken yönü hesapla
        Vector3 direction = (target - firePoint).normalized;

        // Mermiyi ateþlemek için gereken kuvveti hesapla
        Vector3 force = velocity * direction;
        force.y += velocity * Mathf.Sin(pitch);

        // Mermiyi ateþlemek için gereken kuvveti döndür
        return force * shotProjectileForceMultiplier;
    }

    // Mermiye uygulanacak kuvveti ve açýyý hesaplayan bir yöntem (yer çekimi yoksa)
    Vector3 CalculateBulletVelocity(Vector3 target)
    {
        // Mermiyi ateþleyeceðimiz nokta
        Vector3 firePoint = fireTransform.position;

        // Hedef ile ateþ noktasý arasýndaki mesafe
        float distance = Vector3.Distance(firePoint, target);

        // Hedef ile ateþ noktasý arasýndaki yükseklik farký
        float height = target.y - firePoint.y;

        // Mermiyi ateþlemek için gereken baþlangýç hýzýný hesapla
        float velocity = distance / fireRate;

        // Mermiyi ateþlemek için gereken yukarý doðru eðimi hesapla
        float pitch = Mathf.Atan2(height, distance);

        // Mermiyi ateþlemek için gereken yönü hesapla
        Vector3 direction = (target - firePoint).normalized;

        // Mermiyi ateþlemek için gereken kuvveti hesapla
        Vector3 force = velocity * direction;
        force.y += velocity * Mathf.Sin(pitch);

        // Mermiyi ateþlemek için gereken kuvveti döndür
        return force*shotBulletForceMultiplier;
    }
    #endregion 

    #region Aim 
    // Bir transformun yönünü ve eðimini hedefe doðru ayarlayan bir yöntem
    void Aim(Transform t, Vector3 target)
    {
        // Çevirme açýsýný hesapla
        Vector3 direction = target - t.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Çevirmeyi uygula
        t.rotation = rotation;

        // Ateþ noktasýnýn yukarý doðru eðimini ayarla
        float pitch = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
        t.Rotate(pitch, 0f, 0f);
    }
    #endregion

    #region TowerRotate
    // Bir transformun yönünü hedefe doðru ayarlayan bir yöntem
    void RotateToTarget(Transform t, Vector3 target)
    {
        // Çevirme açýsýný hesapla
        Vector3 direction = target - t.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Çevirmeyi uygula
        t.rotation = Quaternion.Slerp(t.rotation, rotation, turnSpeed * Time.deltaTime); 
    }
    // Bir transformun yukarý doðru eðimini hedefe doðru ayarlayan bir yöntem
    void AimUpAndDown(Transform t,Vector3 target)
    {
        // Hedefin pozisyonundan taretin pozisyonunu çýkararak niþan alýnacak yönü hesapla
        Vector3 direction = target - t.position;

        // Yönün y ve z bileþenlerinden eðim açýsýný hesapla
        float pitch = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;

        // Taretin ateþ noktasýný eðim açýsý kadar x ekseni etrafýnda döndür
        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, 0f);
        t.rotation = Quaternion.Slerp(t.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    // Bir transformun yukarý doðru eðimini ayarlayan bir yöntem. Girilen açýya göre çalýþýr.
    void AdjustPitch(Transform t)
    {
        // Ateþ noktasýnýn yukarý doðru eðimini ayarla
        float pitch = fireAngle;
        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, 0f); 
        t.rotation = Quaternion.Slerp(t.rotation, targetRotation, turnSpeed * Time.deltaTime); 
    }
    #endregion

}




