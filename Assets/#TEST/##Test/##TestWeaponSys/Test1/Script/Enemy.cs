using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health = 100f; // düþmanýn caný
    public float knockBackForce = 10f; // düþmanýn geri itilme kuvveti
    public GameObject deathEffect; // düþmanýn ölüm efekti

    public void TakeDamage(float damage)
    {
        health -= damage; // düþmandan caný azalt
        if (health <= 0) // eðer caný sýfýr veya altýndaysa
        {
            Die(); // ölüm fonksiyonunu çaðýr
        }
    }

    public void KnockBack(Vector3 explosionPosition, float explosionRadius)
    {
        Vector3 direction = transform.position - explosionPosition; // patlamaya göre yönel
        float distance = direction.magnitude; // patlamaya olan mesafeyi hesapla
        float force = Mathf.Lerp(knockBackForce, 0, distance / explosionRadius); // mesafeye göre kuvveti ayarla
        GetComponent<Rigidbody>().AddForce(direction.normalized * force, ForceMode.Impulse); // düþmaný geri it
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation); // ölüm efektini oluþtur
        Destroy(gameObject); // kendini yok et
    }

    public void Freeze(float freezeTime)
    {
        StartCoroutine(FreezeCoroutine(freezeTime)); // dondurma iþlemini baþlat
    }

    private IEnumerator FreezeCoroutine(float freezeTime)
    {
        GetComponent<Rigidbody>().isKinematic = true; // düþmanýn fiziksel hareketini durdur
        GetComponent<Renderer>().material.color = Color.blue; // düþmanýn rengini mavi yap
        yield return new WaitForSeconds(freezeTime); // belirli bir süre bekle
        GetComponent<Rigidbody>().isKinematic = false; // düþmanýn fiziksel hareketini baþlat
        GetComponent<Renderer>().material.color = Color.red; // düþmanýn rengini kýrmýzý yap
    }
}