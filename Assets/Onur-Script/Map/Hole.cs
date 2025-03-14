using UnityEngine;

public class Hole : MonoBehaviour
{
    private HoleSpawner spawner;

    public void SetSpawner(HoleSpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnMouseDown()
    {
        if (spawner != null)
        {
            spawner.HoleFixed();
        }
        Destroy(gameObject);
    }
}
