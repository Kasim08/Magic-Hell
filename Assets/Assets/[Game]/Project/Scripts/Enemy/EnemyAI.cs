using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // NavMeshAgent bileþenine eriþmek için deðiþken
    private NavMeshAgent agent;

    // Oyuncu objesine eriþmek için deðiþken
    private GameObject player;

    private void Awake()
    {
        // NavMeshAgent bileþenini al
        agent = GetComponent<NavMeshAgent>();
        // Oyuncu objesini singleton ile al
        player = Base.instance.gameObject;
    }

    void Start()
    {
        // NavMeshAgent bileþenini devre dýþý býrak
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        // NavMeshAgent bileþenini etkinleþtir
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }

    void Update()
    {
        // Oyuncunun pozisyonunu al
        Vector3 target = GetPlayerPosition();

        // Düþmanýn hedef noktasýný ayarla
        agent.SetDestination(target);
    }

    // Oyuncunun pozisyonunu döndüren fonksiyon
    Vector3 GetPlayerPosition()
    {
        // Oyuncunun transform bileþeninden pozisyonu al
        return player.transform.position;
    }
    // Base'e çarptýðýnda çaðrýlan fonksiyon
    void OnTriggerEnter(Collider other)
    {
        // Eðer çarptýðý obje base ise
        if (other.gameObject == Base.instance.gameObject)
        {
            // Objeyi yok et
            Destroy(gameObject);
        }
    }
}
