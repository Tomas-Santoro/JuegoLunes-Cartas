using UnityEngine;
using UnityEngine.SceneManagement; // Agregamos esto para cargar escena directamente desde acá

public class MovimientoJugador : MonoBehaviour
{
    public bool estaMoviendose = false;
    public float velocidad = 5f;
    private Vector3 objetivo;
    private bool tieneObjetivo = false; // Para saber si tiene un destino

    private bool cambiarEscenaCuandoTermine = false; // ✅ NUEVO

    void Update()
    {
        if (tieneObjetivo && Vector3.Distance(transform.position, objetivo) > 0.1f)
        {
            estaMoviendose = true;
            transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);
        }
        else
        {
            if (estaMoviendose) // Solamente si estaba moviéndose antes
            {
                estaMoviendose = false;

                if (cambiarEscenaCuandoTermine)
                {
                    SceneManager.LoadScene("Duelo");
                }
            }
        }
    }

    public void MoverJugador(Transform destino, bool cambiarEscena = false)
    {
        if (destino != null)
        {
            objetivo = destino.position;
            tieneObjetivo = true;
            cambiarEscenaCuandoTermine = cambiarEscena; // ✅ Guardamos si hay que cambiar de escena
        }
    }
}
