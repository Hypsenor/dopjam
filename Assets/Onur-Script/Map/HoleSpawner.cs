using UnityEngine;
using System.Collections;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject holePrefab;          // Delik prefabý
    [SerializeField] private GameObject plugPrefab;          // Týpa prefabý
    [SerializeField] private Transform[] spawnPoints;        // Deliklerin spawnlanacaðý noktalar
    [SerializeField] private Transform[] plugSpawnPoints;    // Týpa spawn noktalarý
    [SerializeField] private float spawnInterval = 5f;       // Spawn aralýðý
    [SerializeField] private int maxHoles = 5;               // Ayný anda maksimum delik sayýsý
    [SerializeField] private WaterRise waterRise;            // Su scriptine referans
    [SerializeField] private PlugManager plugManager;        // PlugManager referansý

    [SerializeField] private int holesPerSpawn = 1;          // Her spawn olayýnda kaç delik oluþturulacak
    [SerializeField] private float spawnCountIncreaseInterval = 30f; // Spawn sayýsýnýn artacaðý süre
    [SerializeField] private int maxHolesPerSpawn = 3;       // Spawnlanabilecek maksimum delik sayýsý

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

            // Oyun ilerledikçe ayný anda oluþabilecek maksimum delik sayýsýný artýrabilirsiniz.
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
                spawnIndex = Random.Range(0, spawnPoints.Length); // Rastgele bir index seç
            } while (spawnIndex == lastSpawnIndex); 
        }
        else
        {
            spawnIndex = 0;
        }
        lastSpawnIndex = spawnIndex;

        Transform spawnPoint = spawnPoints[spawnIndex];
        GameObject holeObj = Instantiate(holePrefab, spawnPoint.position, Quaternion.identity);

     
        Hole holeScript = holeObj.GetComponent<Hole>();
        if (holeScript != null)
        {
            holeScript.SetSpawner(this);
            holeScript.SetPlugManager(plugManager); 
        }

        currentHoles++;
        if (waterRise != null)
        {
            waterRise.SetHoleCount(currentHoles);
        }

        // Delik oluþtuðunda rastgele bir noktada týpa spawnla
        SpawnPlug();
    }

    void SpawnPlug()
    {
        if (plugSpawnPoints == null || plugSpawnPoints.Length == 0)
        {
            Debug.LogError("Plug spawn points array is empty! Lütfen spawn noktalarýný atayýn.");
            return;
        }

        // Rastgele bir plug spawn noktasý seç
        int plugSpawnIndex = Random.Range(0, plugSpawnPoints.Length);

        Transform plugSpawnPoint = plugSpawnPoints[plugSpawnIndex];
        Instantiate(plugPrefab, plugSpawnPoint.position, Quaternion.identity); 
    }

    public void HoleFixed()
    {
        currentHoles = Mathf.Max(0, currentHoles - 1);
        if (waterRise != null)
        {
            waterRise.SetHoleCount(currentHoles);
        }
    }
}
