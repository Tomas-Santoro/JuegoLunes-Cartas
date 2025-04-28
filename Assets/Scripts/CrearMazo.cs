using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazoCartas : MonoBehaviour
{
    private Stack<Carta> mazo = new Stack<Carta>();

    void Start()
    {
        CrearMazo();
        RobarCarta();
        RobarCarta();
        RobarCarta();
    }

    void CrearMazo()
    {
        // Crear lista temporal con cartas
        List<Carta> cartasTemporales = new List<Carta>
        {
            new Carta(TipoCarta.Ataque),
            new Carta(TipoCarta.Defensa),
            new Carta(TipoCarta.Buffeo),
            new Carta(TipoCarta.Ataque),
            new Carta(TipoCarta.Defensa),
            new Carta(TipoCarta.Buffeo)
        };

        // Mostrar el orden original
        Debug.Log("Orden original de cartas:");
        foreach (var carta in cartasTemporales)
        {
            Debug.Log(" - " + carta.tipo);
        }

        // Mezclar
        cartasTemporales = cartasTemporales.OrderBy(c => Random.value).ToList();

        Debug.Log("Orden después de mezclar:");
        foreach (var carta in cartasTemporales)
        {
            Debug.Log(" - " + carta.tipo);
        }

        // Cargar las cartas al mazo
        foreach (var carta in cartasTemporales)
        {
            mazo.Push(carta);
        }

        Debug.Log("Mazo listo con " + mazo.Count + " cartas.");
    }

    void RobarCarta()
    {
        if (mazo.Count > 0)
        {
            Carta cartaRobada = mazo.Pop();
            Debug.Log("Carta robada: " + cartaRobada.tipo);
        }
        else
        {
            Debug.Log("No hay más cartas en el mazo.");
        }
    }
}
