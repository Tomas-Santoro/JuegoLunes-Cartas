using UnityEngine;

public class BotonNodo : MonoBehaviour // ✅ debe heredar de MonoBehaviour
{
    public Nodo nodo; // Referencia a este nodo
    private PanelOpciones panelOpciones;

    void Start()
    {
        panelOpciones = FindObjectOfType<PanelOpciones>();
    }

    void OnMouseDown()
    {
        if (panelOpciones != null && nodo != null)
        {
            panelOpciones.ConfigurarObjetivo(nodo);
            panelOpciones.dialogPanel.SetActive(true);
        }
    }
}
