using UnityEngine;
using UnityEngine.UI;

public class BaseArea : MonoBehaviour
{
    // Base alan�n� tan�mlayan script
    public bool isBase; // Base alan� olup olmad���n� belirten bool de�i�keni
    public Transform baseTransform; // Base alan�n�n transformu

    private void Start()
    {
        isBase = false; // Ba�lang��ta base alan� de�il
    }

    private void OnTriggerEnter(Collider other)
    {
        // Base alan�na yakla�t���nda
        if (other.CompareTag("Player")) // E�er yakla�an oyuncu ise
        {
            isBase = true; // Base alan� olur
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Base alan�ndan uzakla�t���nda
        if (other.CompareTag("Player")) // E�er uzakla�an oyuncu ise
        {
            isBase = false; // Base alan� olmaz
        }
    }
}
