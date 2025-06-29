using UnityEngine;

public class DatosJuego : MonoBehaviour
{
    public static DatosJuego instancia;
    public Nodo nodoDerrotado;
    public Nodo nodoActual;


    public Vector3 ultimaPosicionJugador;
    public int batallasGanadas;
    public int batallasParaGanar;

    public bool resetearNodoAlInicio = false; // ✅ agregada

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
