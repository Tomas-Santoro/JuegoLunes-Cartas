using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartaUI : MonoBehaviour
{
    public int indiceEnMano;
    public SistemaDuelo sistemaDuelo;

    public TMP_Text textoTipoCarta;
    private Button boton;

    void Awake()
    {
        boton = GetComponent<Button>();
        if (boton != null)
        {
            boton.onClick.AddListener(SeleccionarCarta);
        }
    }

    public void ConfigurarCarta(Carta carta, int indice, SistemaDuelo duelo)
    {
        textoTipoCarta.text = carta.tipo.ToString();
        indiceEnMano = indice;
        sistemaDuelo = duelo;
    }

    public void SeleccionarCarta()
    {
        Debug.Log("Clickeaste una carta!"); // Primer prueba
        sistemaDuelo.CartaSeleccionada(indiceEnMano);
    }
}
