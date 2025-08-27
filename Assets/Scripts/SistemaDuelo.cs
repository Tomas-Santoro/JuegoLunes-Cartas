using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SistemaDuelo : MonoBehaviour
{
    public Jugador jugador;
    public Jugador ia;

    public GameObject[] slotsCartasJugador;
    public GameObject cartaPrefab;
    private List<int> indicesSeleccionados = new List<int>();

    //public Image[] vidasJugador;
    //public Image[] vidasIA;

    public TMP_Text textoVidaJugador;
    public TMP_Text textoVidaIA;


    // Cartas visuales en combate
    public Image cartaElegidaYo;
    public Image cartaElegidaRival;

    public Sprite spriteAtaque;
    public Sprite spriteDefensa;
    public Sprite spriteBuffeo;

    void Start()
    {
        jugador = new Jugador();

        Nodo nodo = DatosJuego.instancia.nodoActual;

        if (nodo != null && nodo.enemigo != null)
        {
            ia = new Jugador(nodo.enemigo.vida);
            ia.nombre = nodo.enemigo.nombre;

            Debug.Log($"Enfrentando a: {ia.nombre} con {ia.vida} de vida");
        }
        else
        {
            ia = new Jugador();
            ia.nombre = "IA Genérica";
            Debug.LogWarning("No se pudo asignar enemigo, usando IA por defecto");
        }

        EmpezarNuevaRonda();
    }

    public void EmpezarNuevaRonda()
    {
        jugador.RobarMano();
        ia.RobarMano();

        indicesSeleccionados.Clear();

        List<int> indicesIA = new List<int> { 0, 1 };
        ia.ElegirCartas(indicesIA);

        MostrarCartasJugador();

        // Ocultar cartas de combate por si estaban visibles
        cartaElegidaYo.gameObject.SetActive(false);
        cartaElegidaRival.gameObject.SetActive(false);
        ActualizarVidas();
    }

    void MostrarCartasJugador()
    {
        for (int i = 0; i < jugador.mano.Count; i++)
        {
            Carta carta = jugador.mano[i];
            CartaUI cartaUI = slotsCartasJugador[i].GetComponent<CartaUI>();
            cartaUI.ConfigurarCarta(carta, i, this);
        }
    }

    public void CartaSeleccionada(int indice)
    {
        if (indicesSeleccionados.Contains(indice))
        {
            Debug.Log("Carta ya seleccionada.");
            return;
        }

        indicesSeleccionados.Add(indice);
        Carta cartaElegida = jugador.mano[indice];
        Debug.Log($"Seleccionaste la carta: {cartaElegida.tipo}");

        if (indicesSeleccionados.Count == 2)
        {
            jugador.ElegirCartas(indicesSeleccionados);
            ResolverDuelos();
        }
    }

    public void ResolverDuelos()
    {
        StartCoroutine(ResolverAcciones());
    }

    private IEnumerator ResolverAcciones()
    {
        jugador.DesactivarBuffeo();
        ia.DesactivarBuffeo();

        for (int i = 0; i < 2; i++)
        {
            Carta cartaJugador = jugador.acciones.Desencolar();
            Carta cartaIA = ia.acciones.Desencolar();

            Debug.Log($"Resolviendo duelo: Jugador({cartaJugador.tipo}) vs IA({cartaIA.tipo})");

            // Mostrar visualmente las cartas
            cartaElegidaYo.sprite = ObtenerSprite(cartaJugador.tipo);
            cartaElegidaRival.sprite = ObtenerSprite(cartaIA.tipo);

            cartaElegidaYo.gameObject.SetActive(true);
            cartaElegidaRival.gameObject.SetActive(true);

            // Pop animation
            StartCoroutine(PopCard(cartaElegidaYo.rectTransform));
            StartCoroutine(PopCard(cartaElegidaRival.rectTransform));

            ResolverDuelo(cartaJugador, cartaIA);
            ActualizarVidas();

            yield return new WaitForSeconds(1f);
        }

        if (jugador.vida <= 0)
        {
            SceneManager.LoadScene("Derrota");
        }
        else if (ia.vida <= 0)
        {
            DatosJuego.instancia.batallasGanadas++;
            DatosJuego.instancia.nodoDerrotado = DatosJuego.instancia.nodoActual;

            // 🔥 Eliminar del árbol ABB de enemigos
            Nodo nodo = DatosJuego.instancia.nodoActual;
            if (MapaManager.arbolPoderEnemigos != null && nodo.enemigo != null)
            {
                MapaManager.arbolPoderEnemigos.Eliminar(nodo.enemigo);
                Debug.Log($"Eliminado del ranking: {nodo.enemigo.nombre}");
            }

            if (DatosJuego.instancia.batallasGanadas >= DatosJuego.instancia.batallasParaGanar)
                SceneManager.LoadScene("Victoria");
            else
                SceneManager.LoadScene("SampleScene");
        }
        /*else if (ia.vida <= 0)
        {
            DatosJuego.instancia.batallasGanadas++;
            DatosJuego.instancia.nodoDerrotado = DatosJuego.instancia.nodoActual;

            if (DatosJuego.instancia.batallasGanadas >= DatosJuego.instancia.batallasParaGanar)
                SceneManager.LoadScene("Victoria");
            else
                SceneManager.LoadScene("SampleScene");
        }*/
        else
        {
            EmpezarNuevaRonda();
        }

    }

    private void ResolverDuelo(Carta jugadorCarta, Carta iaCarta)
    {
        if (jugadorCarta.tipo == TipoCarta.Buffeo)
        {
            jugador.AplicarBuffeo();
        }
        if (iaCarta.tipo == TipoCarta.Buffeo)
        {
            ia.AplicarBuffeo();
        }

        if (jugadorCarta.tipo == TipoCarta.Ataque && iaCarta.tipo == TipoCarta.Defensa)
            return;

        if (iaCarta.tipo == TipoCarta.Ataque && jugadorCarta.tipo == TipoCarta.Defensa)
            return;

        if (jugadorCarta.tipo == TipoCarta.Ataque)
        {
            int daño = jugador.tieneBuffeoActivo ? 2 : 1;
            ia.vida -= daño;
            jugador.tieneBuffeoActivo = false;
        }

        if (iaCarta.tipo == TipoCarta.Ataque)
        {
            int daño = ia.tieneBuffeoActivo ? 2 : 1;
            jugador.vida -= daño;
            ia.tieneBuffeoActivo = false;
        }
    }

    /*private void ActualizarVidas()
    {
        for (int i = 0; i < vidasJugador.Length; i++)
            vidasJugador[i].enabled = i < jugador.vida;

        for (int i = 0; i < vidasIA.Length; i++)
            vidasIA[i].enabled = i < ia.vida;
    }*/

    private void ActualizarVidas()
    {
        if (textoVidaJugador != null)
            textoVidaJugador.text = $"Vida: {jugador.vida}";

        if (textoVidaIA != null)
            textoVidaIA.text = $"Vida: {ia.vida}";
    }


    public void MezclarYRobarDeNuevo()
    {
        if (indicesSeleccionados.Count == 0)
        {
            jugador.RobarMano();
            MostrarCartasJugador();
        }
    }

    public void DeseleccionarCarta(int indice)
    {
        if (indicesSeleccionados.Contains(indice))
        {
            indicesSeleccionados.Remove(indice);
        }
    }

    private Sprite ObtenerSprite(TipoCarta tipo)
    {
        switch (tipo)
        {
            case TipoCarta.Ataque: return spriteAtaque;
            case TipoCarta.Defensa: return spriteDefensa;
            case TipoCarta.Buffeo: return spriteBuffeo;
            default: return null;
        }
    }

    private IEnumerator PopCard(RectTransform rect)
    {
        Vector3 escalaOriginal = rect.localScale;
        Vector3 escalaMax = escalaOriginal * 1.2f;
        float duracion = 0.1f;

        float t = 0;
        while (t < duracion)
        {
            rect.localScale = Vector3.Lerp(escalaOriginal, escalaMax, t / duracion);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.localScale = escalaMax;

        t = 0;
        while (t < duracion)
        {
            rect.localScale = Vector3.Lerp(escalaMax, escalaOriginal, t / duracion);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.localScale = escalaOriginal;
    }
}
