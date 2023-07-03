using UnityEngine;

// Yer �ekimi olmayan durumda ate� etme �eklini belirleyen bir s�n�f tan�mla ve interface s�n�f�n� uygula
public class BulletTargetShooter : TowerShooter
{
    // Hedef nesne 
    //protected Transform target;

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    public BulletTargetShooter(Transform fireTransform, GameObject bulletPrefab) : base(fireTransform, bulletPrefab) {  }
    public BulletTargetShooter(Transform fireTransform, GameObject bulletPrefab, float shotForce) : base(fireTransform, bulletPrefab, shotForce)
    {
        // Burada base ile TowerShooter s�n�f�n�n kurucu fonksiyonunu �a��r�yoruz
        // ve fireTransform, bulletPrefab ve shotForce de�erlerini g�nderiyoruz
        // Bu de�erler TowerShooter s�n�f�n�n de�i�kenlerine atanacak
        this.shotForce = shotForce;
    }


    // Interface s�n�f�ndan gelen metodun g�vdesini yaz
    public override void Shoot()
    {
        if (target == null)
        {
            return;
        }
        
        // FireTransformun y�n�n� ve e�imini hedefe do�ru ayarla
        Aim(fireTransform, target.position);

        // Mermiyi olu�tur
        GameObject bullet = Object.Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);

        // Mermiye kuvvet uygula
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(fireTransform.forward * shotForce, ForceMode.Impulse);
        //rb.velocity = bullet.transform.forward * 10f;
    }

    void Aim(Transform t, Vector3 target)
    {
        // �evirme a��s�n� hesapla
        Vector3 direction = target - t.position;
        direction.y = target.y - fireTransform.position.y; // Bu sat�r� ekledim
        Quaternion rotation = Quaternion.LookRotation(direction);

        // �evirmeyi uygula
        t.rotation = rotation;

        // Ate� noktas�n�n yukar� do�ru e�imini hesapla
        float pitch = Mathf.Atan2(direction.y, direction.magnitude) * Mathf.Rad2Deg;

        // Ate� noktas�n�n rotasyonuna e�imi ekle
        t.rotation *= Quaternion.Euler(-pitch, 0f, 0f);
    }
}