using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AbstractTower sýnýfýndan kalýtým alan BulletRandomTower somut sýnýfý
// Bu sýnýf random ateþ edeceði ve herhangi bir objeyi hedeflemeyeceði için farklý bir þekilde yani sadece belirlenen yöne belirlenen kuvvet ile ateþ edecektir.
public class BasicTower : AbstractTower
{
    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    public BasicTower(Transform shootTransform, GameObject bulletPrefab, float shotForce)
    {
        // Ateþ etme þeklini belirleyen deðiþkeni BulletShooter olarak ata
        shootMethod = new TowerShooter(shootTransform, bulletPrefab, shotForce);
    }

    // Abstract sýnýftan gelen metodun gövdesini yaz
    public override void Fire()
    {
        // Ateþ etme þeklini belirleyen deðiþkenin Shoot metodunu çaðýr
        shootMethod.Shoot();
    }
}