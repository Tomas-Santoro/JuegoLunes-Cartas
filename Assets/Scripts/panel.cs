using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con botones
using UnityEngine.SceneManagement;

public class PanelOpciones : MonoBehaviour
{
    public Button btnAtacar;  // Bot�n de "Atacar"
    public Button btnCancelar;  // Bot�n de "Cancelar"
    public GameObject dialogPanel;  // El panel de opciones
    private Transform targetObject;

    private MovimientoJugador movimientoJugador;

    void Start()
    {
        // Asigna los botones
        btnAtacar.onClick.AddListener(OnAtacar);  // Asocia el m�todo "OnAtacar" al bot�n de atacar
        btnCancelar.onClick.AddListener(OnCancelar);  // Asocia el m�todo "OnCancelar" al bot�n de cancelar

        // Encuentra el script de movimiento
        movimientoJugador = FindObjectOfType<MovimientoJugador>();
    }

    // M�todo que se llama al presionar "Atacar"
    void OnAtacar()
    {
        if (movimientoJugador != null && targetObject != null)
        {
            movimientoJugador.MoverJugador(targetObject);  // Mueve al jugador hacia el objeto seleccionado
            dialogPanel.SetActive(false);  // Cierra el panel de opciones
        }
    }

    // M�todo que se llama al presionar "Cancelar"
    void OnCancelar()
    {
        dialogPanel.SetActive(false);  // Solo cierra el panel sin hacer nada m�s
    }

    // M�todo para configurar el objeto al que se mover� el jugador
    public void ConfigurarObjetivo(Transform objeto)
    {
        targetObject = objeto;
    }


    //prueba comit

    //asdasdasda
}
