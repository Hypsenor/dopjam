using UnityEngine;
using System.Collections;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject holePrefab;         // Delik prefabý
    [SerializeField] private Transform[] spawnPoints;         // Deliklerin spawnlanacaðý noktalar
    [SerializeField] private float spawnInterval = 5f;          // Spawn aralýðý
    [SerializeField] private int maxHoles = 5;                  // Ayný anda maksimum delik sayýsý
    [SerializeField] private WaterRise waterRise;             // Su scriptine referans

    [SerializeField] private int holesPerSpawn = 1;           // Her spawn olayýnda kaç delik oluþturulsun
    [SerializeField] private float spawnCountIncreaseInterval = 30f; // Bu süreden sonra spawn sayýsý artar
    [SerializeField] private int maxHolesPerSpawn = 3;          // Spawnlanabilecek maksimum delik sayýsý

    private int currentHoles = 0; // Açýk delik sayýsý
    private float gameTime = 0f;  // Geçen oyun süresi
    private int lastSpawnIndex = -1;

    void Start()
    {
        StartCoroutine(SpawnHoles());
    }

    IEnumerator SpawnHoles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Süreye baðlý olarak her spawn olayýnda oluþturulacak delik sayýsýný artýr
            if (gameTime >= spawnCountIncreaseInterval && holesPerSpawn < maxHolesPerSpawn)
            {
                holesPerSpawn++;
                // Ýsteðe baðlý: gameTime'ý sýfýrlayabilirsiniz.
            }

            // Belirlenen sayýda delik spawnla
            for (int i = 0; i < holesPerSpawn; i++)
            {
                if (currentHoles < maxHoles)
                {
                    SpawnHole();
                }
            }

            gameTime += spawnInterval;

            // Ýsteðe baðlý: Oyun ilerledikçe ayný anda oluþabilecek maksimum delik sayýsýný artýrabilirsiniz.
            if (gameTime > 60f && maxHoles < 10)
            {
                maxHoles++;
            }
        }
    }

    void SpawnHole()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points array is empty! Lütfen spawn noktalarýný atayýn.");
            return;
        }

        int spawnIndex = 0;
        if (spawnPoints.Length > 1)
        {
            do
            {
                spawnIndex = Random.Range(0, spawnPoints.Length);
            } while (spawnIndex == lastSpawnIndex);
        }
        else
        {
            spawnIndex = 0;
        }
        lastSpawnIndex = spawnIndex;

        Transform spawnPoint = spawnPoints[spawnIndex];
        GameObject holeObj = Instantiate(holePrefab, spawnPoint.position, Quaternion.identity);

        // Delik prefabýndaki Hole scriptine bu spawner referansýný atýyoruz.
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

    // Delik týklandýðýnda (tamir edildiðinde) çaðrýlýr.
    public void HoleFixed()
    {
        currentHoles = Mathf.Max(0, currentHoles - 1);
        if (waterRise != null)
        {
            waterRise.SetHoleCount(currentHoles);
        }
    }
}
