using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ammo = 10; // silah�n cephane say�s�
    public float recoil = 5f; // silah�n geri tepme kuvveti
    public AudioSource audioSource; // silah�n ses kayna��
    public AudioClip fireSound; // silah�n ate�leme sesi
    public GameObject firePoint; // silah�n ate�leme noktas�
    public GameObject muzzleEffect; // silah�n muzzle efekti
    public float fireRateTime = 1f;

    private Rigidbody playerRb; // oyuncunun rigidbody'si

    private void Awake()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>(); // oyuncunun rigidbody'sini al
    }

    public virtual void Fire()
    {
            if (ammo > 0) // e�er cephane varsa
            {
                ammo--; // cephane say�s�n� azalt
                audioSource.PlayOneShot(fireSound); // ate�leme sesini �al
                playerRb.AddForce(-transform.forward * recoil, ForceMode.Impulse); // oyuncuyu geriye do�ru it
                Instantiate(muzzleEffect, firePoint.transform.position, firePoint.transform.rotation); // muzzle efektini olu�tur

                Destroy(muzzleEffect, 0.2f); // muzzle efektini 0.5 saniye sonra yok et

            }
            else // e�er cephane yoksa
            {
                Destroy(gameObject); // silah� yok et
            }
        }
    }