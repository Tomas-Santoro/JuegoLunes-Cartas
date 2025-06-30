using UnityEngine;
using System.Collections.Generic;

public class MapaManager : MonoBehaviour
{
    public MovimientoJugador jugador; // Referencia al script de movimiento
    private Nodo nodoDestinoPendiente;

    private GrafoMapa grafo;
    private Nodo nodoActual;

    public List<GameObject> nodosEnemigosGO = new List<GameObject>();

    void Start()
    {
        Debug.Log("MapaManager Start ejecutado.");

        grafo = new GrafoMapa();
        List<Nodo> nodosEnemigos = new List<Nodo>();

        // Crear nodos enemigos y agregarlos al grafo
        foreach (GameObject go in nodosEnemigosGO)
        {
            Nodo nuevoNodo = new Nodo(go.name, go.transform.position, go);
            grafo.AgregarNodo(nuevoNodo);
            nodosEnemigos.Add(nuevoNodo);
        }

        // Conectar todos los nodos enemigos entre sí (bidireccional con peso)
        for (int i = 0; i < nodosEnemigos.Count - 1; i++)
        {
            grafo.ConectarNodos(nodosEnemigos[i], nodosEnemigos[i + 1]);
        }

        // Si existe un nodo derrotado, eliminarlo
        if (DatosJuego.instancia != null && DatosJuego.instancia.nodoDerrotado != null)
        {
            Nodo nodoEliminar = DatosJuego.instancia.nodoDerrotado;
            grafo.nodos.Remove(nodoEliminar);

            if (nodoEliminar.go != null)
                Destroy(nodoEliminar.go);

            Debug.Log($"🗑️ Nodo derrotado eliminado: {nodoEliminar.nombre}");
            DatosJuego.instancia.nodoDerrotado = null;
        }

        nodoActual = null;

        // Re-asignación de nodos después de inicializar grafo
        foreach (GameObject go in nodosEnemigosGO)
        {
            BotonNodo botonNodo = go.GetComponent<BotonNodo>();
            Nodo nodoEncontrado = grafo.nodos.Find(n => n.go == go);

            if (botonNodo != null)
            {
                if (nodoEncontrado != null)
                {
                    botonNodo.nodo = nodoEncontrado;
                    Debug.Log($"🔄 Nodo re-asignado: {nodoEncontrado.nombre} a {go.name}");
                }
                else
                {
                    Debug.LogError($"❌ No se encontró Nodo asociado al GameObject {go.name} en el grafo.");
                }
            }
            else
            {
                Debug.LogError($"❌ BotonNodo no encontrado en {go.name}");
            }
        }
    }

    void Update()
    {
        if (nodoDestinoPendiente != null && !jugador.estaMoviendose)
        {
            PanelOpciones panelOpciones = FindObjectOfType<PanelOpciones>();
            if (panelOpciones != null)
            {
                panelOpciones.ConfigurarObjetivo(nodoDestinoPendiente);
                panelOpciones.dialogPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("❌ PanelOpciones no encontrado en escena.");
            }

            nodoDestinoPendiente = null;
        }
    }

    // Método para mover al jugador a un nodo conectado
    public void MoverJugadorANodo(Nodo destino)
    {
        if (nodoActual == null || nodoActual.conexiones.Exists(a => a.destino == destino))
        {
            nodoDestinoPendiente = destino;
            nodoActual = destino;

            if (DatosJuego.instancia != null)
            {
                DatosJuego.instancia.nodoActual = nodoActual;
            }

            jugador.MoverJugador(destino.posicion, false);
        }
    }
}
