using UnityEngine;

public class DatosJuego : MonoBehaviour
{
    public static DatosJuego instancia;

    public Vector3 ultimaPosicionJugador;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita duplicados si vuelve a crearse
        }
    }
}
