using System.Collections.Generic;
using UnityEngine;

public class DatosJuego : MonoBehaviour
{
    public static DatosJuego instancia;

    public Nodo nodoDerrotado;
    public Nodo nodoActual;

    public Vector3 ultimaPosicionJugador;
    public int batallasGanadas;
    public int batallasParaGanar;
    public bool resetearNodoAlInicio = false;

    public Dictionary<string, Enemigo> enemigosPorNodo = new Dictionary<string, Enemigo>(); 

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            InicializarEnemigos(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InicializarEnemigos()
    {
        enemigosPorNodo = new Dictionary<string, Enemigo>
        {
            { "brasil", new Enemigo("Lucas", 5, 1) },
            { "francia", new Enemigo("Martina", 6, 2) },
            { "japon", new Enemigo("Ernesto", 7, 3) },
            //{ "nodo4", new Enemigo("Carlos", 7, 3) },
        };
    }
}
