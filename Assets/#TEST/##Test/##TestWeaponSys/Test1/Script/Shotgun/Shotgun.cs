using UnityEngine;

public class Shotgun : Weapon
{
    public int pelletCount = 10; // sa�man�n par�a say�s�
    public float spreadAngle = 10f; // sa�man�n yay�lma a��s�
    public float damage = 2f; // sa�man�n verdi�i hasar
    public float speed = 2f;
    public GameObject pelletPrefab; // sa�man�n prefab�
    //public Transform firePoint; // sa�man�n ��k�� noktas�

    public override void Fire()
    {
        base.Fire(); // ana s�n�f�n fire fonksiyonunu �a��r
        for (int i = 0; i < pelletCount; i++) // par�a say�s� kadar d�ng� yap
        {
            GameObject pellet = Instantiate(pelletPrefab, firePoint.transform.position, firePoint.transform.rotation); // sa�may� olu�tur
            pellet.transform.Rotate(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0); // sa�may� rastgele bir a��yla d�nd�r
            pellet.GetComponent<Rigidbody>().AddForce(pellet.transform.forward * speed, ForceMode.Impulse); // sa�maya ileri do�ru bir kuvvet uygula
        }
    }
}
