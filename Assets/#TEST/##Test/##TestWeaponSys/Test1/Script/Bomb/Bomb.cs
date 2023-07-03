using UnityEngine;

public class Bomb : Weapon
{
    public float speed = 10f; // bombanýn hýzý
    public float damage = 20f; // bombanýn verdiði hasar
    public float explosionRadius = 10f; // bombanýn patlama yarýçapý
    public GameObject explosionEffect; // bombanýn patlama efekti
    public GameObject bombPrefab; // bombanýn prefabý
    //public Transform firePoint; // bombanýn çýkýþ noktasý

    public override void Fire()
    {
        base.Fire(); // ana sýnýfýn fire fonksiyonunu çaðýr
        GameObject bomb = Instantiate(bombPrefab, firePoint.transform.position, firePoint.transform.rotation); // bombayý oluþtur
        bomb.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * speed, ForceMode.Impulse); // bombaya ileri doðru bir kuvvet uygula
    }
}
