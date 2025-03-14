using UnityEngine;

public class Plug : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlugManager plugManager = FindObjectOfType<PlugManager>();
            if (plugManager != null)
            {
                plugManager.AddPlug();
            }
            Destroy(gameObject);
        }
    }
}
