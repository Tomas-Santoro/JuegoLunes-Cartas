using UnityEngine;
using UnityEngine.UI;

public class PanelOpciones : MonoBehaviour
{
    public Button btnAtacar;
    public Button btnCancelar;
    public GameObject dialogPanel;

    private Nodo targetNodo; // ✅ Cambiado de Transform a Nodo
    private MapaManager mapaManager;

    void Start()
    {
        btnAtacar.onClick.AddListener(OnAtacar);
        btnCancelar.onClick.AddListener(OnCancelar);

        mapaManager = FindObjectOfType<MapaManager>();
    }

    void OnAtacar()
    {
        if (mapaManager != null && targetNodo != null)
        {
            mapaManager.MoverJugadorANodo(targetNodo); // ✅ Usar grafo para mover
            dialogPanel.SetActive(false);
        }
    }

    void OnCancelar()
    {
        dialogPanel.SetActive(false);
    }

    public void ConfigurarObjetivo(Nodo nodo)
    {
        targetNodo = nodo;
    }
}
