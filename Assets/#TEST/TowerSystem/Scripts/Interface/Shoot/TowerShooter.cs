using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Random bir noktadan veya verilen ge�erli noktadan sadece ate� edilmesi gerekiyorsa bu s�n�f kullan�labilinir.
public class TowerShooter : ITowerShooter
{
    // ITowerShooter aray�z�nden gelen target �zelli�i
    public Transform target { get; set; }

    // Ate� etme kuvveti
    protected float shotForce = 10;

    // Mermiyi olu�turacak transform nesnesi
    protected Transform fireTransform;

    // Mermi nesnesi
    protected GameObject bulletPrefab;

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    public TowerShooter(Transform fireTransform, GameObject bulletPrefab)
    {
        this.fireTransform = fireTransform;
        this.bulletPrefab = bulletPrefab;
    }
    public TowerShooter(Transform fireTransform, GameObject bulletPrefab, float shotForce)
    {
        this.fireTransform = fireTransform;
        this.bulletPrefab = bulletPrefab;
        this.shotForce = shotForce;
    }

    // Interface s�n�f�ndan gelen metodun g�vdesini yaz
    public virtual void Shoot()
    {
        // Mermiyi olu�tur
        GameObject bullet = Object.Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);

        // Mermiye kuvvet uygula
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(fireTransform.forward * shotForce, ForceMode.Impulse);
        //rb.velocity = bullet.transform.forward * 10f;


    }
}