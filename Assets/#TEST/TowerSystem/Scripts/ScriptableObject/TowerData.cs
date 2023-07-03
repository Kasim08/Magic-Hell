using UnityEngine;

// Scriptable object sınıfını tanımlamak için [CreateAssetMenu] attribute'ünü kullanın
[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerData", order = 1)]
public class TowerData : ScriptableObject
{
    // Kule tipi, düşman bulma şekli ve delegate fonksiyonu seçmek için enumları tanımlayın
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
    // Ateş etme yöntemini belirleyen bir enum tipi tanımlayın
    public enum CalculationMethod
    {
        CalculateProjectileVelocity,
        CalculateProjectileAngle
    }

    // Kuleyi oluşturmak için gerekli değişkenleri [SerializeField] attribute ile private olarak tanımlayın
    [SerializeField] private TowerType towerType;
    [SerializeField] private TargetMethod targetMethod;
    [SerializeField] private SelectionMode selectionMode;
    [SerializeField] private CalculationMethod calculationMethod;
    [SerializeField] private float sphereRadius = 10f;
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotForce = 10f;
    [SerializeField] private float fireAngle = 45f;
    [SerializeField] private string enemyLayer;
    [SerializeField] private float fireRate = 1f;


    // Bu değişkenlere erişmek için public property'ler tanımlayın
    public TowerType TypeTower { get => towerType; }
    public TargetMethod MethodTarget { get => targetMethod; }
    public SelectionMode ModeSelection { get => selectionMode; }
    public CalculationMethod MethodCalculation { get => calculationMethod; }
    public float SphereRadius { get => sphereRadius; }
    public float MaxDistance { get => maxDistance; }
    public GameObject BulletPrefab { get => bulletPrefab; }
    public float ShotForce { get => shotForce; }
    public float FireAngle { get => fireAngle; }
    public string EnemyLayer { get => enemyLayer; }
    public float FireRate { get => fireRate;}

}