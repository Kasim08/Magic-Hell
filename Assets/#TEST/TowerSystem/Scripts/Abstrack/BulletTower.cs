using System;
using UnityEngine;

// AbstractTower sýnýfýndan kalýtým alan BulletTower somut sýnýfý
public class BulletTower : AbstractTower
{
    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    //Bullet + SphereCastTarget
    public BulletTower(Transform scanTransform, Vector3 sphereDirection, float sphereRadius, float maxDistance, string layer, Transform fireTransform, GameObject bulletPrefab, float shotForce)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda deðer atamalarý yapýlýyorsa burada sýralamalar önemli olabilir.
        // Düþman bulma þeklini belirleyen deðiþkeni SphereCastTarget olarak ata
        enemyTargetMethod = new SphereCastTarget(scanTransform, sphereDirection, sphereRadius, maxDistance, enemyLayerMask);

        // Ateþ etme þeklini belirleyen deðiþkeni BulletTargetShooter olarak ata
        shootMethod = new BulletTargetShooter(fireTransform, bulletPrefab, shotForce);
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    // Bullet + OverlapSphereTarget
    // default = nearestSelect
    public BulletTower(Transform scanTransform, float sphereRadius, string layer, Transform fireTransform, GameObject bulletPrefab, float shotForce)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda deðer atamalarý yapýlýyorsa burada sýralamalar önemli olabilir.
        // Düþman bulma þeklini belirleyen deðiþkeni OverlapSphereTarget olarak ata
        enemyTargetMethod = new OverlapSphereTarget(scanTransform.position, sphereRadius, enemyLayerMask);

        // Ateþ etme þeklini belirleyen deðiþkeni BulletTargetShooter olarak ata
        shootMethod = new BulletTargetShooter(fireTransform, bulletPrefab, shotForce);
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    // Bullet + OverlapSphereTarget
    public BulletTower(Transform scanTransform, float sphereRadius, string layer, Transform fireTransform, GameObject bulletPrefab, float shotForce, Func<Collider[], Vector3, UnityEngine.Object> selectTarget)
    {
        int enemyLayerMask = LayerMask.GetMask(layer);
        // Kurucu fonksiyonlarda deðer atamalarý yapýlýyorsa burada sýralamalar önemli olabilir.
        // Düþman bulma þeklini belirleyen deðiþkeni OverlapSphereTarget olarak ata
        enemyTargetMethod = new OverlapSphereTarget(scanTransform.position, sphereRadius, enemyLayerMask, selectTarget);

        // Ateþ etme þeklini belirleyen deðiþkeni BulletTargetShooter olarak ata
        shootMethod = new BulletTargetShooter(fireTransform, bulletPrefab, shotForce);
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    // Bullet + RandomTarget
    public BulletTower(Transform[] scanTransforms,Transform fireTransform,GameObject bulletPrefab,float shotForce) 
    {
        enemyTargetMethod = new RandomTarget(scanTransforms);

        // Ateþ etme þeklini belirleyen deðiþkeni BulletTargetShooter olarak ata
        shootMethod = new BulletTargetShooter(fireTransform, bulletPrefab, shotForce);
    }

    // Abstract sýnýftan gelen metodun gövdesini yaz
    public override void EnemyTarget()
    {
        // Düþman bulma þeklini belirleyen deðiþkenin EnemyTarget metodunu çaðýr. Transform açýk deðiþim uyguluyorz object döndürüyor fonksiyon dönüþüm bunun için önemli.
       target = (Transform)enemyTargetMethod.EnemyTarget();
       //UnityEngine.Debug.Log(target.position.ToString());
    }

    // Abstract sýnýftan gelen metodun gövdesini yaz
    public override void Fire()
    { 
        // Ateþ etme þeklini belirleyen deðiþkenin Shoot metodunu çaðýr
        if (target != null)
        {
            shootMethod.target = target; // target özelliðini set et
            shootMethod.Shoot();
        }
    }
}
