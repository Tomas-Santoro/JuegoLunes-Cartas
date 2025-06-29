using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas

public class MovimientoJugador : MonoBehaviour
{
    public bool estaMoviendose = false;
    public float velocidad = 5f;
    private Vector3 objetivo;
    private bool tieneObjetivo = false; // Si tiene un destino

    private bool cambiarEscenaCuandoTermine = false; // Cambiar escena al llegar

    void Start()
    {
        if (DatosJuego.instancia != null)
        {
            // ✅ Si usás resetearNodoAlInicio en DatosJuego
            if (DatosJuego.instancia.resetearNodoAlInicio)
            {
                // Buscar el MapaManager para colocar al jugador en el nodo inicial
                MapaManager mapa = FindObjectOfType<MapaManager>();
                if (mapa != null && mapa.nodosEnemigosGO.Count > 0)
                {
                    transform.position = mapa.nodosEnemigosGO[0].transform.position;
                }

                // Resetear variable para futuros combates
                DatosJuego.instancia.resetearNodoAlInicio = false;
            }
            else
            {
                // Posición guardada antes de entrar al duelo
                transform.position = DatosJuego.instancia.ultimaPosicionJugador;
            }
        }
    }

    void Update()
    {
        if (tieneObjetivo && Vector3.Distance(transform.position, objetivo) > 0.1f)
        {
            estaMoviendose = true;
            transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);
        }
        else
        {
            if (estaMoviendose) // Solo si estaba moviéndose antes
            {
                estaMoviendose = false;

                if (cambiarEscenaCuandoTermine)
                {
                    // ✅ Guarda la posición antes de cargar la nueva escena
                    if (DatosJuego.instancia != null)
                    {
                        DatosJuego.instancia.ultimaPosicionJugador = transform.position;
                    }

                    SceneManager.LoadScene("Duelo");
                }
            }
        }
    }

    public void MoverJugador(Vector3 destino, bool cambiarEscena = false)
    {
        objetivo = destino;
        tieneObjetivo = true;
        cambiarEscenaCuandoTermine = cambiarEscena;
    }
}
