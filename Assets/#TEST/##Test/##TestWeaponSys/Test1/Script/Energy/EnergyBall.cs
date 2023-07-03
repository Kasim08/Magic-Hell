using UnityEngine;

public class EnergyBall : Weapon
{
    public float speed = 10f; // enerji topunun hýzý
    public float damage = 5f; // enerji topunun verdiði hasar
    public GameObject energyBallPrefab; // enerji topunun prefabý
    public Transform[] firePoints; // enerji topunun çýkýþ noktalarý

    public override void Fire()
    {
        base.Fire(); // ana sýnýfýn fire fonksiyonunu çaðýr
        foreach (Transform firePoint in firePoints) // her çýkýþ noktasý için
        {
            GameObject energyBall = Instantiate(energyBallPrefab, firePoint.position, firePoint.rotation); // enerji topunu oluþtur
            energyBall.GetComponent<Rigidbody>().AddForce(firePoint.forward * speed, ForceMode.Impulse); // enerji topuna ileri doðru bir kuvvet uygula
        }
    }
}