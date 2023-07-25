using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health = 100f; // d��man�n can�
    public float knockBackForce = 10f; // d��man�n geri itilme kuvveti
    public GameObject deathEffect; // d��man�n �l�m efekti

    public void TakeDamage(float damage)
    {
        health -= damage; // d��mandan can� azalt
        if (health <= 0) // e�er can� s�f�r veya alt�ndaysa
        {
            Die(); // �l�m fonksiyonunu �a��r
        }
    }

    public void KnockBack(Vector3 explosionPosition, float explosionRadius)
    {
        Vector3 direction = transform.position - explosionPosition; // patlamaya g�re y�nel
        float distance = direction.magnitude; // patlamaya olan mesafeyi hesapla
        float force = Mathf.Lerp(knockBackForce, 0, distance / explosionRadius); // mesafeye g�re kuvveti ayarla
        GetComponent<Rigidbody>().AddForce(direction.normalized * force, ForceMode.Impulse); // d��man� geri it
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation); // �l�m efektini olu�tur
        Destroy(gameObject); // kendini yok et
    }

    public void Freeze(float freezeTime)
    {
        StartCoroutine(FreezeCoroutine(freezeTime)); // dondurma i�lemini ba�lat
    }

    private IEnumerator FreezeCoroutine(float freezeTime)
    {
        GetComponent<Rigidbody>().isKinematic = true; // d��man�n fiziksel hareketini durdur
        GetComponent<Renderer>().material.color = Color.blue; // d��man�n rengini mavi yap
        yield return new WaitForSeconds(freezeTime); // belirli bir s�re bekle
        GetComponent<Rigidbody>().isKinematic = false; // d��man�n fiziksel hareketini ba�lat
        GetComponent<Renderer>().material.color = Color.red; // d��man�n rengini k�rm�z� yap
    }
}