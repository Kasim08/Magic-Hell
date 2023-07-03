using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Yer �ekimi olan durumda ate� etme �eklini belirleyen bir s�n�f tan�mla ve interface s�n�f�n� uygula
public class ProjectileTargetShooter : TowerShooter
{
    // ProjectileTargetShooter s�n�f�nda bir delegate tan�mla
    public delegate Vector3 CalculationMethod(Vector3 target);

    private CalculationMethod calculationMethod;

    // Mermiyi ate�lemek i�in kullanaca��m�z a��. E�er CalculateProjectileVelocity kullan�lacak ise.
    protected float fireAngle = 45;

    public ProjectileTargetShooter(Transform fireTransform, GameObject bulletPrefab,float parameter): base(fireTransform, bulletPrefab)
    {
        calculationMethod = CalculateProjectileAngle;
        shotForce = parameter;
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    public ProjectileTargetShooter(Transform fireTransform, GameObject bulletPrefab, float parameter, bool method) : base(fireTransform, bulletPrefab)
    {
        // Burada base ile TowerShooter s�n�f�n�n kurucu fonksiyonunu �a��r�yoruz
        // ve fireTransform, bulletPrefab ve shotForce de�erlerini g�nderiyoruz
        // Bu de�erler TowerShooter s�n�f�n�n de�i�kenlerine atanacak

        // Delegate de�i�kenine g�re fireAngle veya shotForce de�erini ata
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

    // Interface s�n�f�ndan gelen metodun g�vdesini yaz
    public override void Shoot()
    {
        if (target == null) return;

        // Mermiyi olu�tur
        GameObject bullet = UnityEngine.Object.Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);

        // Mermiyi hedefe do�ru atacak kuvveti hesapla
        Vector3 force = calculationMethod(target.position);

        // Mermiyi hedefe do�ru at
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    // Mermiye uygulanacak kuvveti hesaplayan bir y�ntem (yer �ekimi varsa)
    public Vector3 CalculateProjectileVelocity(Vector3 target)
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
        return force;
    }

    // Mermiye uygulanacak a��y� hesaplayan bir y�ntem (yer �ekimi varsa)
    public Vector3 CalculateProjectileAngle(Vector3 target)
    {
        // Mermiyi ate�leyece�imiz nokta
        Vector3 firePoint = fireTransform.position;

        // Hedef ile ate� noktas� aras�ndaki yatay mesafe
        float x = Vector3.Distance(new Vector3(firePoint.x, 0, firePoint.z), new Vector3(target.x, 0, target.z));

        // Hedef ile ate� noktas� aras�ndaki dikey mesafe
        float y = target.y - firePoint.y;

        // Mermiyi ate�lemek i�in gereken ba�lang�� h�z�n� hesapla
        float v = shotForce;

        // Mermiyi ate�lemek i�in gereken a��y� hesapla (radyan cinsinden)
        float theta = 0.5f * Mathf.Atan((v * v + Mathf.Sqrt(v * v * v * v - Physics.gravity.magnitude * (Physics.gravity.magnitude * x * x + 2 * y * v * v))) / (Physics.gravity.magnitude * x));

        // A��y� derece cinsine �evir
        theta *= Mathf.Rad2Deg;

        // A��n�n de�erini fireAngle de�i�kenine ata
        fireAngle = theta;

        // Mermiyi ate�lemek i�in gereken y�n� hesapla
        Vector3 direction = (target - firePoint).normalized;

        // Mermiyi ate�lemek i�in gereken kuvveti hesapla
        Vector3 force = v * direction;
        force.y += v * Mathf.Sin(theta * Mathf.Deg2Rad);

        // Mermiyi ate�lemek i�in gereken kuvveti d�nd�r
        return force;
    }
}