using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaterRise : MonoBehaviour
{
    [SerializeField] private float baseRiseSpeed = 0.5f; // Temel y�kselme h�z�
    [SerializeField] private float maxHeight = 10f;        // Su seviyesi limiti
    [SerializeField] private Slider waterLevelSlider;      // UI slider referans�
    [SerializeField] private string playerTag = "Player";    // Oyuncu tag'�

    private float startY;
    private int holeCount = 0; // A��k delik say�s�

    void Start()
    {
        startY = transform.position.y;
        if (waterLevelSlider != null)
        {
            waterLevelSlider.minValue = 0f;
            waterLevelSlider.maxValue = 100f;
            waterLevelSlider.value = 0f;
        }
    }

    void Update()
    {
        // Delik say�s�na ba�l� dinamik h�z
        float riseSpeed = baseRiseSpeed + (holeCount * 0.2f);

        if (holeCount == 0 && transform.position.y > startY)
        {
            // A��k delik kalmad�ysa su yava��a geri �ekilir
            transform.position += new Vector3(0, -baseRiseSpeed * Time.deltaTime, 0);
        }
        else if (transform.position.y < maxHeight)
        {
            // A��k delik varsa su y�kselir
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
        }

        UpdateSlider();
    }

    void UpdateSlider()
    {
        if (waterLevelSlider != null)
        {
            float progress = Mathf.Clamp01((transform.position.y - startY) / (maxHeight - startY));
            waterLevelSlider.value = progress * 100f;
        }
    }

    // Di�er scriptlerden �a�r�larak a��k delik say�s�n� g�nceller
    public void SetHoleCount(int count)
    {
        holeCount = count;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
