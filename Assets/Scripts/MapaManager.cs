using UnityEngine;
using System.Collections.Generic;

public class MapaManager : MonoBehaviour
{
    public MovimientoJugador jugador; // Referencia al script de movimiento
    private Nodo nodoDestinoPendiente; // ✅ Nodo al que me estoy moviendo y abriré panel luego

    private GrafoMapa grafo; // Grafo del mapa
    private Nodo nodoActual; // Nodo donde está el jugador ahora

    // Lista de GameObjects de los nodos enemigos
    public List<GameObject> nodosEnemigosGO = new List<GameObject>();

    void Start()
    {
        Debug.Log("MapaManager Start ejecutado.");

        grafo = new GrafoMapa();

        // Crear nodos enemigos y agregarlos al grafo
        List<Nodo> nodosEnemigos = new List<Nodo>();

        Dictionary<GameObject, Nodo> diccionarioNodos = new Dictionary<GameObject, Nodo>();

        // Crear nodos primero
        foreach (GameObject go in nodosEnemigosGO)
        {
            Nodo nuevoNodo = new Nodo(go.name, go.transform.position, go);
            grafo.AgregarNodo(nuevoNodo);
            diccionarioNodos[go] = nuevoNodo;
        }

        // Ahora conectar nodos desde el componente NodoVisual
        foreach (GameObject go in nodosEnemigosGO)
        {
            NodoVisual visual = go.GetComponent<NodoVisual>();
            if (visual != null)
            {
                Nodo nodoOrigen = diccionarioNodos[go];

                foreach (GameObject conectadoGO in visual.conexionesGO)
                {
                    if (conectadoGO != null && diccionarioNodos.ContainsKey(conectadoGO))
                    {
                        Nodo nodoDestino = diccionarioNodos[conectadoGO];

                        // Conexión bidireccional si no existe aún
                        if (!nodoOrigen.conexiones.Contains(nodoDestino))
                            grafo.ConectarNodos(nodoOrigen, nodoDestino);
                    }
                }
            }
        }

        // ✅ Conectar todos los nodos enemigos entre sí (bidireccional)
        for (int i = 0; i < nodosEnemigos.Count - 1; i++)
        {
            grafo.ConectarNodos(nodosEnemigos[i], nodosEnemigos[i + 1]);
            grafo.ConectarNodos(nodosEnemigos[i + 1], nodosEnemigos[i]); // conexión inversa
        }

        // ✅ Si existe un nodo derrotado, eliminarlo
        if (DatosJuego.instancia != null && DatosJuego.instancia.nodoDerrotado != null)
        {
            Nodo nodoEliminar = DatosJuego.instancia.nodoDerrotado;
            grafo.nodos.Remove(nodoEliminar);

            // Además destruí su GameObject en escena
            if (nodoEliminar.go != null)
                Destroy(nodoEliminar.go);

            Debug.Log($"🗑️ Nodo derrotado eliminado: {nodoEliminar.nombre}");

            // Limpiamos referencia
            DatosJuego.instancia.nodoDerrotado = null;
        }

        if (nodosEnemigos.Count > 0)
        {
            nodoActual = null; // ✅ Ahora inicia sin nodo actual
            // jugador.transform.position = nodoActual.posicion; // ❌ Quitamos esto
        }

        // ✅ Forzar re-asignación de nodos después de inicializar grafo
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
        // ✅ Cuando el jugador termine de moverse, abre el panel de opciones
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

            nodoDestinoPendiente = null; // ✅ Limpiamos destino pendiente
        }
    }

    // Método para mover al jugador a un nodo conectado
    public void MoverJugadorANodo(Nodo destino)
    {
        if (nodoActual == null)
        {
            // Si no hay nodoActual, intentamos encontrar el nodo más cercano
            nodoActual = grafo.nodos.Find(n => Vector3.Distance(n.posicion, jugador.transform.position) < 0.5f);
            if (nodoActual == null)
            {
                Debug.LogError(" nodoActual es null y no se encontró uno cercano.");
                return;
            }
        }

        if (nodoActual == destino)
        {
            Debug.Log(" Ya estás en el nodo destino.");
            return;
        }

        List<Nodo> camino = grafo.ObtenerCaminoMasCorto(nodoActual, destino);

        if (camino == null || camino.Count < 2)
        {
            Debug.LogWarning(" Camino no válido o ya estás en el destino.");
            return;
        }

        Debug.Log(" Iniciando movimiento del jugador a través del camino.");
        foreach (var paso in camino)
            Debug.Log("🟢 Paso: " + paso.nombre);

        nodoActual = destino;
        nodoDestinoPendiente = destino;

        if (DatosJuego.instancia != null)
            DatosJuego.instancia.nodoActual = nodoActual;

        StartCoroutine(MoverSecuencia(camino));
    }

    private System.Collections.IEnumerator MoverSecuencia(List<Nodo> camino)
    {
        Debug.Log("Iniciando secuencia de movimiento con " + camino.Count + " pasos");

        foreach (Nodo paso in camino)
        {
            Debug.Log(" Moviéndose a: " + paso.nombre);
            jugador.MoverJugador(paso.posicion, false);
            yield return new WaitUntil(() => jugador.estaMoviendose == false);
        }

        Debug.Log(" Secuencia de movimiento finalizada");
    }
}
