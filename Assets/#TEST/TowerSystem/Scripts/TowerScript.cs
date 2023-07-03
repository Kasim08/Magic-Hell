using UnityEngine;

public class TowerScript : MonoBehaviour
{
    // Kule tipi, d��man bulma �ekli ve delegate fonksiyonu se�mek i�in enumlar� tan�mlay�n
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

    // Ate� etme y�ntemini belirleyen bir enum tipi tan�mlay�n
    public enum CalculationMethod
    {
        CalculateProjectileVelocity,
        CalculateProjectileAngle
    }

    // Kuleyi olu�turmak i�in gerekli de�i�kenleri [SerializeField] attribute ile private olarak tan�mlay�n
    [SerializeField] private Transform scanTransform;
    [SerializeField] private Transform[] enemysTransforms;
    [SerializeField] private float sphereRadius = 10f;
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotForce = 10f;
    [SerializeField] private float fireAngle = 45f;
    // D��manlar�n layer mask de�eri
    [SerializeField] private string layer;
    // Ate� etme oran�
    [SerializeField] private float fireRate = 1f;

    // Son ate� etme zaman�
    private float lastFireTime = 0f;

    // Bu enumlar� public olarak tan�mla
    public TowerType towerType;
    public TargetMethod targetMethod;
    public SelectionMode selectionMode;
    public CalculationMethod calculationMethod;
    // AbstractTower tipinde bir de�i�ken tan�mla
    private AbstractTower tower;
    //private ProjectileTargetShooter shooter;

    // Script ba�lad���nda �al��acak bir metod tan�mlay�n
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
                                // Bir ProjectileTargetShooter nesnesi olu�turma
                                //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, fireAngle, shooter.CalculateProjectileVelocity);
                                // Nesne �zerinden metodu �a��rma
                                tower = new ProjectileTower(scanTransform, scanTransform.forward, sphereRadius, maxDistance, layer, fireTransform, bulletPrefab, fireAngle, true);
                                break;
                            case TargetMethod.OverlapSphere:
                                switch (selectionMode)
                                {
                                    case SelectionMode.RandomSelect:
                                        // Bir ProjectileTargetShooter nesnesi olu�turma
                                        //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, fireAngle);
                                        // Nesne �zerinden metodu �a��rma
                                        tower = new ProjectileTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab,OverlapSphereTarget.randomSelect, fireAngle, true);
                                        break;
                                    case SelectionMode.NearestSelect:
                                        // Bir ProjectileTargetShooter nesnesi olu�turma
                                        //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, fireAngle);
                                        // Nesne �zerinden metodu �a��rma
                                        tower = new ProjectileTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab, OverlapSphereTarget.nearestSelect, fireAngle, true);
                                        break;
                                }
                                break;
                            case TargetMethod.Random:
                                // Bir ProjectileTargetShooter nesnesi olu�turma
                                //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, fireAngle, shooter.CalculateProjectileVelocity);
                                // Nesne �zerinden metodu �a��rma
                                tower = new ProjectileTower(enemysTransforms, fireTransform, bulletPrefab, fireAngle, true);
                                break;
                        }
                        break;

                    case CalculationMethod.CalculateProjectileAngle:
                        switch (targetMethod)
                        {
                            case TargetMethod.SphereCast:
                                // Bir ProjectileTargetShooter nesnesi olu�turma
                                //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, shotForce, shooter.CalculateProjectileAngle);
                                // Nesne �zerinden metodu �a��rma
                                tower = new ProjectileTower(scanTransform, scanTransform.forward, sphereRadius, maxDistance, layer, fireTransform, bulletPrefab, shotForce, false);
                                break;
                            case TargetMethod.OverlapSphere:
                                switch (selectionMode)
                                {
                                    case SelectionMode.RandomSelect:
                                        // Bir ProjectileTargetShooter nesnesi olu�turma
                                        //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, shotForce, shooter.CalculateProjectileAngle);
                                        // Nesne �zerinden metodu �a��rma
                                        tower = new ProjectileTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab, OverlapSphereTarget.randomSelect, fireAngle, false);
                                        break;
                                    case SelectionMode.NearestSelect:
                                        // Bir ProjectileTargetShooter nesnesi olu�turma
                                        //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, shotForce, shooter.CalculateProjectileAngle);
                                        // Nesne �zerinden metodu �a��rma
                                        tower = new ProjectileTower(scanTransform, sphereRadius, layer, fireTransform, bulletPrefab, OverlapSphereTarget.nearestSelect, fireAngle, false);
                                        break;
                                }
                                break;
                            case TargetMethod.Random:
                                // Bir ProjectileTargetShooter nesnesi olu�turma
                                //shooter = new ProjectileTargetShooter(fireTransform, bulletPrefab, shotForce, shooter.CalculateProjectileAngle);
                                // Nesne �zerinden metodu �a��rma
                                tower = new ProjectileTower(enemysTransforms, fireTransform, bulletPrefab, shotForce, false);
                                break;
                        }
                        break;
                }
                break;
        }
    }

    // Her karede �al��acak bir metod tan�mlay�n
    private void FixedUpdate()
    {
        // Ate� etme ko�ulunu bir bool de�i�kene atayabilirsin.
        bool canFire = Time.time - lastFireTime > 1f / fireRate;

        if (canFire)
        {
            // Kuleye d��man hedeflemesini s�yle
            tower.EnemyTarget();

            // Kuleye ate� etmesini s�yle
            tower.Fire();

            // Son ate� etme zaman�n� g�ncelle
            lastFireTime = Time.time;
        }
    }
}