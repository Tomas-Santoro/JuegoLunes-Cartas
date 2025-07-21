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
            { "brasil", new Enemigo("Brasil", 5, 5) },
            { "francia", new Enemigo("Francia", 6, 6) },
            { "japon", new Enemigo("Japón", 7, 7) },
            { "eeuu", new Enemigo("Estados Unidos", 4, 4) },
            { "sudafrica", new Enemigo("Sudáfrica", 3, 3) },
            //{ "nodo4", new Enemigo("Carlos", 7, 3) },
        };
    }

    public void ResetearJuego()
    {
        /*nodoDerrotado = null;
        nodoActual = null;
        ultimaPosicionJugador = Vector3.zero;
        batallasGanadas = 0;
        MapaManager.arbolPoderEnemigos = null;

        InicializarEnemigos(); // Reinicia enemigos por nodo*/
        nodoDerrotado = null;
        nodoActual = null;
        ultimaPosicionJugador = Vector3.zero;
        batallasGanadas = 0;

        enemigosPorNodo = new Dictionary<string, Enemigo>
        {
            { "brasil", new Enemigo("Brasil", 5, 5) },
            { "francia", new Enemigo("Francia", 6, 6) },
            { "japon", new Enemigo("Japón", 7, 7) },
            { "eeuu", new Enemigo("Estados Unidos", 4, 4) },
            { "sudafrica", new Enemigo("Sudáfrica", 3, 3) },
        };

        // Resetear árbol también
        MapaManager.arbolPoderEnemigos = null;

        Debug.Log("♻️ Datos del juego reseteados.");
    }

}
