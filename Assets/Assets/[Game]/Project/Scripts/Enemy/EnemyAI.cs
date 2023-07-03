using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // NavMeshAgent bile�enine eri�mek i�in de�i�ken
    private NavMeshAgent agent;

    // Oyuncu objesine eri�mek i�in de�i�ken
    private GameObject player;

    private void Awake()
    {
        // NavMeshAgent bile�enini al
        agent = GetComponent<NavMeshAgent>();
        // Oyuncu objesini singleton ile al
        player = Base.instance.gameObject;
    }

    void Start()
    {
        // NavMeshAgent bile�enini devre d��� b�rak
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        // NavMeshAgent bile�enini etkinle�tir
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }

    void Update()
    {
        // Oyuncunun pozisyonunu al
        Vector3 target = GetPlayerPosition();

        // D��man�n hedef noktas�n� ayarla
        agent.SetDestination(target);
    }

    // Oyuncunun pozisyonunu d�nd�ren fonksiyon
    Vector3 GetPlayerPosition()
    {
        // Oyuncunun transform bile�eninden pozisyonu al
        return player.transform.position;
    }
    // Base'e �arpt���nda �a�r�lan fonksiyon
    void OnTriggerEnter(Collider other)
    {
        // E�er �arpt��� obje base ise
        if (other.gameObject == Base.instance.gameObject)
        {
            // Objeyi yok et
            Destroy(gameObject);
        }
    }
}
