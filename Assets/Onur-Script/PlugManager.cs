using UnityEngine;
using UnityEngine.UI;

public class PlugManager : MonoBehaviour
{
    public int plugCount = 0; // Oyuncunun sahip olduðu týpa sayýsý
    public Text plugText; // UI'da göstermek için

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
