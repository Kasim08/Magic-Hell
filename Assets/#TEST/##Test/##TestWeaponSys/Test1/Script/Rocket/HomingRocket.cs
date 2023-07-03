using UnityEngine;

public class HomingRocket : Rocket
{
    public float turnSpeed = 10f; // roketin d�n�� h�z�
    public Transform target; // roketin hedefi

    private void Start()
    {
        FindTarget(); // hedef bul
    }

    private void Update()
    {
        if (target != null) // e�er hedef varsa
        {
            Turn(); // d�n�� fonksiyonunu �a��r
        }
    }

    private void FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // sahnedeki t�m d��manlar� al
        if (enemies.Length > 0) // e�er d��man varsa
        {
            float minDistance = Mathf.Infinity; // minimum mesafeyi sonsuz yap
            foreach (Enemy enemy in enemies) // her d��man i�in
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position); // mesafeyi hesapla
                if (distance < minDistance) // e�er mesafe minimum mesafeden k���kse
                {
                    minDistance = distance; // minimum mesafeyi g�ncelle
                    target = enemy.transform; // hedefi belirle
                }
            }
        }
    }

    private void Turn()
    {
        Vector3 direction = target.position - transform.position; // hedefe do�ru y�nel
        Quaternion lookRotation = Quaternion.LookRotation(direction); // bak�� rotasyonunu hesapla
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime); // yava� yava� d�n
    }
}