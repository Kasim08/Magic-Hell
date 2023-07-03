using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Yer çekimi olan durumda ateþ etme þeklini belirleyen bir sýnýf tanýmla ve interface sýnýfýný uygula
public class ProjectileTargetShooter : TowerShooter
{
    // ProjectileTargetShooter sýnýfýnda bir delegate tanýmla
    public delegate Vector3 CalculationMethod(Vector3 target);

    private CalculationMethod calculationMethod;

    // Mermiyi ateþlemek için kullanacaðýmýz açý. Eðer CalculateProjectileVelocity kullanýlacak ise.
    protected float fireAngle = 45;

    public ProjectileTargetShooter(Transform fireTransform, GameObject bulletPrefab,float parameter): base(fireTransform, bulletPrefab)
    {
        calculationMethod = CalculateProjectileAngle;
        shotForce = parameter;
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    public ProjectileTargetShooter(Transform fireTransform, GameObject bulletPrefab, float parameter, bool method) : base(fireTransform, bulletPrefab)
    {
        // Burada base ile TowerShooter sýnýfýnýn kurucu fonksiyonunu çaðýrýyoruz
        // ve fireTransform, bulletPrefab ve shotForce deðerlerini gönderiyoruz
        // Bu deðerler TowerShooter sýnýfýnýn deðiþkenlerine atanacak

        // Delegate deðiþkenine göre fireAngle veya shotForce deðerini ata
        if (method)
        {
            calculationMethod = CalculateProjectileVelocity;
            fireAngle = parameter;
        }
        else
        {
            calculationMethod = CalculateProjectileAngle;
            shotForce = parameter;
        }
    }

    // Interface sýnýfýndan gelen metodun gövdesini yaz
    public override void Shoot()
    {
        if (target == null) return;

        // Mermiyi oluþtur
        GameObject bullet = UnityEngine.Object.Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);

        // Mermiyi hedefe doðru atacak kuvveti hesapla
        Vector3 force = calculationMethod(target.position);

        // Mermiyi hedefe doðru at
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    // Mermiye uygulanacak kuvveti hesaplayan bir yöntem (yer çekimi varsa)
    public Vector3 CalculateProjectileVelocity(Vector3 target)
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
        return force;
    }

    // Mermiye uygulanacak açýyý hesaplayan bir yöntem (yer çekimi varsa)
    public Vector3 CalculateProjectileAngle(Vector3 target)
    {
        // Mermiyi ateþleyeceðimiz nokta
        Vector3 firePoint = fireTransform.position;

        // Hedef ile ateþ noktasý arasýndaki yatay mesafe
        float x = Vector3.Distance(new Vector3(firePoint.x, 0, firePoint.z), new Vector3(target.x, 0, target.z));

        // Hedef ile ateþ noktasý arasýndaki dikey mesafe
        float y = target.y - firePoint.y;

        // Mermiyi ateþlemek için gereken baþlangýç hýzýný hesapla
        float v = shotForce;

        // Mermiyi ateþlemek için gereken açýyý hesapla (radyan cinsinden)
        float theta = 0.5f * Mathf.Atan((v * v + Mathf.Sqrt(v * v * v * v - Physics.gravity.magnitude * (Physics.gravity.magnitude * x * x + 2 * y * v * v))) / (Physics.gravity.magnitude * x));

        // Açýyý derece cinsine çevir
        theta *= Mathf.Rad2Deg;

        // Açýnýn deðerini fireAngle deðiþkenine ata
        fireAngle = theta;

        // Mermiyi ateþlemek için gereken yönü hesapla
        Vector3 direction = (target - firePoint).normalized;

        // Mermiyi ateþlemek için gereken kuvveti hesapla
        Vector3 force = v * direction;
        force.y += v * Mathf.Sin(theta * Mathf.Deg2Rad);

        // Mermiyi ateþlemek için gereken kuvveti döndür
        return force;
    }
}