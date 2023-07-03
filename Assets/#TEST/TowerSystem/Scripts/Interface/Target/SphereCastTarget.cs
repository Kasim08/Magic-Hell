using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

// Bu interface'den kal�t�m alan SphereCastTarget s�n�f� olu�tur
public class SphereCastTarget : IEnemyTarget
{
    // Gerekli de�i�kenleri tan�mla
    protected Transform scanTransform;
    protected Vector3 sphereDirection;
    protected float sphereRadius = 10;
    protected int enemyLayerMask;
    protected float maxDistance = 10;
    // Hedef nesneyi d�nd�ren bir de�i�ken
    protected Transform target;

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    public SphereCastTarget(Transform scanTransform)
    {
        this.scanTransform = scanTransform;
        this.sphereDirection = scanTransform.forward;
        this.enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    public SphereCastTarget(Transform scanTransform, Vector3 sphereDirection, float sphereRadius,float maxDistance, int enemyLayerMask)
    {
        this.scanTransform = scanTransform;
        this.sphereDirection = sphereDirection;
        this.sphereRadius = sphereRadius;
        this.maxDistance = maxDistance;
        this.enemyLayerMask = enemyLayerMask;
    }

    // Interface'den gelen metodun g�vdesini yaz
    public Object EnemyTarget()
    {
        // Ate�in etraf�ndaki d��manlar� bul
        RaycastHit hit;
        bool isHit = Physics.SphereCast(scanTransform.position, sphereRadius, sphereDirection, out hit, maxDistance, enemyLayerMask);

        // E�er bir d��man varsa, onu hedef olarak belirle
        if (isHit)
        {
            return hit.collider.transform;
        }
        else
        {
            return null;
        }
    }
}