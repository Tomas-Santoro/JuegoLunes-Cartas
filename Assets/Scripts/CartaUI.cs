using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CartaUI : MonoBehaviour
{
    public int indiceEnMano;
    public SistemaDuelo sistemaDuelo;

    public TMP_Text textoTipoCarta;
    private Button boton;
    private Image imagenFondo; // Fondo de la carta para cambiar el color

    private Color colorOriginal = Color.blue;
    public Color colorSeleccionado = Color.cyan; // Color cuando la carta está seleccionada

    private bool estaSeleccionada = false;

    public Image imagenCarta;
    public Sprite spriteAtaque;
    public Sprite spriteDefensa;
    public Sprite spriteBuffeo;


    void Awake()
    {
        boton = GetComponent<Button>();
        imagenFondo = GetComponent<Image>();

        if (imagenFondo != null)
            colorOriginal = imagenFondo.color;

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

        // Cambiar la imagen según el tipo de carta
        if (imagenCarta != null)
        {
            switch (carta.tipo)
            {
                case TipoCarta.Ataque:
                    imagenCarta.sprite = spriteAtaque;
                    break;
                case TipoCarta.Defensa:
                    imagenCarta.sprite = spriteDefensa;
                    break;
                case TipoCarta.Buffeo:
                    imagenCarta.sprite = spriteBuffeo;
                    break;
            }
        }

        // Siempre que configuramos, la carta arranca no seleccionada
        estaSeleccionada = false;
        if (imagenFondo != null)
            imagenFondo.color = colorOriginal;

    }

    public void SeleccionarCarta()
    {
        if (estaSeleccionada)
        {
            // Si ya estaba seleccionada -> la deseleccionamos
            estaSeleccionada = false;
            if (imagenFondo != null)
                imagenFondo.color = colorOriginal;

            sistemaDuelo.DeseleccionarCarta(indiceEnMano);
        }
        else
        {
            // Si no estaba seleccionada -> la seleccionamos
            estaSeleccionada = true;
            if (imagenFondo != null)
                imagenFondo.color = colorSeleccionado;

            sistemaDuelo.CartaSeleccionada(indiceEnMano);
        }
    }
}
