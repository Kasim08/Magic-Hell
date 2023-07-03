using UnityEngine;

public class RocketLauncher : Weapon
{
    public float speed = 20f; // roketin h�z�
    public GameObject rocketPrefab; // roketin prefab'�

    public override void Fire()
    {
        base.Fire(); // ana s�n�f�n fire fonksiyonunu �a��r
        GameObject rocket = Instantiate(rocketPrefab, firePoint.transform.position, firePoint.transform.rotation); // roketi ate�leme noktas�ndan olu�tur
        rocket.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse); // rokete ileri do�ru bir kuvvet uygula
    }
}