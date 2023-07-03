using System;
using UnityEngine;

// AbstractTower s�n�f�ndan kal�t�m alan ProjectileTower somut s�n�f�
public class ProjectileTower : AbstractTower
{
    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    //Projecktile + SphereCastTarget
    public ProjectileTower(Transform scanTransform, Vector3 sphereDirection, float sphereRadius, float maxDistance, string layer, Transform fireTransform, GameObject bulletPrefab, float parameter, bool method)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda de�er atamalar� yap�l�yorsa burada s�ralamalar �nemli olabilir.
        // D��man bulma �eklini belirleyen de�i�keni SphereCastTarget olarak ata
        enemyTargetMethod = new SphereCastTarget(scanTransform, sphereDirection, sphereRadius, maxDistance, enemyLayerMask);

        // Ate� etme �eklini belirleyen de�i�keni ProjectileTargetShooter olarak ata
        shootMethod = new ProjectileTargetShooter(fireTransform, bulletPrefab, parameter, method);
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    // default = nearestSelect
    // Projecktile + OverlapSphereTarget
    public ProjectileTower(Transform scanTransform, float sphereRadius, string layer, Transform fireTransform, GameObject bulletPrefab, float parameter, bool method)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda de�er atamalar� yap�l�yorsa burada s�ralamalar �nemli olabilir.
        // D��man bulma �eklini belirleyen de�i�keni OverlapSphereTarget olarak ata
        enemyTargetMethod = new OverlapSphereTarget(scanTransform.position, sphereRadius, enemyLayerMask);

        // Ate� etme �eklini belirleyen de�i�keni ProjectileTargetShooter olarak ata
        shootMethod = new ProjectileTargetShooter(fireTransform, bulletPrefab, parameter, method);
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    // Projecktile + OverlapSphereTarget
    public ProjectileTower(Transform scanTransform, float sphereRadius, string layer, Transform fireTransform, GameObject bulletPrefab, Func<Collider[], Vector3, UnityEngine.Object> selectTarget, float parameter, bool method)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda de�er atamalar� yap�l�yorsa burada s�ralamalar �nemli olabilir.
        // D��man bulma �eklini belirleyen de�i�keni OverlapSphereTarget olarak ata
        enemyTargetMethod = new OverlapSphereTarget(scanTransform.position, sphereRadius, enemyLayerMask, selectTarget);

        // Ate� etme �eklini belirleyen de�i�keni ProjectileTargetShooter olarak ata
        shootMethod = new ProjectileTargetShooter(fireTransform, bulletPrefab, parameter, method);
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    // Projecktile + RandomTarget
    public ProjectileTower(Transform[] scanTransforms, Transform fireTransform, GameObject bulletPrefab, float parameter, bool method)
    {
        enemyTargetMethod = new RandomTarget(scanTransforms);

        // Ate� etme �eklini belirleyen de�i�keni ProjectileTargetShooter olarak ata
        shootMethod = new ProjectileTargetShooter(fireTransform, bulletPrefab, parameter, method);
    }

    // Abstract s�n�ftan gelen metodun g�vdesini yaz
    public override void EnemyTarget()
    {
        // D��man bulma �eklini belirleyen de�i�kenin EnemyTarget metodunu �a��r. Transform a��k de�i�im uyguluyorz object d�nd�r�yor fonksiyon d�n���m bunun i�in �nemli.
        target = (Transform)enemyTargetMethod.EnemyTarget();
    }

    // Abstract s�n�ftan gelen metodun g�vdesini yaz
    public override void Fire()
    {

        if (target != null)
        {
            shootMethod.target = target; // target �zelli�ini set et
                                         // Ate� etme �eklini belirleyen de�i�kenin Shoot metodunu �a��r
            shootMethod.Shoot();
        }
    }
}