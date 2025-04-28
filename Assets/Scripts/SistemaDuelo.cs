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

        // A la IA le elegimos 2 cartas aleatorias autom�ticamente
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

            yield return new WaitForSeconds(1f); // Peque�o delay entre duelos
        }

        // Chequear si alguien perdi�
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
            // Si nadie perdi�, empezar nueva ronda
            Debug.Log("Empieza una nueva ronda!");
            EmpezarNuevaRonda();
        }
    }

    private void ResolverDuelo(Carta jugadorCarta, Carta iaCarta)
    {
        // Combinaciones b�sicas
        if (jugadorCarta.tipo == TipoCarta.Buffeo)
        {
            jugador.AplicarBuffeo();
            Debug.Log("Jugador aplic� BUFFEO.");
        }
        if (iaCarta.tipo == TipoCarta.Buffeo)
        {
            ia.AplicarBuffeo();
            Debug.Log("IA aplic� BUFFEO.");
        }

        if (jugadorCarta.tipo == TipoCarta.Ataque && iaCarta.tipo == TipoCarta.Defensa)
        {
            Debug.Log("Jugador atac� pero IA defendi�. No pasa nada.");
            return;
        }
        if (iaCarta.tipo == TipoCarta.Ataque && jugadorCarta.tipo == TipoCarta.Defensa)
        {
            Debug.Log("IA atac� pero Jugador defendi�. No pasa nada.");
            return;
        }

        // Si llegamos hasta ac�, aplicamos da�os si corresponde
        if (jugadorCarta.tipo == TipoCarta.Ataque)
        {
            int da�o = jugador.tieneBuffeoActivo ? 2 : 1;
            ia.vida -= da�o;
            Debug.Log($"Jugador hizo {da�o} de da�o a la IA.");
            jugador.tieneBuffeoActivo = false; // Se gasta el buffeo
        }

        if (iaCarta.tipo == TipoCarta.Ataque)
        {
            int da�o = ia.tieneBuffeoActivo ? 2 : 1;
            jugador.vida -= da�o;
            Debug.Log($"IA hizo {da�o} de da�o al Jugador.");
            ia.tieneBuffeoActivo = false; // Se gasta el buffeo
        }
    }
}
