using UnityEngine;
using UnityEngine.UI;

public class BaseAreaPlayer : MonoBehaviour
{
    // Player'ý kontrol eden script
    public Image towerImage; // Kule koymak için kullanýlacak görsel
    public GameObject towerPrefab; // Kule objesinin prefabý
    public float distanceThreshold; // Base alanýna yakýn olma eþik deðeri

    private void Start()
    {
        towerImage.gameObject.SetActive(false); // Baþlangýçta görsel görünmez
    }

    private void Update()
    {
        RaycastHit hit; // Raycast için hit deðiþkeni
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Kameradan fare pozisyonuna doðru bir ýþýn oluþtur

        if (Physics.Raycast(ray, out hit)) // Eðer ýþýn bir þeyle çarpýþýrsa
        {
            BaseArea baseArea = hit.transform.GetComponent<BaseArea>(); // Çarpýþtýðý objeden BaseArea componentini al

            if (baseArea != null) // Eðer BaseArea componenti varsa
            {
                float distance = Vector3.Distance(transform.position, baseArea.baseTransform.position); // Player ile base alaný arasýndaki uzaklýðý hesapla

                if (distance < distanceThreshold) // Eðer uzaklýk eþik deðerinden küçükse
                {
                    towerImage.gameObject.SetActive(true); // Görseli görünür yap

                    if (Input.GetKeyDown(KeyCode.E)) // Eðer E tuþuna basýlýrsa
                    {
                        PlaceTower(baseArea.baseTransform); // Kule koyma fonksiyonunu çaðýrma
                    }
                }
                else // Eðer uzaklýk eþik deðerinden büyükse
                {
                    towerImage.gameObject.SetActive(false); // Görseli görünmez yap
                }
            }
            else // Eðer BaseArea componenti yoksa
            {
                towerImage.gameObject.SetActive(false); // Görseli görünmez yap
            }
        }
    }

    private void PlaceTower(Transform baseTransform)
    {
        // Kule koyma fonksiyonu
        Instantiate(towerPrefab, baseTransform.position, baseTransform.rotation); // Kule objesini base transformunun pozisyonuna ve rotasyonuna göre instantiate etme
    }
}
