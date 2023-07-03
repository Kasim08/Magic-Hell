using UnityEngine;
using System.Collections;
public class PlayerWeapon : MonoBehaviour
{
    public float pickUpRange = 5f; // silah� alabilece�i mesafe
    public Transform weaponHolder; // silah�n tutulaca�� yer
    public KeyCode pickUpKey = KeyCode.E; // silah� almak i�in bas�lacak tu�
    public KeyCode fireKey = KeyCode.Mouse0; // silah� ate�lemek i�in bas�lacak tu�

    bool isFiring = false; // ate�lenip ate�lenmedi�ini tutan de�i�ken
    private Weapon currentWeapon; // oyuncunun elindeki silah


    private void Update()
    {
        if (Input.GetKeyDown(pickUpKey)) // e�er silah� almak i�in tu�a bas�ld�ysa
        {
            if(currentWeapon == null) { PickUpWeapon(); }
            else { DropWeapon(); }
        }
        
        if (Input.GetKeyDown(fireKey) && !isFiring) // e�er silah� ate�lemek i�in tu�a bas�ld�ysa
        {
            StartCoroutine(FireRate()); // ate�leme h�z� coroutine'ini ba�lat
        }
    }

    private void PickUpWeapon()
    {
        Weapon closestWeapon = null; // en yak�n silah
        float minDistance = Mathf.Infinity; // minimum mesafe
        Weapon[] weapons = FindObjectsOfType<Weapon>(); // sahnedeki t�m silahlar� al
        foreach (Weapon weapon in weapons) // her silah i�in
        {
            float distance = Vector3.Distance(transform.position, weapon.transform.position); // mesafeyi hesapla
            if (distance < minDistance) // e�er mesafe minimum mesafeden k���kse
            {
                minDistance = distance; // minimum mesafeyi g�ncelle
                closestWeapon = weapon; // en yak�n silah� belirle
            }
        }
        if (closestWeapon != null && minDistance <= pickUpRange) // e�er en yak�n silah varsa ve mesafe alabilece�imiz mesafeden k���kse veya e�itse
        {
            if (currentWeapon != null) // e�er elimizde bir silah varsa
            {
                DropWeapon(); // silah� b�rakma fonksiyonunu �a��r
            }
            currentWeapon = closestWeapon; // elindeki silah� g�ncelle
            currentWeapon.transform.parent = weaponHolder; // silah�n ebeveynini ayarla
            currentWeapon.transform.position = weaponHolder.position; // silah�n pozisyonunu ayarla
            currentWeapon.transform.rotation = weaponHolder.rotation; // silah�n rotasyonunu ayarla
            //currentWeapon.GetComponent<Rigidbody>().isKinematic = true; // silah�n rigidbody'sini kinematik yap
            currentWeapon.GetComponent<Rigidbody>().useGravity = false; // silah�n rigidbody'sini kinematik yap
            currentWeapon.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void DropWeapon()
    {
        if (currentWeapon != null) // e�er elimizde bir silah varsa
        {
            currentWeapon.transform.parent = null; // silah�n ebeveynini kald�r
            currentWeapon.transform.position += transform.forward * 2f; // silah�n pozisyonunu ileri do�ru kayd�r
            //currentWeapon.GetComponent<Rigidbody>().isKinematic = false; // silah�n rigidbody'sinin kinematikli�ini kapat
            currentWeapon.GetComponent<Rigidbody>().useGravity = true;
            currentWeapon.GetComponent<BoxCollider>().isTrigger = false;
            currentWeapon = null; // elindeki silah� s�f�rla
        }
    }
    IEnumerator FireRate()
    {
        isFiring = true; // ate�leniyor olarak ayarla
        if (currentWeapon != null) // e�er elimizde bir silah varsa
        {
            currentWeapon.Fire();
            yield return new WaitForSeconds(currentWeapon.fireRateTime); // ate�leme h�z�na g�re bekle
        }
        isFiring = false; // ate�lenmiyor olarak ayarla
    }
}
