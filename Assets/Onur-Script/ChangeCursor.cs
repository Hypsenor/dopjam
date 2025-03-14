using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D hammerCursor;  // Çekiç görseli

    void Start()
    {
        // Fareyi çekiç görseliyle deðiþtir
        Cursor.SetCursor(hammerCursor, Vector2.zero, CursorMode.ForceSoftware);

        // Fareyi her zaman görünür tut
        Cursor.visible = true;

        // Fareyi ekran içinde tut
        Cursor.lockState = CursorLockMode.None;
    }

    // Oyun sonunda fareyi eski haline getirmek isterseniz
    void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Varsayýlan cursor'a dön
    }
}
