using UnityEngine;

public class RocketLauncher : Weapon
{
    public float speed = 20f; // roketin hýzý
    public GameObject rocketPrefab; // roketin prefab'ý

    public override void Fire()
    {
        base.Fire(); // ana sýnýfýn fire fonksiyonunu çaðýr
        GameObject rocket = Instantiate(rocketPrefab, firePoint.transform.position, firePoint.transform.rotation); // roketi ateþleme noktasýndan oluþtur
        rocket.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse); // rokete ileri doðru bir kuvvet uygula
    }
}