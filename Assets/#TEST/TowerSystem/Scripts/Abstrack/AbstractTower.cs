using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AbstractTower abstract s�n�f�
public abstract class AbstractTower : MonoBehaviour
{
    // Gerekli de�i�kenleri tan�mla
    protected ITowerShooter shootMethod;
    protected IEnemyTarget enemyTargetMethod;
    protected Transform target;

    // Abstract s�n�ftan gelen metodun g�vdesini yaz
    public abstract void Fire();

    // Abstract s�n�ftan gelen metodun g�vdesini yaz
    public virtual void EnemyTarget() { }
}