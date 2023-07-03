using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AbstractTower abstract sýnýfý
public abstract class AbstractTower : MonoBehaviour
{
    // Gerekli deðiþkenleri tanýmla
    protected ITowerShooter shootMethod;
    protected IEnemyTarget enemyTargetMethod;
    protected Transform target;

    // Abstract sýnýftan gelen metodun gövdesini yaz
    public abstract void Fire();

    // Abstract sýnýftan gelen metodun gövdesini yaz
    public virtual void EnemyTarget() { }
}