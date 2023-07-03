using UnityEngine;

// Scriptable object s�n�f�n� tan�mlamak i�in [CreateAssetMenu] attribute'�n� kullan�n
[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerData", order = 1)]
public class TowerData : ScriptableObject
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


    // Bu de�i�kenlere eri�mek i�in public property'ler tan�mlay�n
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