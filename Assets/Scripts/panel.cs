using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con botones
using UnityEngine.SceneManagement;

public class PanelOpciones : MonoBehaviour
{
    public Button btnAtacar;  // Botón de "Atacar"
    public Button btnCancelar;  // Botón de "Cancelar"
    public GameObject dialogPanel;  // El panel de opciones
    private Transform targetObject;

    private MovimientoJugador movimientoJugador;

    void Start()
    {
        // Asigna los botones
        btnAtacar.onClick.AddListener(OnAtacar);  // Asocia el método "OnAtacar" al botón de atacar
        btnCancelar.onClick.AddListener(OnCancelar);  // Asocia el método "OnCancelar" al botón de cancelar

        // Encuentra el script de movimiento
        movimientoJugador = FindObjectOfType<MovimientoJugador>();
    }

    // Método que se llama al presionar "Atacar"
    void OnAtacar()
    {
        if (movimientoJugador != null && targetObject != null)
        {
            movimientoJugador.MoverJugador(targetObject);  // Mueve al jugador hacia el objeto seleccionado
            dialogPanel.SetActive(false);  // Cierra el panel de opciones
        }
    }

    // Método que se llama al presionar "Cancelar"
    void OnCancelar()
    {
        dialogPanel.SetActive(false);  // Solo cierra el panel sin hacer nada más
    }

    // Método para configurar el objeto al que se moverá el jugador
    public void ConfigurarObjetivo(Transform objeto)
    {
        targetObject = objeto;
    }


    //prueba comit

    //asdasdasda
}
