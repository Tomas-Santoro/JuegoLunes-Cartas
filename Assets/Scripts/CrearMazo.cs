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
    }

    void CrearMazo()
    {
        // Crear lista temporal con cartas para despues usar funcion mezclar 
        List<Carta> cartasTemporales = new List<Carta>
        {
            new Carta("DAÑO"),
            new Carta("DEFENSA"),
            new Carta("BUFO"),
            new Carta("DAÑO"),
            new Carta("DEFENSA"),
            new Carta("BUFO")
        };

        // Mostrar el orden original
        Debug.Log(" Orden original de cartas:");
        foreach (var carta in cartasTemporales)
        {
            Debug.Log(" - " + carta.tipo);
        }

        // Mezclar //NO FUNCIONAAAAAAAAAAAAAAAAAAAAAAAA
        cartasTemporales = cartasTemporales.OrderBy(c => Random.value).ToList();

        // PRUEBA PARA VER SI AGARRA EL ORDEN
        Debug.Log(" Orden después de mezclar:");
        foreach (var carta in cartasTemporales)
        {
            Debug.Log(" - " + carta.tipo);
        }

        // Cargar las cartas al mazo (pila)
        foreach (var carta in cartasTemporales)
        {
            mazo.Push(carta);
        }

        Debug.Log(" Mazo listo con " + mazo.Count + " cartas.");
    }

    void RobarCarta()
    {
        if (mazo.Count > 0)
        {
            Carta cartaRobada = mazo.Pop();
            Debug.Log(" Carta robada: " + cartaRobada.tipo);
        }
        else
        {
            Debug.Log(" No hay más cartas en el mazo.");
        }
    }
}

//prueba
