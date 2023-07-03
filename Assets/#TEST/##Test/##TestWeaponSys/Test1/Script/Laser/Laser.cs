using UnityEngine;

public class Laser : Weapon
{
    public float damage = 10f; // lazerin verdiði hasar
    public LineRenderer lineRenderer; // lazerin görüntüsü
    //public Transform firePoint; // lazerin çýkýþ noktasý

    public override void Fire()
    {
        base.Fire(); // ana sýnýfýn fire fonksiyonunu çaðýr
        RaycastHit hit; // çarpýþma bilgisi
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit)) // lazerin çýkýþ noktasýndan ileri doðru bir ýþýn at
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>(); // düþman var mý kontrol et
            if (enemy != null) // eðer varsa
            {
                enemy.TakeDamage(damage); // düþmana hasar ver
            }
            lineRenderer.SetPosition(0, firePoint.transform.position); // lazerin baþlangýç noktasýný ayarla
            lineRenderer.SetPosition(1, hit.point); // lazerin bitiþ noktasýný ayarla
        }
        else // eðer ýþýn bir þeye çarpmazsa
        {
            lineRenderer.SetPosition(0, firePoint.transform.position); // lazerin baþlangýç noktasýný ayarla
            lineRenderer.SetPosition(1, firePoint.transform.position + firePoint.transform.forward * 100f); // lazerin bitiþ noktasýný ayarla
        }
    }
}
