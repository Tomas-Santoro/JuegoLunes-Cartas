using System.Collections;
using UnityEngine;

public class SistemaDuelo : MonoBehaviour
{
    public Jugador jugador;
    public Jugador ia;

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

        // A la IA le elegimos 2 cartas aleatorias automáticamente
        List<int> indicesIA = new List<int> { 0, 1 };
        ia.ElegirCartas(indicesIA);

        // Ahora falta: dejar que el jugador elija sus 2 cartas de mano
        Debug.Log("Jugador debe elegir sus 2 cartas.");
    }

    public void ResolverDuelos()
    {
        StartCoroutine(ResolverAcciones());
    }

    private IEnumerator ResolverAcciones()
    {
        for (int i = 0; i < 2; i++)
        {
            Carta cartaJugador = jugador.acciones.Dequeue();
            Carta cartaIA = ia.acciones.Dequeue();

            Debug.Log($"Resolviendo duelo: Jugador({cartaJugador.tipo}) vs IA({cartaIA.tipo})");

            ResolverDuelo(cartaJugador, cartaIA);

            yield return new WaitForSeconds(1f); // Pequeño delay entre duelos
        }

        // Chequear si alguien perdió
        if (jugador.vida <= 0)
        {
            Debug.Log("Perdiste el duelo!");
        }
        else if (ia.vida <= 0)
        {
            Debug.Log("Ganaste el duelo!");
        }
        else
        {
            // Si nadie perdió, empezar nueva ronda
            Debug.Log("Empieza una nueva ronda!");
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
}
