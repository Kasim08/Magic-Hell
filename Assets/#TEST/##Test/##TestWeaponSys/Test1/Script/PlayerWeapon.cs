using UnityEngine;
using System.Collections;
public class PlayerWeapon : MonoBehaviour
{
    public float pickUpRange = 5f; // silahý alabileceði mesafe
    public Transform weaponHolder; // silahýn tutulacaðý yer
    public KeyCode pickUpKey = KeyCode.E; // silahý almak için basýlacak tuþ
    public KeyCode fireKey = KeyCode.Mouse0; // silahý ateþlemek için basýlacak tuþ

    bool isFiring = false; // ateþlenip ateþlenmediðini tutan deðiþken
    private Weapon currentWeapon; // oyuncunun elindeki silah


    private void Update()
    {
        if (Input.GetKeyDown(pickUpKey)) // eðer silahý almak için tuþa basýldýysa
        {
            if(currentWeapon == null) { PickUpWeapon(); }
            else { DropWeapon(); }
        }
        
        if (Input.GetKeyDown(fireKey) && !isFiring) // eðer silahý ateþlemek için tuþa basýldýysa
        {
            StartCoroutine(FireRate()); // ateþleme hýzý coroutine'ini baþlat
        }
    }

    private void PickUpWeapon()
    {
        Weapon closestWeapon = null; // en yakýn silah
        float minDistance = Mathf.Infinity; // minimum mesafe
        Weapon[] weapons = FindObjectsOfType<Weapon>(); // sahnedeki tüm silahlarý al
        foreach (Weapon weapon in weapons) // her silah için
        {
            float distance = Vector3.Distance(transform.position, weapon.transform.position); // mesafeyi hesapla
            if (distance < minDistance) // eðer mesafe minimum mesafeden küçükse
            {
                minDistance = distance; // minimum mesafeyi güncelle
                closestWeapon = weapon; // en yakýn silahý belirle
            }
        }
        if (closestWeapon != null && minDistance <= pickUpRange) // eðer en yakýn silah varsa ve mesafe alabileceðimiz mesafeden küçükse veya eþitse
        {
            if (currentWeapon != null) // eðer elimizde bir silah varsa
            {
                DropWeapon(); // silahý býrakma fonksiyonunu çaðýr
            }
            currentWeapon = closestWeapon; // elindeki silahý güncelle
            currentWeapon.transform.parent = weaponHolder; // silahýn ebeveynini ayarla
            currentWeapon.transform.position = weaponHolder.position; // silahýn pozisyonunu ayarla
            currentWeapon.transform.rotation = weaponHolder.rotation; // silahýn rotasyonunu ayarla
            //currentWeapon.GetComponent<Rigidbody>().isKinematic = true; // silahýn rigidbody'sini kinematik yap
            currentWeapon.GetComponent<Rigidbody>().useGravity = false; // silahýn rigidbody'sini kinematik yap
            currentWeapon.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void DropWeapon()
    {
        if (currentWeapon != null) // eðer elimizde bir silah varsa
        {
            currentWeapon.transform.parent = null; // silahýn ebeveynini kaldýr
            currentWeapon.transform.position += transform.forward * 2f; // silahýn pozisyonunu ileri doðru kaydýr
            //currentWeapon.GetComponent<Rigidbody>().isKinematic = false; // silahýn rigidbody'sinin kinematikliðini kapat
            currentWeapon.GetComponent<Rigidbody>().useGravity = true;
            currentWeapon.GetComponent<BoxCollider>().isTrigger = false;
            currentWeapon = null; // elindeki silahý sýfýrla
        }
    }
    IEnumerator FireRate()
    {
        isFiring = true; // ateþleniyor olarak ayarla
        if (currentWeapon != null) // eðer elimizde bir silah varsa
        {
            currentWeapon.Fire();
            yield return new WaitForSeconds(currentWeapon.fireRateTime); // ateþleme hýzýna göre bekle
        }
        isFiring = false; // ateþlenmiyor olarak ayarla
    }
}
