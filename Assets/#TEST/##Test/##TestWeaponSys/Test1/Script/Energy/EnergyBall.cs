using UnityEngine;

public class EnergyBall : Weapon
{
    public float speed = 10f; // enerji topunun h�z�
    public float damage = 5f; // enerji topunun verdi�i hasar
    public GameObject energyBallPrefab; // enerji topunun prefab�
    public Transform[] firePoints; // enerji topunun ��k�� noktalar�

    public override void Fire()
    {
        base.Fire(); // ana s�n�f�n fire fonksiyonunu �a��r
        foreach (Transform firePoint in firePoints) // her ��k�� noktas� i�in
        {
            GameObject energyBall = Instantiate(energyBallPrefab, firePoint.position, firePoint.rotation); // enerji topunu olu�tur
            energyBall.GetComponent<Rigidbody>().AddForce(firePoint.forward * speed, ForceMode.Impulse); // enerji topuna ileri do�ru bir kuvvet uygula
        }
    }
}