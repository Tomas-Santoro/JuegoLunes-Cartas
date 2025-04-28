using System.Collections.Generic;

public class Jugador
{
    public int vida = 3;
    public Deck deck;
    public List<Carta> mano = new List<Carta>();
    public Queue<Carta> acciones = new Queue<Carta>();
    public bool tieneBuffeoActivo = false;

    public Jugador()
    {
        deck = new Deck();
    }

    public void RobarMano()
    {
        mano = deck.RobarCartas(3);
    }

    public void ElegirCartas(List<int> indicesElegidos)
    {
        acciones.Clear();
        foreach (int index in indicesElegidos)
        {
            acciones.Enqueue(mano[index]);
        }
    }

    public void AplicarBuffeo()
    {
        tieneBuffeoActivo = true;
    }
}
