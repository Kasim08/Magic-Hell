using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bu interface'den kal�t�m alan RandomTarget s�n�f� olu�tur bu s�n�fta random listeden hedef se�erek ate� edebiliriz
public class RandomTarget : IEnemyTarget
{
    // Gerekli de�i�kenleri tan�mla
    protected Transform[] scanTransforms;

    // S�n�f�n kurucu metodu, gerekli de�i�kenleri al�r ve atar
    public RandomTarget(Transform[] scanTransforms)
    {
        this.scanTransforms = scanTransforms;
    }

    // Interface'den gelen metodun g�vdesini yaz
    public Object EnemyTarget()
    {
        // E�er scanTransforms dizisi bo� ise, null d�nd�r
        if (scanTransforms.Length == 0) return null;

        // Rastgele bir transform nesnesi se�
        int index = UnityEngine.Random.Range(0, scanTransforms.Length);
        return scanTransforms[index];
    }
}
// ileride burada update yaparak kurucu fonksiyon i�ine arg�man almadan bunu di�er target s�n�flar�n�n kal�t�lm�� hali ile belli alandakileri diziye al�p o dizi i�inde rastgele ate� target se�en bir fonksiyon yapcaca��m.