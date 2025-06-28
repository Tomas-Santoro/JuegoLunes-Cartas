using UnityEngine;
using System.Collections.Generic;

public class MapaManager : MonoBehaviour
{
    public MovimientoJugador jugador; // Referencia al script de movimiento

    private GrafoMapa grafo; // Grafo del mapa
    private Nodo nodoActual; // Nodo donde está el jugador ahora

    // Lista de GameObjects de los nodos enemigos
    public List<GameObject> nodosEnemigosGO = new List<GameObject>();

    void Start()
    {
        grafo = new GrafoMapa();

        // Crear nodos enemigos y agregarlos al grafo
        List<Nodo> nodosEnemigos = new List<Nodo>();

        foreach (GameObject go in nodosEnemigosGO) //nodo mopdificado para que vay7a a asi mismo
        {
            Nodo nuevoNodo = new Nodo(go.name, go.transform.position, go);
            grafo.AgregarNodo(nuevoNodo);
            nodosEnemigos.Add(nuevoNodo);

            // ✅ Agregar conexión a sí mismo (para testing)
            nuevoNodo.AgregarConexion(nuevoNodo);

            // Enlazar al script BotonNodo
            BotonNodo botonNodo = go.GetComponent<BotonNodo>();
            if (botonNodo != null)
            {
                botonNodo.nodo = nuevoNodo;
            }
        }


        // Conectar todos los nodos enemigos entre sí (opcional, depende de tu mapa)
        for (int i = 0; i < nodosEnemigos.Count - 1; i++)
        {
            grafo.ConectarNodos(nodosEnemigos[i], nodosEnemigos[i + 1]);
        }

        // Nodo inicial: primer enemigo de la lista
        if (nodosEnemigos.Count > 0)
        {
            nodoActual = nodosEnemigos[0];
            jugador.transform.position = nodoActual.posicion;
        }
    }

    // Método para mover al jugador a un nodo conectado
    public void MoverJugadorANodo(Nodo destino)
    {
        if (nodoActual.conexiones.Contains(destino))
        {
            nodoActual = destino;
            jugador.MoverJugador(destino.posicion, true);
        }
    }
}
