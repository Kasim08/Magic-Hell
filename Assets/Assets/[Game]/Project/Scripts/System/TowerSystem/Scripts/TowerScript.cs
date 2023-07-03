using UnityEngine;

public class TowerScript : MonoBehaviour
{
    // Kule tipi, düþman bulma þekli ve delegate fonksiyonu seçmek için enumlarý tanýmlayýn
    public enum TowerType
    {
        Bullet,
        Projectile
    }

    public enum TargetMethod
    {
        SphereCast,
        OverlapSphere,
        Random
    }

    public enum SelectionMode
    {
        RandomSelect,
        NearestSelect
    }

    // Ateþ etme yöntemini belirleyen bir enum tipi tanýmlayýn
    public enum CalculationMethod
    {
        CalculateProjectileVelocity,
        CalculateProjectileAngle
    }

    // Kuleyi oluþturmak için gerekli deðiþkenleri [SerializeField] attribute ile private olarak tanýmlayýn
    [SerializeField] private Transform scanTransform;
    [SerializeField] private Transform[] enemysTransforms;
    [SerializeField] private float sphereRadius = 10f;
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotForce = 10f;
    [SerializeField] private float fireAngle = 45f;
    // Düþmanlarýn layer mask deðeri
    [SerializeField] private string layer;
    // Ateþ etme oraný
    [SerializeField] private float fireRate = 1f;

    // Son ateþ etme zamaný
    private float lastFireTime = 0f;

    // Bu enumlarý public olarak tanýmla
    public TowerType towerType;
    public TargetMethod targetMethod;
    public SelectionMode selectionMode;
    public CalculationMethod calculationMethod;
    // AbstractTower tipinde bir deðiþken tanýmla
    private AbstractTower tower;
    //private ProjectileTargetShooter shooter;

    // Script baþladýðýnda çalýþacak bir metod tanýmlayýn
    private void Awake()
    {
        switch (towerType)
        {
            case TowerType.Bullet:
                switch (targetMethod)
                {
                    case TargetMethod.SphereCast:
                        tower = new BulletTower(scanTransform, scanTransform.forward, sphereRadius, maxDistance, layer, fireTransform, bulletPrefab, shotForce);
                        break;
                    case TargetMethod.OverlapSphere:
                        switch (selectionMode)
                        {
                            case SelectionMode.RandomSelect:
                                tower = new BulletTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab, shotForce, OverlapSphereTarget.randomSelect);
                                break;
                            case SelectionMode.NearestSelect:
                                tower = new BulletTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab, shotForce, OverlapSphereTarget.nearestSelect);
                                break;
                        }

                        break;
                    case TargetMethod.Random:
                        tower = new BulletTower(enemysTransforms, fireTransform, bulletPrefab, shotForce);
                        break;
                }
                break;
            case TowerType.Projectile:
                switch (calculationMethod)
                {
                    case CalculationMethod.CalculateProjectileVelocity:
                        switch (targetMethod)
                        {
                            case TargetMethod.SphereCast:
                                // Bir ProjectileTargetShooter nesnesi oluþturma
                                //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, fireAngle, shooter.CalculateProjectileVelocity);
                                // Nesne üzerinden metodu çaðýrma
                                tower = new ProjectileTower(scanTransform, scanTransform.forward, sphereRadius, maxDistance, layer, fireTransform, bulletPrefab, fireAngle, true);
                                break;
                            case TargetMethod.OverlapSphere:
                                switch (selectionMode)
                                {
                                    case SelectionMode.RandomSelect:
                                        // Bir ProjectileTargetShooter nesnesi oluþturma
                                        //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, fireAngle);
                                        // Nesne üzerinden metodu çaðýrma
                                        tower = new ProjectileTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab,OverlapSphereTarget.randomSelect, fireAngle, true);
                                        break;
                                    case SelectionMode.NearestSelect:
                                        // Bir ProjectileTargetShooter nesnesi oluþturma
                                        //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, fireAngle);
                                        // Nesne üzerinden metodu çaðýrma
                                        tower = new ProjectileTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab, OverlapSphereTarget.nearestSelect, fireAngle, true);
                                        break;
                                }
                                break;
                            case TargetMethod.Random:
                                // Bir ProjectileTargetShooter nesnesi oluþturma
                                //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, fireAngle, shooter.CalculateProjectileVelocity);
                                // Nesne üzerinden metodu çaðýrma
                                tower = new ProjectileTower(enemysTransforms, fireTransform, bulletPrefab, fireAngle, true);
                                break;
                        }
                        break;

                    case CalculationMethod.CalculateProjectileAngle:
                        switch (targetMethod)
                        {
                            case TargetMethod.SphereCast:
                                // Bir ProjectileTargetShooter nesnesi oluþturma
                                //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, shotForce, shooter.CalculateProjectileAngle);
                                // Nesne üzerinden metodu çaðýrma
                                tower = new ProjectileTower(scanTransform, scanTransform.forward, sphereRadius, maxDistance, layer, fireTransform, bulletPrefab, shotForce, false);
                                break;
                            case TargetMethod.OverlapSphere:
                                switch (selectionMode)
                                {
                                    case SelectionMode.RandomSelect:
                                        // Bir ProjectileTargetShooter nesnesi oluþturma
                                        //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, shotForce, shooter.CalculateProjectileAngle);
                                        // Nesne üzerinden metodu çaðýrma
                                        tower = new ProjectileTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab, OverlapSphereTarget.randomSelect, fireAngle, false);
                                        break;
                                    case SelectionMode.NearestSelect:
                                        // Bir ProjectileTargetShooter nesnesi oluþturma
                                        //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, shotForce, shooter.CalculateProjectileAngle);
                                        // Nesne üzerinden metodu çaðýrma
                                        tower = new ProjectileTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab, OverlapSphereTarget.nearestSelect, fireAngle, false);
                                        break;
                                }
                                break;
                            case TargetMethod.Random:
                                // Bir ProjectileTargetShooter nesnesi oluþturma
                                //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, shotForce, shooter.CalculateProjectileAngle);
                                // Nesne üzerinden metodu çaðýrma
                                tower = new ProjectileTower(enemysTransforms, fireTransform, bulletPrefab, shotForce, false);
                                break;
                        }
                        break;
                }
                break;
        }
    }

    // Her karede çalýþacak bir metod tanýmlayýn
    private void FixedUpdate()
    {
        // Ateþ etme koþulunu bir bool deðiþkene atayabilirsin.
        bool canFire = Time.time - lastFireTime > 1f / fireRate;

        if (canFire)
        {
            // Kuleye düþman hedeflemesini söyle
            tower.EnemyTarget();

            // Kuleye ateþ etmesini söyle
            tower.Fire();

            // Son ateþ etme zamanýný güncelle
            lastFireTime = Time.time;
        }
    }
}