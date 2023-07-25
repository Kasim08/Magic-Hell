using UnityEngine;
using UnityEngine.UI;

public class BaseArea : MonoBehaviour
{
    // Base alanýný tanýmlayan script
    public bool isBase; // Base alaný olup olmadýðýný belirten bool deðiþkeni
    public Transform baseTransform; // Base alanýnýn transformu

    private void Start()
    {
        isBase = false; // Baþlangýçta base alaný deðil
    }

    private void OnTriggerEnter(Collider other)
    {
        // Base alanýna yaklaþtýðýnda
        if (other.CompareTag("Player")) // Eðer yaklaþan oyuncu ise
        {
            isBase = true; // Base alaný olur
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Base alanýndan uzaklaþtýðýnda
        if (other.CompareTag("Player")) // Eðer uzaklaþan oyuncu ise
        {
            isBase = false; // Base alaný olmaz
        }
    }
}
