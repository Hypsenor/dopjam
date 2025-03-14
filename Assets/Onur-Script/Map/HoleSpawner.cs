using UnityEngine;
using System.Collections;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject holePrefab;         // Delik prefabý
    [SerializeField] private Transform[] spawnPoints;         // Deliklerin spawnlanacaðý noktalar
    [SerializeField] private float spawnInterval = 5f;          // Spawn aralýðý
    [SerializeField] private int maxHoles = 5;                  // Ayný anda maksimum delik sayýsý
    [SerializeField] private WaterRise waterRise;             // Su scriptine referans

    private int currentHoles = 0; // Açýk delik sayýsý
    private float gameTime = 0f;  // Geçen oyun süresi

    void Start()
    {
        StartCoroutine(SpawnHoles());
    }

    IEnumerator SpawnHoles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentHoles < maxHoles)
            {
                SpawnHole();
            }

            gameTime += spawnInterval;
            // Oyun ilerledikçe, örneðin 30 saniyeden sonra maksimum delik sayýsýný artýr
            if (gameTime > 30f && maxHoles < 10)
                maxHoles++;
        }
    }

    void SpawnHole()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points array is empty! Lütfen spawn noktalarýný atayýn.");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject holeObj = Instantiate(holePrefab, spawnPoint.position, Quaternion.identity);

        // Delik prefabýndaki Hole scriptine bu spawner referansýný atýyoruz
        Hole holeScript = holeObj.GetComponent<Hole>();
        if (holeScript != null)
        {
            holeScript.SetSpawner(this);
        }

        currentHoles++;
        if (waterRise != null)
        {
            waterRise.SetHoleCount(currentHoles);
        }
    }

    // Bir delik týklandýðýnda (tamir edildiðinde) çaðrýlýr
    public void HoleFixed()
    {
        currentHoles = Mathf.Max(0, currentHoles - 1);
        if (waterRise != null)
        {
            waterRise.SetHoleCount(currentHoles);
        }
    }
}
