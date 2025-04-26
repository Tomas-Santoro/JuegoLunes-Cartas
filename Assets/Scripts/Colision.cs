using UnityEngine;
using UnityEngine.UI;

public class CambioColorAlPasarMouse : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color colorOriginal;

    public GameObject dialogPanel;
    public PanelOpciones panelOpciones;  

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;

        // Asegura de que el panel esté desactivado al inicio.
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
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(true);  
            panelOpciones.ConfigurarObjetivo(transform); 
        }
    }
}
