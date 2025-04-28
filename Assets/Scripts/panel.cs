using UnityEngine;
using UnityEngine.UI;

public class PanelOpciones : MonoBehaviour
{
    public Button btnAtacar;
    public Button btnCancelar;
    public GameObject dialogPanel;
    private Transform targetObject;

    private MovimientoJugador movimientoJugador;

    void Start()
    {
        btnAtacar.onClick.AddListener(OnAtacar);
        btnCancelar.onClick.AddListener(OnCancelar);

        movimientoJugador = FindObjectOfType<MovimientoJugador>();
    }

    void OnAtacar()
    {
        if (movimientoJugador != null && targetObject != null)
        {
            movimientoJugador.MoverJugador(targetObject, true); //  Le decimos que después del movimiento cambie de escena
            dialogPanel.SetActive(false);
        }
    }

    void OnCancelar()
    {
        dialogPanel.SetActive(false);
    }

    public void ConfigurarObjetivo(Transform objeto)
    {
        targetObject = objeto;
    }
}
