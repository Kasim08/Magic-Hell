using UnityEngine;

public class HomingRocket : Rocket
{
    public float turnSpeed = 10f; // roketin dönüþ hýzý
    public Transform target; // roketin hedefi

    private void Start()
    {
        FindTarget(); // hedef bul
    }

    private void Update()
    {
        if (target != null) // eðer hedef varsa
        {
            Turn(); // dönüþ fonksiyonunu çaðýr
        }
    }

    private void FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // sahnedeki tüm düþmanlarý al
        if (enemies.Length > 0) // eðer düþman varsa
        {
            float minDistance = Mathf.Infinity; // minimum mesafeyi sonsuz yap
            foreach (Enemy enemy in enemies) // her düþman için
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position); // mesafeyi hesapla
                if (distance < minDistance) // eðer mesafe minimum mesafeden küçükse
                {
                    minDistance = distance; // minimum mesafeyi güncelle
                    target = enemy.transform; // hedefi belirle
                }
            }
        }
    }

    private void Turn()
    {
        Vector3 direction = target.position - transform.position; // hedefe doðru yönel
        Quaternion lookRotation = Quaternion.LookRotation(direction); // bakýþ rotasyonunu hesapla
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime); // yavaþ yavaþ dön
    }
}