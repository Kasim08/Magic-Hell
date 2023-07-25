using UnityEngine;

public class Base : MonoBehaviour
{
    public static Base instance;

    void Awake()
    {
        // Singleton deðiþkenini ayarla
        instance = this;
    }
}
