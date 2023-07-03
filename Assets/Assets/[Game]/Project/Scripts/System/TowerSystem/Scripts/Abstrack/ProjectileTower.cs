using System;
using UnityEngine;

// AbstractTower sýnýfýndan kalýtým alan ProjectileTower somut sýnýfý
public class ProjectileTower : AbstractTower
{
    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    //Projecktile + SphereCastTarget
    public ProjectileTower(Transform scanTransform, Vector3 sphereDirection, float sphereRadius, float maxDistance, string layer, Transform fireTransform, GameObject bulletPrefab, float parameter, bool method)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda deðer atamalarý yapýlýyorsa burada sýralamalar önemli olabilir.
        // Düþman bulma þeklini belirleyen deðiþkeni SphereCastTarget olarak ata
        enemyTargetMethod = new SphereCastTarget(scanTransform, sphereDirection, sphereRadius, maxDistance, enemyLayerMask);

        // Ateþ etme þeklini belirleyen deðiþkeni ProjectileTargetShooter olarak ata
        shootMethod = new ProjectileTargetShooter(fireTransform, bulletPrefab, parameter, method);
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    // default = nearestSelect
    // Projecktile + OverlapSphereTarget
    public ProjectileTower(Transform scanTransform, float sphereRadius, string layer, Transform fireTransform, GameObject bulletPrefab, float parameter, bool method)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda deðer atamalarý yapýlýyorsa burada sýralamalar önemli olabilir.
        // Düþman bulma þeklini belirleyen deðiþkeni OverlapSphereTarget olarak ata
        enemyTargetMethod = new OverlapSphereTarget(scanTransform.position, sphereRadius, enemyLayerMask);

        // Ateþ etme þeklini belirleyen deðiþkeni ProjectileTargetShooter olarak ata
        shootMethod = new ProjectileTargetShooter(fireTransform, bulletPrefab, parameter, method);
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    // Projecktile + OverlapSphereTarget
    public ProjectileTower(Transform scanTransform, float sphereRadius, string layer, Transform fireTransform, GameObject bulletPrefab, Func<Collider[], Vector3, UnityEngine.Object> selectTarget, float parameter, bool method)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda deðer atamalarý yapýlýyorsa burada sýralamalar önemli olabilir.
        // Düþman bulma þeklini belirleyen deðiþkeni OverlapSphereTarget olarak ata
        enemyTargetMethod = new OverlapSphereTarget(scanTransform.position, sphereRadius, enemyLayerMask, selectTarget);

        // Ateþ etme þeklini belirleyen deðiþkeni ProjectileTargetShooter olarak ata
        shootMethod = new ProjectileTargetShooter(fireTransform, bulletPrefab, parameter, method);
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    // Projecktile + RandomTarget
    public ProjectileTower(Transform[] scanTransforms, Transform fireTransform, GameObject bulletPrefab, float parameter, bool method)
    {
        enemyTargetMethod = new RandomTarget(scanTransforms);

        // Ateþ etme þeklini belirleyen deðiþkeni ProjectileTargetShooter olarak ata
        shootMethod = new ProjectileTargetShooter(fireTransform, bulletPrefab, parameter, method);
    }

    // Abstract sýnýftan gelen metodun gövdesini yaz
    public override void EnemyTarget()
    {
        // Düþman bulma þeklini belirleyen deðiþkenin EnemyTarget metodunu çaðýr. Transform açýk deðiþim uyguluyorz object döndürüyor fonksiyon dönüþüm bunun için önemli.
        target = (Transform)enemyTargetMethod.EnemyTarget();
    }

    // Abstract sýnýftan gelen metodun gövdesini yaz
    public override void Fire()
    {

        if (target != null)
        {
            shootMethod.target = target; // target özelliðini set et
                                         // Ateþ etme þeklini belirleyen deðiþkenin Shoot metodunu çaðýr
            shootMethod.Shoot();
        }
    }
}