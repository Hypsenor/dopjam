using UnityEngine;

public class Hole : MonoBehaviour
{
    private HoleSpawner spawner;

    // Spawner referans�n� ayarlar
    public void SetSpawner(HoleSpawner spawner)
    {
        this.spawner = spawner;
    }

    // Fareyle t�klanma i�lemi
    void OnMouseDown()
    {
        if (spawner != null)
        {
            spawner.HoleFixed();
        }
        Destroy(gameObject);
    }
}
