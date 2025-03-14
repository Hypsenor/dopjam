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
        if (holeCount == 0 && transform.position.y > startY)
        {
            // A��k delik yoksa, su belirledi�in lowerSpeed ile orijinal konuma iniyor.
            float newY = transform.position.y - lowerSpeed * Time.deltaTime;
            if (newY < startY) newY = startY;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else if (holeCount > 0 && transform.position.y < maxHeight)
        {
            // A��k delik varsa, su y�kselir. H�z, a��k delik say�s�na g�re artar.
            float riseSpeed = baseRiseSpeed + (holeCount * 0.2f);
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
