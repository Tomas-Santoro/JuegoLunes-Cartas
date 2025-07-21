using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAJugar : MonoBehaviour
{
    public void ReiniciarJuego()
    {
        if (DatosJuego.instancia != null)
        {
            DatosJuego.instancia.ResetearJuego();
        }

        SceneManager.LoadScene("SampleScene");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
