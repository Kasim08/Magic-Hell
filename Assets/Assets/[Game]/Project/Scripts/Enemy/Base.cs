using UnityEngine;

public class Base : MonoBehaviour
{
    // Oyuncu objesinin singleton de�i�keni
    public static Base instance;

    void Awake()
    {
        // Singleton de�i�kenini ayarla
        instance = this;
    }
}
