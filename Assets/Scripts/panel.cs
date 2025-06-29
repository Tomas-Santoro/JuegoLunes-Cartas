using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelOpciones : MonoBehaviour
{
    public Button btnAtacar;
    public Button btnCancelar;
    public GameObject dialogPanel;

    private Nodo targetNodo; // ✅ Referencia al Nodo seleccionado
    private MapaManager mapaManager;

    void Start()
    {
        // ✅ Asignar listeners a los botones
        btnAtacar.onClick.AddListener(OnAtacar);
        btnCancelar.onClick.AddListener(OnCancelar);

        // ✅ Buscar el MapaManager en la escena
        mapaManager = FindObjectOfType<MapaManager>();

        // ✅ Verificar referencias en consola
        if (mapaManager == null)
            Debug.LogError("❌ MapaManager no encontrado en la escena.");

        if (dialogPanel != null)
            dialogPanel.SetActive(false);
        else
            Debug.LogError("❌ dialogPanel no asignado en PanelOpciones.");
    }

    void OnAtacar()
    {
        Debug.Log("🗡️ Botón Atacar presionado.");

        if (targetNodo != null)
        {
            Debug.Log($"✅ Atacando nodo: {targetNodo.nombre}");
            dialogPanel.SetActive(false);

            // ✅ Guarda la posición antes de entrar al duelo
            if (DatosJuego.instancia != null)
            {
                DatosJuego.instancia.ultimaPosicionJugador = mapaManager.jugador.transform.position;
            }

            // ✅ Carga la escena de duelo si existe en Build Settings
            if (Application.CanStreamedLevelBeLoaded("Duelo"))
            {
                Debug.Log("🔄 Cargando escena Duelo...");
                SceneManager.LoadScene("Duelo"); // Asegurate que la escena se llame exactamente "Duelo"
            }
            else
            {
                Debug.LogError("❌ La escena 'Duelo' no está en Build Settings o el nombre es incorrecto.");
            }
        }
        else
        {
            Debug.LogError("❌ Error al atacar: targetNodo es null.");
        }
    }

    void OnCancelar()
    {
        Debug.Log("❌ Botón Cancelar presionado.");
        dialogPanel.SetActive(false);
    }

    // ✅ Configura el nodo objetivo desde BotonNodo
    public void ConfigurarObjetivo(Nodo nodo)
    {
        targetNodo = nodo;
        Debug.Log($"✅ targetNodo configurado: {nodo.nombre}");
    }
}
