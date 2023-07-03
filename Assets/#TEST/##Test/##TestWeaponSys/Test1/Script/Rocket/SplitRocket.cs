using UnityEngine;

public class SplitRocket : Rocket
{
    public float splitTime = 1f; // split olma süresi
    public int splitCount = 3; // kaç parçaya ayrýlacaðý
    public GameObject splitRocketPrefab; // parçalanan roketin prefabý
    public Transform[] spawnPoints; // parçalanan roketlerin oluþacaðý noktalar

    private float timer; // zamanlayýcý

    private void Start()
    {
        timer = splitTime; // zamanlayýcýyý baþlat
    }

    private void Update()
    {
        timer -= Time.deltaTime; // zamanlayýcýyý güncelle
        if (timer <= 0) // eðer zamanlayýcý sýfýra ulaþtýysa
        {
            Split(); // split fonksiyonunu çaðýr
        }
    }

    private void Split()
    {
        for (int i = 0; i < spawnPoints.Length && i < splitCount; i++) // parça sayýsý kadar veya nokta sayýsý kadar döngü yap
        {
            GameObject splitRocket = Instantiate(splitRocketPrefab, spawnPoints[i].position, spawnPoints[i].rotation); // parçalanan roketi belirlenen noktada oluþtur
            // apply a random force to the split rocket
            splitRocket.GetComponent<Rigidbody>().AddForce(transform.forward * speed + Random.insideUnitSphere * 10f); // parçalanan rokete rastgele bir kuvvet uygula
        }
        Destroy(gameObject); // kendini yok et
    }
}
