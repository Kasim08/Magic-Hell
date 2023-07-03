using System;
using UnityEngine;

// Bu interface'den kal�t�m alan OverlapSphereTarget s�n�f� olu�tur
public class OverlapSphereTarget : IEnemyTarget
{
    // Gerekli de�i�kenleri tan�mla
    protected Vector3 position;
    protected float radius = 10;
    protected int enemyLayerMask;
    // Hedef se�mek i�in delegate fonksiyonu
    Func<Collider[],Vector3, UnityEngine.Object> selectTarget;


    // Sol taraf arg�man, sa� taraf son de�er d�n�� de�eri. Lambda sol taraf� de�er atamas� sa� taraf� d�nd�r�lecek de�er i�lemleri ve d�nd�r�lmesi.
    // Rastgele hedef se�mek i�in bir lambda ifadesi yaz
    public static Func<Collider[], Vector3, UnityEngine.Object> randomSelect = (enemies, _) => enemies[UnityEngine.Random.Range(0, enemies.Length)];

    //S�n�f d�zeyinde tan�mlamak ��zemedi�im bir hataya yol a�t� ��zmek i�in static de�i�ken tan�mlamak zorunda kal�yorum.
    // En yak�n hedef se�mek i�in bir delegate fonksiyon yaz
    public static Func<Collider[],Vector3, UnityEngine.Object> nearestSelect = (enemies,position) =>
    {
        // En yak�n d��man� belirle.
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


    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    public OverlapSphereTarget(Vector3 position)
    {
        this.position = position;
        this.enemyLayerMask = LayerMask.GetMask("Enemy");
        this.selectTarget = nearestSelect;
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
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

    // Interface'den gelen metodun g�vdesini yaz
    public UnityEngine.Object EnemyTarget()
    {
        // Etraftaki t�m d��manlar� bul
        Collider[] enemies = Physics.OverlapSphere(position, radius, enemyLayerMask);
        return selectTarget(enemies, position);
    }
}