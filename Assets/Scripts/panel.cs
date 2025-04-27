using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelOpciones : MonoBehaviour
{
    public Button btnAtacar;
    public Button btnCancelar;
    public GameObject dialogPanel;
    private Transform targetObject;

    private MovimientoJugador movimientoJugador;
    private bool esperandoCambioDeEscena = false; // Para esperar a que termine de moverse

    void Start()
    {
        btnAtacar.onClick.AddListener(OnAtacar);
        btnCancelar.onClick.AddListener(OnCancelar);

        movimientoJugador = FindObjectOfType<MovimientoJugador>();
    }

    void Update()
    {
        if (esperandoCambioDeEscena && movimientoJugador != null && !movimientoJugador.estaMoviendose)
        {
            SceneManager.LoadScene("Duelo");
        }
    }

    void OnAtacar()
    {
        if (movimientoJugador != null && targetObject != null)
        {
            movimientoJugador.MoverJugador(targetObject);
            dialogPanel.SetActive(false);
            esperandoCambioDeEscena = true;
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
