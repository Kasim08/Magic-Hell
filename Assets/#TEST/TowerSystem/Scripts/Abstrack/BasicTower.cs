using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AbstractTower s�n�f�ndan kal�t�m alan BulletRandomTower somut s�n�f�
// Bu s�n�f random ate� edece�i ve herhangi bir objeyi hedeflemeyece�i i�in farkl� bir �ekilde yani sadece belirlenen y�ne belirlenen kuvvet ile ate� edecektir.
public class BasicTower : AbstractTower
{
    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    public BasicTower(Transform shootTransform, GameObject bulletPrefab, float shotForce)
    {
        // Ate� etme �eklini belirleyen de�i�keni BulletShooter olarak ata
        shootMethod = new TowerShooter(shootTransform, bulletPrefab, shotForce);
    }

    // Abstract s�n�ftan gelen metodun g�vdesini yaz
    public override void Fire()
    {
        // Ate� etme �eklini belirleyen de�i�kenin Shoot metodunu �a��r
        shootMethod.Shoot();
    }
}