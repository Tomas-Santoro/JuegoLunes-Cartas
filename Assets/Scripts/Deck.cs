using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck
{
    private List<Carta> cartas = new List<Carta>();

    public Deck()
    {
        InicializarCartas();
    }

    private void InicializarCartas()
    {
        // Crea un mazo de 6 cartas
        cartas.Add(new Carta(TipoCarta.Ataque));
        cartas.Add(new Carta(TipoCarta.Ataque));
        cartas.Add(new Carta(TipoCarta.Defensa));
        cartas.Add(new Carta(TipoCarta.Defensa));
        cartas.Add(new Carta(TipoCarta.Buffeo));
        cartas.Add(new Carta(TipoCarta.Buffeo));
    }

    public void Mezclar()
    {
        cartas = cartas.OrderBy(c => Random.value).ToList();
    }

    public List<Carta> RobarCartas(int cantidad)
    {
        Mezclar();
        return cartas.Take(cantidad).ToList();
    }
}
