using UnityEngine;

public class AdvancedLaser : Laser
{
    public override void Fire()
    {
        base.Fire(); // ana s�n�f�n fire fonksiyonunu �a��r
        RaycastHit hit; // �arp��ma bilgisi
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit)) // lazerin ��k�� noktas�ndan ileri do�ru bir ���n at
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>(); // d��man var m� kontrol et
            if (enemy != null) // e�er varsa
            {
                enemy.TakeDamage(damage); // d��mana hasar ver
            }
            else // e�er yoksa
            {
                Vector3 reflectDirection = Vector3.Reflect(firePoint.transform.forward, hit.normal); // yans�ma y�n�n� hesapla
                if (Physics.Raycast(hit.point, reflectDirection, out hit)) // yans�ma noktas�ndan yans�ma y�n�ne do�ru bir ���n at
                {
                    enemy = hit.collider.GetComponent<Enemy>(); // d��man var m� kontrol et
                    if (enemy != null) // e�er varsa
                    {
                        enemy.TakeDamage(damage); // d��mana hasar ver
                    }
                }
                lineRenderer.SetPosition(0, firePoint.transform.position); // lazerin ba�lang�� noktas�n� ayarla
                lineRenderer.SetPosition(1, hit.point); // lazerin biti� noktas�n� ayarla
            }
        }
        else // e�er ���n bir �eye �arpmazsa
        {
            lineRenderer.SetPosition(0, firePoint.transform.position); // lazerin ba�lang�� noktas�n� ayarla
            lineRenderer.SetPosition(1, firePoint.transform.position + firePoint.transform.forward * 100f); // lazerin biti� noktas�n� ayarla
        }
    }
}
