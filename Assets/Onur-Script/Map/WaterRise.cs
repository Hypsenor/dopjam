using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaterRise : MonoBehaviour
{
    [SerializeField] private float baseRiseSpeed = 0.5f; // Temel yükselme hýzý
    [SerializeField] private float lowerSpeed = 0.3f;    // Su alçalma hýzý
    [SerializeField] private float maxHeight = 10f;        // Su seviyesinin ulaþabileceði maksimum yükseklik
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
        // Delik sayýsýna baðlý olarak yükselme hýzý
        float riseSpeed = baseRiseSpeed + (holeCount * 0.2f);

        if (holeCount == 0 && transform.position.y > startY)
        {
            // Delik yoksa su aþaðýya insin
            float newY = transform.position.y - lowerSpeed * Time.deltaTime;
            if (newY < startY) newY = startY;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else if (holeCount > 0 && transform.position.y < maxHeight)
        {
            // Açýk delik varsa, su yükseliyor
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

    public void SetHoleCount(int count)
    {
        holeCount = count;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D tetiklendi, çarpýþan obje: " + collision.gameObject.name);

        if (collision.CompareTag(playerTag))
        {
            Debug.Log("Su oyuncuya deðdi! Menüye dönülüyor...");
            SceneManager.LoadScene("Menu");
        }
    }
}
