using UnityEngine;
using System.Collections;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject holePrefab;         // Delik prefab�
    [SerializeField] private Transform[] spawnPoints;         // Deliklerin spawnlanaca�� noktalar
    [SerializeField] private float spawnInterval = 5f;          // Spawn aral���
    [SerializeField] private int maxHoles = 5;                  // Ayn� anda maksimum delik say�s�
    [SerializeField] private WaterRise waterRise;             // Su scriptine referans

    private int currentHoles = 0; // A��k delik say�s�
    private float gameTime = 0f;  // Ge�en oyun s�resi

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
            // Oyun ilerledik�e, �rne�in 30 saniyeden sonra maksimum delik say�s�n� art�r
            if (gameTime > 30f && maxHoles < 10)
                maxHoles++;
        }
    }

    void SpawnHole()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points array is empty! L�tfen spawn noktalar�n� atay�n.");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject holeObj = Instantiate(holePrefab, spawnPoint.position, Quaternion.identity);

        // Delik prefab�ndaki Hole scriptine bu spawner referans�n� at�yoruz
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

    // Bir delik t�kland���nda (tamir edildi�inde) �a�r�l�r
    public void HoleFixed()
    {
        currentHoles = Mathf.Max(0, currentHoles - 1);
        if (waterRise != null)
        {
            waterRise.SetHoleCount(currentHoles);
        }
    }
}
