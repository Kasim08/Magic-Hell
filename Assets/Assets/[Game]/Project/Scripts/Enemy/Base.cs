using UnityEngine;

public class Base : MonoBehaviour
{
    // Oyuncu objesinin singleton deðiþkeni
    public static Base instance;

    void Awake()
    {
        // Singleton deðiþkenini ayarla
        instance = this;
    }
}
