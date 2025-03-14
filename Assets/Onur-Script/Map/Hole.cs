using UnityEngine;

public class Hole : MonoBehaviour
{
    private HoleSpawner spawner;

    // Spawner referansýný ayarlar
    public void SetSpawner(HoleSpawner spawner)
    {
        this.spawner = spawner;
    }

    // Fareyle týklanma iþlemi
    void OnMouseDown()
    {
        if (spawner != null)
        {
            spawner.HoleFixed();
        }
        Destroy(gameObject);
    }
}
