using UnityEngine;

public class Base : MonoBehaviour
{
    public static Base instance;

    void Awake()
    {
        // Singleton de�i�kenini ayarla
        instance = this;
    }
}
