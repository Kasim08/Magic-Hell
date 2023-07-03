using UnityEngine;

public class Shotgun : Weapon
{
    public int pelletCount = 10; // saçmanýn parça sayýsý
    public float spreadAngle = 10f; // saçmanýn yayýlma açýsý
    public float damage = 2f; // saçmanýn verdiði hasar
    public float speed = 2f;
    public GameObject pelletPrefab; // saçmanýn prefabý
    //public Transform firePoint; // saçmanýn çýkýþ noktasý

    public override void Fire()
    {
        base.Fire(); // ana sýnýfýn fire fonksiyonunu çaðýr
        for (int i = 0; i < pelletCount; i++) // parça sayýsý kadar döngü yap
        {
            GameObject pellet = Instantiate(pelletPrefab, firePoint.transform.position, firePoint.transform.rotation); // saçmayý oluþtur
            pellet.transform.Rotate(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0); // saçmayý rastgele bir açýyla döndür
            pellet.GetComponent<Rigidbody>().AddForce(pellet.transform.forward * speed, ForceMode.Impulse); // saçmaya ileri doðru bir kuvvet uygula
        }
    }
}
