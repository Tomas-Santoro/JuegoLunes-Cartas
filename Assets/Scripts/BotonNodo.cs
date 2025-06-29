using UnityEngine;

public class BotonNodo : MonoBehaviour
{
    public Nodo nodo; // Referencia a este nodo
    private PanelOpciones panelOpciones;
    private MapaManager mapaManager;

    void Start()
    {
        panelOpciones = FindObjectOfType<PanelOpciones>();
        mapaManager = FindObjectOfType<MapaManager>();

        if (panelOpciones == null)
            Debug.LogError($"❌ PanelOpciones no encontrado en la escena por {gameObject.name}");
        else
            Debug.Log($"✅ PanelOpciones detectado correctamente por {gameObject.name}");

        if (mapaManager == null)
            Debug.LogError($"❌ MapaManager no encontrado en la escena por {gameObject.name}");
        else
            Debug.Log($"✅ MapaManager detectado correctamente por {gameObject.name}");
    }

    void OnMouseDown()
    {
        Debug.Log($"🖱️ Click detectado en {gameObject.name}");

        if (mapaManager != null && nodo != null)
        {
            mapaManager.MoverJugadorANodo(nodo); // ✅ Mueve el jugador al nodo clickeado
        }

        if (panelOpciones != null && nodo != null)
        {
            Debug.Log($"✅ Abriendo panel para nodo: {nodo.nombre}");
            panelOpciones.ConfigurarObjetivo(nodo);
            panelOpciones.dialogPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("❌ No se pudo abrir panel: panelOpciones o nodo es null");
        }
    }
}
