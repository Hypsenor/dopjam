using UnityEngine;
using UnityEngine.UI;

public class PlugManager : MonoBehaviour
{
    public int plugCount = 0; // Oyuncunun sahip oldu�u t�pa say�s�
    public Text plugText; // UI'da g�stermek i�in

    void Start()
    {
        UpdatePlugUI();
    }

    public void AddPlug()
    {
        plugCount++;
        UpdatePlugUI();
    }

    public bool UsePlug()
    {
        if (plugCount > 0)
        {
            plugCount--;
            UpdatePlugUI();
            return true;
        }
        return false;
    }

    void UpdatePlugUI()
    {
        if (plugText != null)
        {
            plugText.text = "Plugs: " + plugCount;
        }
    }
}
