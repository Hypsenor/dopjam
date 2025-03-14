using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaterRise : MonoBehaviour
{
    [SerializeField] private float baseRiseSpeed = 0.5f; // Temel yükselme hýzý
    [SerializeField] private float maxHeight = 10f;        // Su seviyesi limiti
    [SerializeField] private Slider waterLevelSlider;      // UI slider referansý
    [SerializeField] private string playerTag = "Player";    // Oyuncu tag'ý

    private float startY;
    private int holeCount = 0; // Açýk delik sayýsý

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
        // Delik sayýsýna baðlý dinamik hýz
        float riseSpeed = baseRiseSpeed + (holeCount * 0.2f);

        if (holeCount == 0 && transform.position.y > startY)
        {
            // Açýk delik kalmadýysa su yavaþça geri çekilir
            transform.position += new Vector3(0, -baseRiseSpeed * Time.deltaTime, 0);
        }
        else if (transform.position.y < maxHeight)
        {
            // Açýk delik varsa su yükselir
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

    // Diðer scriptlerden çaðrýlarak açýk delik sayýsýný günceller
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
