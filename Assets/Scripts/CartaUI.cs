/*using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CartaUI : MonoBehaviour
{
    public int indiceEnMano;
    public SistemaDuelo sistemaDuelo;

    public TMP_Text textoTipoCarta;
    private Button boton;
    private Image imagenFondo;

    private Color colorOriginal = Color.blue;
    public Color colorSeleccionado = Color.cyan;

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

*/
/*using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartaUI : MonoBehaviour
{
    public int indiceEnMano;
    public SistemaDuelo sistemaDuelo;

    public TMP_Text textoTipoCarta;
    private Button boton;
    private Image imagenFondo;

    private Color colorOriginal = Color.blue;
    public Color colorSeleccionado = Color.cyan;

    private bool estaSeleccionada = false;

    public Image imagenCarta;
    public Sprite spriteAtaque;
    public Sprite spriteDefensa;
    public Sprite spriteBuffeo;

    // Movimiento visual
    private Vector3 posicionOriginal;
    public float desplazamientoY = 200f;

    void Awake()
    {
        boton = GetComponent<Button>();
        imagenFondo = GetComponent<Image>();

        if (imagenFondo != null)
            colorOriginal = imagenFondo.color;

        if (boton != null)
            boton.onClick.AddListener(SeleccionarCarta);
    }

    void Start()
    {
        posicionOriginal = transform.localPosition;
    }

    public void ConfigurarCarta(Carta carta, int indice, SistemaDuelo duelo)
    {
        textoTipoCarta.text = carta.tipo.ToString();
        indiceEnMano = indice;
        sistemaDuelo = duelo;

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

        // Resetear selección visual
        estaSeleccionada = false;
        if (imagenFondo != null)
            imagenFondo.color = colorOriginal;

        transform.localPosition = posicionOriginal; // Restaurar posición
    }

    public void SeleccionarCarta()
    {
        if (estaSeleccionada)
        {
            // Deseleccionar
            estaSeleccionada = false;
            if (imagenFondo != null)
                imagenFondo.color = colorOriginal;

            transform.localPosition = posicionOriginal;
            sistemaDuelo.DeseleccionarCarta(indiceEnMano);
            Debug.Log("Carta Deslegida!!");
            Debug.Log(transform.localPosition);
        }
        else
        {
            // Seleccionar
            estaSeleccionada = true;
            if (imagenFondo != null)
                imagenFondo.color = colorSeleccionado;

            transform.localPosition = posicionOriginal + new Vector3(0, desplazamientoY, 0);
            sistemaDuelo.CartaSeleccionada(indiceEnMano);
            Debug.Log("Carta Elegida!!");
            Debug.Log(transform.localPosition);
        }
    }
}
*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CartaUI : MonoBehaviour
{
    public int indiceEnMano;
    public SistemaDuelo sistemaDuelo;

    public TMP_Text textoTipoCarta;
    private Button boton;
    private Image imagenFondo;

    private Color colorOriginal = Color.blue;
    public Color colorSeleccionado = Color.cyan;

    private bool estaSeleccionada = false;

    public Image imagenCarta;
    public Sprite spriteAtaque;
    public Sprite spriteDefensa;
    public Sprite spriteBuffeo;

    // Movimiento visual
    private Vector3 posicionOriginalImagen;
    public float desplazamientoY = 50f;
    public float duracionMovimiento = 0.08f;

    void Awake()
    {
        boton = GetComponent<Button>();
        imagenFondo = GetComponent<Image>();

        if (imagenFondo != null)
            colorOriginal = imagenFondo.color;

        if (boton != null)
            boton.onClick.AddListener(SeleccionarCarta);
    }

    void Start()
    {
        if (imagenCarta != null)
            posicionOriginalImagen = imagenCarta.rectTransform.localPosition;
    }

    public void ConfigurarCarta(Carta carta, int indice, SistemaDuelo duelo)
    {
        textoTipoCarta.text = carta.tipo.ToString();
        indiceEnMano = indice;
        sistemaDuelo = duelo;

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

        estaSeleccionada = false;
        if (imagenFondo != null)
            imagenFondo.color = colorOriginal;

        if (imagenCarta != null)
            imagenCarta.rectTransform.localPosition = posicionOriginalImagen;
    }

    public void SeleccionarCarta()
    {
        if (estaSeleccionada)
        {
            estaSeleccionada = false;
            if (imagenFondo != null)
                imagenFondo.color = colorOriginal;

            if (imagenCarta != null)
                StartCoroutine(MoverCarta(imagenCarta.rectTransform, posicionOriginalImagen));

            sistemaDuelo.DeseleccionarCarta(indiceEnMano);
            Debug.Log("Carta Deseleccionada!!");
        }
        else
        {
            estaSeleccionada = true;
            if (imagenFondo != null)
                imagenFondo.color = colorSeleccionado;

            if (imagenCarta != null)
            {
                Vector3 destino = posicionOriginalImagen + new Vector3(0, desplazamientoY, 0);
                StartCoroutine(MoverCarta(imagenCarta.rectTransform, destino));
            }

            sistemaDuelo.CartaSeleccionada(indiceEnMano);
            Debug.Log("Carta Seleccionada!!");
        }
    }

    IEnumerator MoverCarta(RectTransform carta, Vector3 destino)
    {
        Vector3 inicio = carta.localPosition;
        float t = 0;

        while (t < duracionMovimiento)
        {
            carta.localPosition = Vector3.Lerp(inicio, destino, t / duracionMovimiento);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        carta.localPosition = destino;
    }
}
