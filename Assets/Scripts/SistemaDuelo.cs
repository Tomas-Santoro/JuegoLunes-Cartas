using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SistemaDuelo : MonoBehaviour
{
    public Jugador jugador;
    public Jugador ia;

    public GameObject[] slotsCartasJugador; // Los 3 botones o imágenes de las cartas
    public GameObject cartaPrefab; // Prefab de la carta visual
    private List<int> indicesSeleccionados = new List<int>();

    public Image[] vidasJugador;
    public Image[] vidasIA;

    void Start()
    {
        jugador = new Jugador();
        ia = new Jugador();

        EmpezarNuevaRonda();
    }

    public void EmpezarNuevaRonda()
    {
        jugador.RobarMano();
        ia.RobarMano();

        indicesSeleccionados.Clear();

        // IA elige automáticamente
        List<int> indicesIA = new List<int> { 0, 1 };
        ia.ElegirCartas(indicesIA);

        MostrarCartasJugador();
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
            //Carta cartaJugador = jugador.acciones.Dequeue();
            //Carta cartaIA = ia.acciones.Dequeue();
            Carta cartaJugador = jugador.acciones.Desencolar();
            Carta cartaIA = ia.acciones.Desencolar();

            Debug.Log($"Resolviendo duelo: Jugador({cartaJugador.tipo}) vs IA({cartaIA.tipo})");

            ResolverDuelo(cartaJugador, cartaIA);

            ActualizarVidas(); // Actualizamos visualmente

            yield return new WaitForSeconds(1f);
        }

        if (jugador.vida <= 0)
        {
            Debug.Log("Perdiste el duelo!");
            SceneManager.LoadScene("Derrota");
        }
        else if (ia.vida <= 0)
        {
            Debug.Log("Ganaste el duelo!");

         
            DatosJuego.instancia.batallasGanadas++;

           
            if (DatosJuego.instancia.batallasGanadas >= DatosJuego.instancia.batallasParaGanar)
            {
                SceneManager.LoadScene("Victoria"); 
            }
            else
            {
                SceneManager.LoadScene("SampleScene"); 
            }
        }
        else
        {
            Debug.Log("Nueva ronda!");
            EmpezarNuevaRonda();
        }
    }

    private void ResolverDuelo(Carta jugadorCarta, Carta iaCarta)
    {
        // Combinaciones básicas
        if (jugadorCarta.tipo == TipoCarta.Buffeo)
        {
            jugador.AplicarBuffeo();
            Debug.Log("Jugador aplicó BUFFEO.");
        }
        if (iaCarta.tipo == TipoCarta.Buffeo)
        {
            ia.AplicarBuffeo();
            Debug.Log("IA aplicó BUFFEO.");
        }

        if (jugadorCarta.tipo == TipoCarta.Ataque && iaCarta.tipo == TipoCarta.Defensa)
        {
            Debug.Log("Jugador atacó pero IA defendió. No pasa nada.");
            return;
        }
        if (iaCarta.tipo == TipoCarta.Ataque && jugadorCarta.tipo == TipoCarta.Defensa)
        {
            Debug.Log("IA atacó pero Jugador defendió. No pasa nada.");
            return;
        }

        // Si llegamos hasta acá, aplicamos daños si corresponde
        if (jugadorCarta.tipo == TipoCarta.Ataque)
        {
            int daño = jugador.tieneBuffeoActivo ? 2 : 1;
            ia.vida -= daño;
            Debug.Log($"Jugador hizo {daño} de daño a la IA.");
            jugador.tieneBuffeoActivo = false; // Se gasta el buffeo
        }

        if (iaCarta.tipo == TipoCarta.Ataque)
        {
            int daño = ia.tieneBuffeoActivo ? 2 : 1;
            jugador.vida -= daño;
            Debug.Log($"IA hizo {daño} de daño al Jugador.");
            ia.tieneBuffeoActivo = false; // Se gasta el buffeo
        }
    }

    public void MezclarYRobarDeNuevo()
    {
        if (indicesSeleccionados.Count == 0)
        {
            jugador.RobarMano();
            MostrarCartasJugador();
            Debug.Log("Se mezcló el mazo y se robaron nuevas cartas.");
        }
        else
        {
            Debug.Log("No se puede mezclar después de elegir cartas.");
        }
    }


    private void ActualizarVidas()
    {
        for (int i = 0; i < vidasJugador.Length; i++)
        {
            vidasJugador[i].enabled = i < jugador.vida;
        }

        for (int i = 0; i < vidasIA.Length; i++)
        {
            vidasIA[i].enabled = i < ia.vida;
        }
    }

    public void DeseleccionarCarta(int indice)
    {
        if (indicesSeleccionados.Contains(indice))
        {
            indicesSeleccionados.Remove(indice);
            Debug.Log($"Deseleccionaste la carta: {jugador.mano[indice].tipo}");
        }
    }


}
