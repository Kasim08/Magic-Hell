using UnityEngine;
using UnityEngine.UI;

public class BaseAreaPlayer : MonoBehaviour
{
    // Player'� kontrol eden script
    public Image towerImage; // Kule koymak i�in kullan�lacak g�rsel
    public GameObject towerPrefab; // Kule objesinin prefab�
    public float distanceThreshold; // Base alan�na yak�n olma e�ik de�eri

    private void Start()
    {
        towerImage.gameObject.SetActive(false); // Ba�lang��ta g�rsel g�r�nmez
    }

    private void Update()
    {
        RaycastHit hit; // Raycast i�in hit de�i�keni
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Kameradan fare pozisyonuna do�ru bir ���n olu�tur

        if (Physics.Raycast(ray, out hit)) // E�er ���n bir �eyle �arp���rsa
        {
            BaseArea baseArea = hit.transform.GetComponent<BaseArea>(); // �arp��t��� objeden BaseArea componentini al

            if (baseArea != null) // E�er BaseArea componenti varsa
            {
                float distance = Vector3.Distance(transform.position, baseArea.baseTransform.position); // Player ile base alan� aras�ndaki uzakl��� hesapla

                if (distance < distanceThreshold) // E�er uzakl�k e�ik de�erinden k���kse
                {
                    towerImage.gameObject.SetActive(true); // G�rseli g�r�n�r yap

                    if (Input.GetKeyDown(KeyCode.E)) // E�er E tu�una bas�l�rsa
                    {
                        PlaceTower(baseArea.baseTransform); // Kule koyma fonksiyonunu �a��rma
                    }
                }
                else // E�er uzakl�k e�ik de�erinden b�y�kse
                {
                    towerImage.gameObject.SetActive(false); // G�rseli g�r�nmez yap
                }
            }
            else // E�er BaseArea componenti yoksa
            {
                towerImage.gameObject.SetActive(false); // G�rseli g�r�nmez yap
            }
        }
    }

    private void PlaceTower(Transform baseTransform)
    {
        // Kule koyma fonksiyonu
        Instantiate(towerPrefab, baseTransform.position, baseTransform.rotation); // Kule objesini base transformunun pozisyonuna ve rotasyonuna g�re instantiate etme
    }
}
