using UnityEngine;
using System.Collections;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject holePrefab;         // Delik prefab�
    [SerializeField] private Transform[] spawnPoints;         // Deliklerin spawnlanaca�� noktalar
    [SerializeField] private float spawnInterval = 5f;          // Spawn aral���
    [SerializeField] private int maxHoles = 5;                  // Ayn� anda maksimum delik say�s�
    [SerializeField] private WaterRise waterRise;             // Su scriptine referans

    [SerializeField] private int holesPerSpawn = 1;           // Her spawn olay�nda ka� delik olu�turulsun
    [SerializeField] private float spawnCountIncreaseInterval = 30f; // Bu s�reden sonra spawn say�s� artar
    [SerializeField] private int maxHolesPerSpawn = 3;          // Spawnlanabilecek maksimum delik say�s�

    private int currentHoles = 0; // A��k delik say�s�
    private float gameTime = 0f;  // Ge�en oyun s�resi
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

            // S�reye ba�l� olarak her spawn olay�nda olu�turulacak delik say�s�n� art�r
            if (gameTime >= spawnCountIncreaseInterval && holesPerSpawn < maxHolesPerSpawn)
            {
                holesPerSpawn++;
                // �ste�e ba�l�: gameTime'� s�f�rlayabilirsiniz.
            }

            // Belirlenen say�da delik spawnla
            for (int i = 0; i < holesPerSpawn; i++)
            {
                if (currentHoles < maxHoles)
                {
                    SpawnHole();
                }
            }

            gameTime += spawnInterval;

            // �ste�e ba�l�: Oyun ilerledik�e ayn� anda olu�abilecek maksimum delik say�s�n� art�rabilirsiniz.
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
            Debug.LogError("Spawn points array is empty! L�tfen spawn noktalar�n� atay�n.");
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

        // Delik prefab�ndaki Hole scriptine bu spawner referans�n� at�yoruz.
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

    // Delik t�kland���nda (tamir edildi�inde) �a�r�l�r.
    public void HoleFixed()
    {
        currentHoles = Mathf.Max(0, currentHoles - 1);
        if (waterRise != null)
        {
            waterRise.SetHoleCount(currentHoles);
        }
    }
}
