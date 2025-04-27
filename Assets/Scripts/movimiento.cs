using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public bool estaMoviendose = false;
    public float velocidad = 5f;
    private Vector3 objetivo;

    void Update()
    {
        if (Vector3.Distance(transform.position, objetivo) > 0.1f)
        {
            estaMoviendose = true;
            transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);
        }
        else
        {
            estaMoviendose = false;
        }
    }

    public void MoverJugador(Transform destino)
    {
        if (destino != null)
        {
            objetivo = destino.position;
        }
    }
}

