using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ammo = 10; // silahýn cephane sayýsý
    public float recoil = 5f; // silahýn geri tepme kuvveti
    public AudioSource audioSource; // silahýn ses kaynaðý
    public AudioClip fireSound; // silahýn ateþleme sesi
    public GameObject firePoint; // silahýn ateþleme noktasý
    public GameObject muzzleEffect; // silahýn muzzle efekti
    public float fireRateTime = 1f;

    private Rigidbody playerRb; // oyuncunun rigidbody'si

    private void Awake()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>(); // oyuncunun rigidbody'sini al
    }

    public virtual void Fire()
    {
            if (ammo > 0) // eðer cephane varsa
            {
                ammo--; // cephane sayýsýný azalt
                audioSource.PlayOneShot(fireSound); // ateþleme sesini çal
                playerRb.AddForce(-transform.forward * recoil, ForceMode.Impulse); // oyuncuyu geriye doðru it
                Instantiate(muzzleEffect, firePoint.transform.position, firePoint.transform.rotation); // muzzle efektini oluþtur

                Destroy(muzzleEffect, 0.2f); // muzzle efektini 0.5 saniye sonra yok et

            }
            else // eðer cephane yoksa
            {
                Destroy(gameObject); // silahý yok et
            }
        }
    }