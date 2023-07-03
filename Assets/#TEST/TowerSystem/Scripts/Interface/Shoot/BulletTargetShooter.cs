using UnityEngine;

// Yer çekimi olmayan durumda ateþ etme þeklini belirleyen bir sýnýf tanýmla ve interface sýnýfýný uygula
public class BulletTargetShooter : TowerShooter
{
    // Hedef nesne 
    //protected Transform target;

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    public BulletTargetShooter(Transform fireTransform, GameObject bulletPrefab) : base(fireTransform, bulletPrefab) {  }
    public BulletTargetShooter(Transform fireTransform, GameObject bulletPrefab, float shotForce) : base(fireTransform, bulletPrefab, shotForce)
    {
        // Burada base ile TowerShooter sýnýfýnýn kurucu fonksiyonunu çaðýrýyoruz
        // ve fireTransform, bulletPrefab ve shotForce deðerlerini gönderiyoruz
        // Bu deðerler TowerShooter sýnýfýnýn deðiþkenlerine atanacak
        this.shotForce = shotForce;
    }


    // Interface sýnýfýndan gelen metodun gövdesini yaz
    public override void Shoot()
    {
        if (target == null)
        {
            return;
        }
        
        // FireTransformun yönünü ve eðimini hedefe doðru ayarla
        Aim(fireTransform, target.position);

        // Mermiyi oluþtur
        GameObject bullet = Object.Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);

        // Mermiye kuvvet uygula
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(fireTransform.forward * shotForce, ForceMode.Impulse);
        //rb.velocity = bullet.transform.forward * 10f;
    }

    void Aim(Transform t, Vector3 target)
    {
        // Çevirme açýsýný hesapla
        Vector3 direction = target - t.position;
        direction.y = target.y - fireTransform.position.y; // Bu satýrý ekledim
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Çevirmeyi uygula
        t.rotation = rotation;

        // Ateþ noktasýnýn yukarý doðru eðimini hesapla
        float pitch = Mathf.Atan2(direction.y, direction.magnitude) * Mathf.Rad2Deg;

        // Ateþ noktasýnýn rotasyonuna eðimi ekle
        t.rotation *= Quaternion.Euler(-pitch, 0f, 0f);
    }
}