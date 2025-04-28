using UnityEngine;

public class DatosJuego : MonoBehaviour
{
    public static DatosJuego instancia;

    public Vector3 ultimaPosicionJugador;

    
    public int batallasGanadas = 0;
    public int batallasParaGanar = 2; // Ganás luego de 2 batallas

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
