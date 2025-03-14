using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaterRise : MonoBehaviour
{
    [SerializeField] private float baseRiseSpeed = 0.5f; // Temel y�kselme h�z�
    [SerializeField] private float lowerSpeed = 0.3f;    // Su al�alma h�z�
    [SerializeField] private float maxHeight = 10f;        // Su seviyesinin ula�abilece�i maksimum y�kseklik
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
        // Delik say�s�na ba�l� olarak y�kselme h�z�
        float riseSpeed = baseRiseSpeed + (holeCount * 0.2f);

        if (holeCount == 0 && transform.position.y > startY)
        {
            // Delik yoksa su a�a��ya insin
            float newY = transform.position.y - lowerSpeed * Time.deltaTime;
            if (newY < startY) newY = startY;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else if (holeCount > 0 && transform.position.y < maxHeight)
        {
            // A��k delik varsa, su y�kseliyor
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
        Debug.Log("OnTriggerEnter2D tetiklendi, �arp��an obje: " + collision.gameObject.name);

        if (collision.CompareTag(playerTag))
        {
            Debug.Log("Su oyuncuya de�di! Men�ye d�n�l�yor...");
            SceneManager.LoadScene("Menu");
        }
    }
}
