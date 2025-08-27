using UnityEngine;
using System.Collections.Generic;

public class MapaManager : MonoBehaviour
{
    public MovimientoJugador jugador;
    private Nodo nodoDestinoPendiente;

    private GrafoMapa grafo;
    private Nodo nodoActual;

    public List<GameObject> nodosEnemigosGO = new List<GameObject>();
    public static ArbolABB arbolPoderEnemigos;

    void Start()
    {
        Debug.Log("MapaManager Start ejecutado.");

        grafo = new GrafoMapa();
        List<Nodo> nodosEnemigos = new List<Nodo>();
        Dictionary<GameObject, Nodo> diccionarioNodos = new Dictionary<GameObject, Nodo>();

        Dictionary<string, Enemigo> enemigosPorNodo = DatosJuego.instancia.enemigosPorNodo;

        if (enemigosPorNodo == null || enemigosPorNodo.Count == 0)
        {
            Debug.LogWarning("DatosJuego.enemigosPorNodo está vacío o null.");
        }

        // Inicializar el árbol solo si es null (evitar duplicado)
        if (arbolPoderEnemigos == null)
        {
            arbolPoderEnemigos = new ArbolABB();

            foreach (GameObject go in nodosEnemigosGO)
            {
                if (enemigosPorNodo != null && enemigosPorNodo.ContainsKey(go.name))
                {
                    Enemigo enemigo = enemigosPorNodo[go.name];
                    arbolPoderEnemigos.Insertar(enemigo);
                    Debug.Log($"🆕 Enemigo agregado al árbol: {enemigo.nombre} - Poder: {enemigo.poder}");
                }
            }
        }
        else
        {
            Debug.Log("🔁 Árbol ya existente, no se vuelve a cargar.");
        }

        // Crear nodos y grafo
        foreach (GameObject go in nodosEnemigosGO)
        {
            Nodo nuevoNodo = new Nodo(go.name, go.transform.position, go);

            if (enemigosPorNodo != null && enemigosPorNodo.ContainsKey(go.name))
            {
                nuevoNodo.enemigo = enemigosPorNodo[go.name];
                Debug.Log($"Enemigo asignado a {go.name}: {nuevoNodo.enemigo.nombre} (Poder: {nuevoNodo.enemigo.poder})");
            }
            else
            {
                Debug.LogWarning($"No se encontró enemigo para {go.name}");
            }

            grafo.AgregarNodo(nuevoNodo);
            diccionarioNodos[go] = nuevoNodo;
            nodosEnemigos.Add(nuevoNodo);
        }

        // Conexiones
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
                        if (!nodoOrigen.conexiones.Contains(nodoDestino))
                            grafo.ConectarNodos(nodoOrigen, nodoDestino);
                    }
                }
            }
        }

        // Eliminar nodo derrotado
        if (DatosJuego.instancia != null && DatosJuego.instancia.nodoDerrotado != null)
        {
            Nodo nodoEliminar = DatosJuego.instancia.nodoDerrotado;
            grafo.nodos.Remove(nodoEliminar);

            if (nodoEliminar.go != null)
                Destroy(nodoEliminar.go);

            Debug.Log($"Nodo derrotado eliminado: {nodoEliminar.nombre}");
            DatosJuego.instancia.nodoDerrotado = null;
        }

        if (nodosEnemigos.Count > 0)
            nodoActual = null;

        // Asignar nodos a botones
        foreach (GameObject go in nodosEnemigosGO)
        {
            BotonNodo botonNodo = go.GetComponent<BotonNodo>();
            Nodo nodoEncontrado = grafo.nodos.Find(n => n.go == go);

            if (botonNodo != null)
            {
                if (nodoEncontrado != null)
                {
                    botonNodo.nodo = nodoEncontrado;
                    Debug.Log($"Nodo re-asignado: {nodoEncontrado.nombre} a {go.name}");
                }
                else
                {
                    Debug.LogError($"No se encontró Nodo asociado a {go.name}");
                }
            }
            else
            {
                Debug.LogError($"BotonNodo no encontrado en {go.name}");
            }
        }

        // Debug ranking
        var enemigosDebug = arbolPoderEnemigos.ObtenerEnemigosOrdenados();
        foreach (var e in enemigosDebug)
        {
            Debug.Log($"🔎 ABB Enemigo: {e.nombre} - Poder: {e.poder}");
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
                Debug.LogError("PanelOpciones no encontrado en escena.");
            }

            nodoDestinoPendiente = null;
        }
    }

    public void MoverJugadorANodo(Nodo destino)
    {
        if (nodoActual == null)
        {
            nodoActual = grafo.nodos.Find(n => Vector3.Distance(n.posicion, jugador.transform.position) < 0.5f);
            if (nodoActual == null)
            {
                Debug.LogError("nodoActual es null y no se encontró uno cercano.");
                return;
            }
        }

        if (nodoActual == destino)
        {
            Debug.Log("Ya estás en el nodo destino.");
            return;
        }

        List<Nodo> camino = grafo.ObtenerCaminoMasCorto(nodoActual, destino);

        if (camino == null || camino.Count < 2)
        {
            Debug.LogWarning("Camino no válido o ya estás en el destino.");
            return;
        }

        Debug.Log("Iniciando movimiento del jugador:");
        foreach (var paso in camino)
            Debug.Log("Paso: " + paso.nombre);

        nodoActual = destino;
        nodoDestinoPendiente = destino;

        if (DatosJuego.instancia != null)
            DatosJuego.instancia.nodoActual = nodoActual;

        StartCoroutine(MoverSecuencia(camino));
    }

    private System.Collections.IEnumerator MoverSecuencia(List<Nodo> camino)
    {
        Debug.Log("Iniciando secuencia de movimiento...");

        foreach (Nodo paso in camino)
        {
            Debug.Log("➡️ Moviéndose a: " + paso.nombre);
            jugador.MoverJugador(paso.posicion, false);
            yield return new WaitUntil(() => jugador.estaMoviendose == false);
        }

        Debug.Log("Movimiento finalizado");
    }
}
