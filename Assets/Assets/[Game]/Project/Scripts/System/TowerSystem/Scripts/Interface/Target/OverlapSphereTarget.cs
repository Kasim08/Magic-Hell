using System;
using UnityEngine;

// Bu interface'den kalýtým alan OverlapSphereTarget sýnýfý oluþtur
public class OverlapSphereTarget : IEnemyTarget
{
    // Gerekli deðiþkenleri tanýmla
    protected Vector3 position;
    protected float radius = 10;
    protected int enemyLayerMask;
    // Hedef seçmek için delegate fonksiyonu
    Func<Collider[],Vector3, UnityEngine.Object> selectTarget;


    // Sol taraf argüman, sað taraf son deðer dönüþ deðeri. Lambda sol tarafý deðer atamasý sað tarafý döndürülecek deðer iþlemleri ve döndürülmesi.
    // Rastgele hedef seçmek için bir lambda ifadesi yaz
    public static Func<Collider[], Vector3, UnityEngine.Object> randomSelect = (enemies, _) => enemies[UnityEngine.Random.Range(0, enemies.Length)];

    //Sýnýf düzeyinde tanýmlamak çözemediðim bir hataya yol açtý çözmek için static deðiþken tanýmlamak zorunda kalýyorum.
    // En yakýn hedef seçmek için bir delegate fonksiyon yaz
    public static Func<Collider[],Vector3, UnityEngine.Object> nearestSelect = (enemies,position) =>
    {
        // En yakýn düþmaný belirle.
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (Collider enemy in enemies)
        {
            float distance = (position - enemy.transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    };


    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    public OverlapSphereTarget(Vector3 position)
    {
        this.position = position;
        this.enemyLayerMask = LayerMask.GetMask("Enemy");
        this.selectTarget = nearestSelect;
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    public OverlapSphereTarget(Vector3 position, float radius, int enemyLayerMask)
    {
        this.position = position;
        this.radius = radius;
        this.enemyLayerMask = enemyLayerMask;
        this.selectTarget = nearestSelect;
    }

    public OverlapSphereTarget(Vector3 position, float radius, int enemyLayerMask, Func<Collider[], Vector3, UnityEngine.Object> selectTarget)
    {
        this.position = position;
        this.radius = radius;
        this.enemyLayerMask = enemyLayerMask;
        this.selectTarget = selectTarget;
    }

    // Interface'den gelen metodun gövdesini yaz
    public UnityEngine.Object EnemyTarget()
    {
        // Etraftaki tüm düþmanlarý bul
        Collider[] enemies = Physics.OverlapSphere(position, radius, enemyLayerMask);
        return selectTarget(enemies, position);
    }
}