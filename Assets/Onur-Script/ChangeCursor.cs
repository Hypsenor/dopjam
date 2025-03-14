using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D hammerCursor;  // �eki� g�rseli

    void Start()
    {
        // Fareyi �eki� g�rseliyle de�i�tir
        Cursor.SetCursor(hammerCursor, Vector2.zero, CursorMode.ForceSoftware);

        // Fareyi her zaman g�r�n�r tut
        Cursor.visible = true;

        // Fareyi ekran i�inde tut
        Cursor.lockState = CursorLockMode.None;
    }

    // Oyun sonunda fareyi eski haline getirmek isterseniz
    void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Varsay�lan cursor'a d�n
    }
}
