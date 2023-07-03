using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Random bir noktadan veya verilen geçerli noktadan sadece ateþ edilmesi gerekiyorsa bu sýnýf kullanýlabilinir.
public class TowerShooter : ITowerShooter
{
    // ITowerShooter arayüzünden gelen target özelliði
    public Transform target { get; set; }

    // Ateþ etme kuvveti
    protected float shotForce = 10;

    // Mermiyi oluþturacak transform nesnesi
    protected Transform fireTransform;

    // Mermi nesnesi
    protected GameObject bulletPrefab;

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
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

    // Interface sýnýfýndan gelen metodun gövdesini yaz
    public virtual void Shoot()
    {
        // Mermiyi oluþtur
        GameObject bullet = Object.Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);

        // Mermiye kuvvet uygula
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(fireTransform.forward * shotForce, ForceMode.Impulse);
        //rb.velocity = bullet.transform.forward * 10f;


    }
}