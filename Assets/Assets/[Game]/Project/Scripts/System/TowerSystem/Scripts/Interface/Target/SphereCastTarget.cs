using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

// Bu interface'den kalýtým alan SphereCastTarget sýnýfý oluþtur
public class SphereCastTarget : IEnemyTarget
{
    // Gerekli deðiþkenleri tanýmla
    protected Transform scanTransform;
    protected Vector3 sphereDirection;
    protected float sphereRadius = 10;
    protected int enemyLayerMask;
    protected float maxDistance = 10;
    // Hedef nesneyi döndüren bir deðiþken
    protected Transform target;

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    public SphereCastTarget(Transform scanTransform)
    {
        this.scanTransform = scanTransform;
        this.sphereDirection = scanTransform.forward;
        this.enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    public SphereCastTarget(Transform scanTransform, Vector3 sphereDirection, float sphereRadius,float maxDistance, int enemyLayerMask)
    {
        this.scanTransform = scanTransform;
        this.sphereDirection = sphereDirection;
        this.sphereRadius = sphereRadius;
        this.maxDistance = maxDistance;
        this.enemyLayerMask = enemyLayerMask;
    }

    // Interface'den gelen metodun gövdesini yaz
    public Object EnemyTarget()
    {
        // Ateþin etrafýndaki düþmanlarý bul
        RaycastHit hit;
        bool isHit = Physics.SphereCast(scanTransform.position, sphereRadius, sphereDirection, out hit, maxDistance, enemyLayerMask);

        // Eðer bir düþman varsa, onu hedef olarak belirle
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