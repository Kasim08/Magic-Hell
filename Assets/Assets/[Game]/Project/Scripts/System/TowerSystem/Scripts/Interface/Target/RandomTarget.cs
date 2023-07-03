using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bu interface'den kalýtým alan RandomTarget sýnýfý oluþtur bu sýnýfta random listeden hedef seçerek ateþ edebiliriz
public class RandomTarget : IEnemyTarget
{
    // Gerekli deðiþkenleri tanýmla
    protected Transform[] scanTransforms;

    // Sýnýfýn kurucu metodu, gerekli deðiþkenleri alýr ve atar
    public RandomTarget(Transform[] scanTransforms)
    {
        this.scanTransforms = scanTransforms;
    }

    // Interface'den gelen metodun gövdesini yaz
    public Object EnemyTarget()
    {
        // Eðer scanTransforms dizisi boþ ise, null döndür
        if (scanTransforms.Length == 0) return null;

        // Rastgele bir transform nesnesi seç
        int index = UnityEngine.Random.Range(0, scanTransforms.Length);
        return scanTransforms[index];
    }
}
// ileride burada update yaparak kurucu fonksiyon içine argüman almadan bunu diðer target sýnýflarýnýn kalýtýlmýþ hali ile belli alandakileri diziye alýp o dizi içinde rastgele ateþ target seçen bir fonksiyon yapcacaðým.