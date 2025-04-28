using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    //private Stack<Carta> cartas = new Stack<Carta>();
    private Pila<Carta> cartas = new Pila<Carta>();
    public Deck()
    {
        InicializarCartas();
        Mezclar();
    }

    private void InicializarCartas()
    {
        // Lista para mezclar
        List<Carta> cartasTemporales = new List<Carta>
        {
            new Carta(TipoCarta.Ataque),
            new Carta(TipoCarta.Ataque),
            new Carta(TipoCarta.Defensa),
            new Carta(TipoCarta.Defensa),
            new Carta(TipoCarta.Buffeo),
            new Carta(TipoCarta.Buffeo)
        };

        // Mezcla
        for (int i = 0; i < cartasTemporales.Count; i++)
        {
            int randomIndex = Random.Range(0, cartasTemporales.Count);
            Carta temp = cartasTemporales[i];
            cartasTemporales[i] = cartasTemporales[randomIndex];
            cartasTemporales[randomIndex] = temp;
        }

        // Crear la pila en base al arreglo de arriba 
        //cartas = new Stack<Carta>(cartasTemporales);
        cartas = new Pila<Carta>(cartasTemporales);
    }

    public void Mezclar()
    {
        // List<Carta> cartasLista = new List<Carta>(cartas);
        List<Carta> cartasLista = cartas.ALista();

        for (int i = 0; i < cartasLista.Count; i++)
        {
            int randomIndex = Random.Range(0, cartasLista.Count);
            Carta temp = cartasLista[i];
            cartasLista[i] = cartasLista[randomIndex];
            cartasLista[randomIndex] = temp;
        }
        //crea la pila en base a una lista
        //cartas = new Stack<Carta>(cartasLista);
        cartas = new Pila<Carta>(cartasLista);

    }

    public List<Carta> RobarCartas(int cantidad)
    {
        List<Carta> cartasRobadas = new List<Carta>();

        for (int i = 0; i < cantidad; i++)
        {
            if (cartas.Contar() == 0)
            {
                Debug.LogWarning("No hay más cartas en el mazo, se volverá a mezclar.");
                InicializarCartas();
                Mezclar();
            }

            cartasRobadas.Add(cartas.Desapilar());
        }

        return cartasRobadas;
    }
}
