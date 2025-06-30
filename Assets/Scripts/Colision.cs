using UnityEngine;
using UnityEngine.UI;

public class CambioColorAlPasarMouse : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color colorOriginal;

    public GameObject dialogPanel;
    public PanelOpciones panelOpciones;

    private BotonNodo botonNodo; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;

        botonNodo = GetComponent<BotonNodo>(); 

        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    void OnMouseEnter()
    {
        spriteRenderer.color = Color.red;
    }

    void OnMouseExit()
    {
        spriteRenderer.color = colorOriginal;
    }

    // Se llama al clic
    void OnMouseDown()
    {
        if (dialogPanel != null && botonNodo != null && botonNodo.nodo != null)
        {
            dialogPanel.SetActive(true);
            panelOpciones.ConfigurarObjetivo(botonNodo.nodo); 
        }
    }
}
