using UnityEngine;

public class Hole : MonoBehaviour
{
    private HoleSpawner spawner;
    private PlugManager plugManager;

    public void SetSpawner(HoleSpawner spawner)
    {
        this.spawner = spawner;
    }

    public void SetPlugManager(PlugManager plugManager)
    {
        this.plugManager = plugManager;
    }

    void OnMouseDown()
    {
        if (plugManager != null && plugManager.UsePlug())
        {
            spawner.HoleFixed();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Yetersiz týpa! Önce týpa toplamalýsýn.");
        }
    }
}
