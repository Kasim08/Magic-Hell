using UnityEngine;

public class Bomb : Weapon
{
    public float speed = 10f; // bomban�n h�z�
    public float damage = 20f; // bomban�n verdi�i hasar
    public float explosionRadius = 10f; // bomban�n patlama yar��ap�
    public GameObject explosionEffect; // bomban�n patlama efekti
    public GameObject bombPrefab; // bomban�n prefab�
    //public Transform firePoint; // bomban�n ��k�� noktas�

    public override void Fire()
    {
        base.Fire(); // ana s�n�f�n fire fonksiyonunu �a��r
        GameObject bomb = Instantiate(bombPrefab, firePoint.transform.position, firePoint.transform.rotation); // bombay� olu�tur
        bomb.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * speed, ForceMode.Impulse); // bombaya ileri do�ru bir kuvvet uygula
    }
}
