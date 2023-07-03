using System;
using UnityEngine;

// AbstractTower s�n�f�ndan kal�t�m alan BulletTower somut s�n�f�
public class BulletTower : AbstractTower
{
    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    //Bullet + SphereCastTarget
    public BulletTower(Transform scanTransform, Vector3 sphereDirection, float sphereRadius, float maxDistance, string layer, Transform fireTransform, GameObject bulletPrefab, float shotForce)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda de�er atamalar� yap�l�yorsa burada s�ralamalar �nemli olabilir.
        // D��man bulma �eklini belirleyen de�i�keni SphereCastTarget olarak ata
        enemyTargetMethod = new SphereCastTarget(scanTransform, sphereDirection, sphereRadius, maxDistance, enemyLayerMask);

        // Ate� etme �eklini belirleyen de�i�keni BulletTargetShooter olarak ata
        shootMethod = new BulletTargetShooter(fireTransform, bulletPrefab, shotForce);
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    // Bullet + OverlapSphereTarget
    // default = nearestSelect
    public BulletTower(Transform scanTransform, float sphereRadius, string layer, Transform fireTransform, GameObject bulletPrefab, float shotForce)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda de�er atamalar� yap�l�yorsa burada s�ralamalar �nemli olabilir.
        // D��man bulma �eklini belirleyen de�i�keni OverlapSphereTarget olarak ata
        enemyTargetMethod = new OverlapSphereTarget(scanTransform.position, sphereRadius, enemyLayerMask);

        // Ate� etme �eklini belirleyen de�i�keni BulletTargetShooter olarak ata
        shootMethod = new BulletTargetShooter(fireTransform, bulletPrefab, shotForce);
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    // Bullet + OverlapSphereTarget
    public BulletTower(Transform scanTransform, float sphereRadius, string layer, Transform fireTransform, GameObject bulletPrefab, float shotForce, Func<Collider[], Vector3, UnityEngine.Object> selectTarget)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda de�er atamalar� yap�l�yorsa burada s�ralamalar �nemli olabilir.
        // D��man bulma �eklini belirleyen de�i�keni OverlapSphereTarget olarak ata
        enemyTargetMethod = new OverlapSphereTarget(scanTransform.position, sphereRadius, enemyLayerMask, selectTarget);

        // Ate� etme �eklini belirleyen de�i�keni BulletTargetShooter olarak ata
        shootMethod = new BulletTargetShooter(fireTransform, bulletPrefab, shotForce);
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    // Bullet + RandomTarget
    public BulletTower(Transform[] scanTransforms,Transform fireTransform,GameObject bulletPrefab,float shotForce) 
    {
        enemyTargetMethod = new RandomTarget(scanTransforms);

        // Ate� etme �eklini belirleyen de�i�keni BulletTargetShooter olarak ata
        shootMethod = new BulletTargetShooter(fireTransform, bulletPrefab, shotForce);
    }

    // Abstract s�n�ftan gelen metodun g�vdesini yaz
    public override void EnemyTarget()
    {
        // D��man bulma �eklini belirleyen de�i�kenin EnemyTarget metodunu �a��r. Transform a��k de�i�im uyguluyorz object d�nd�r�yor fonksiyon d�n���m bunun i�in �nemli.
       target = (Transform)enemyTargetMethod.EnemyTarget();
       //UnityEngine.Debug.Log(target.position.ToString());
    }

    // Abstract s�n�ftan gelen metodun g�vdesini yaz
    public override void Fire()
    { 
        // Ate� etme �eklini belirleyen de�i�kenin Shoot metodunu �a��r
        if (target != null)
        {
            shootMethod.target = target; // target �zelli�ini set et
            shootMethod.Shoot();
        }
    }
}
