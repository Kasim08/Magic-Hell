using UnityEngine;

public class SplitRocket : Rocket
{
    public float splitTime = 1f; // split olma s�resi
    public int splitCount = 3; // ka� par�aya ayr�laca��
    public GameObject splitRocketPrefab; // par�alanan roketin prefab�
    public Transform[] spawnPoints; // par�alanan roketlerin olu�aca�� noktalar

    private float timer; // zamanlay�c�

    private void Start()
    {
        timer = splitTime; // zamanlay�c�y� ba�lat
    }

    private void Update()
    {
        timer -= Time.deltaTime; // zamanlay�c�y� g�ncelle
        if (timer <= 0) // e�er zamanlay�c� s�f�ra ula�t�ysa
        {
            Split(); // split fonksiyonunu �a��r
        }
    }

    private void Split()
    {
        for (int i = 0; i < spawnPoints.Length && i < splitCount; i++) // par�a say�s� kadar veya nokta say�s� kadar d�ng� yap
        {
            GameObject splitRocket = Instantiate(splitRocketPrefab, spawnPoints[i].position, spawnPoints[i].rotation); // par�alanan roketi belirlenen noktada olu�tur
            // apply a random force to the split rocket
            splitRocket.GetComponent<Rigidbody>().AddForce(transform.forward * speed + Random.insideUnitSphere * 10f); // par�alanan rokete rastgele bir kuvvet uygula
        }
        Destroy(gameObject); // kendini yok et
    }
}
