using System.Collections.Generic;

public class Jugador
{
    public string nombre;
    public int vida;
    public Deck deck;
    public List<Carta> mano;
    public Cola<Carta> acciones;
    public bool tieneBuffeoActivo = false;

    // Constructor por defecto (jugador humano)
    public Jugador()
    {
        this.nombre = "Jugador";
        this.vida = 3;
        this.deck = new Deck();
        this.mano = new List<Carta>();
        this.acciones = new Cola<Carta>();
    }

    // Constructor con vida personalizada (para IA/enemigos)
    public Jugador(int vida)
    {
        this.nombre = "Enemigo";
        this.vida = vida;
        this.deck = new Deck();
        this.mano = new List<Carta>();
        this.acciones = new Cola<Carta>();
    }

    public void RobarMano()
    {
        mano = deck.RobarCartas(3);
    }

    public void ElegirCartas(List<int> indicesElegidos)
    {
        acciones.Limpiar();
        foreach (int index in indicesElegidos)
        {
            acciones.Encolar(mano[index]);
        }
    }

    public void AplicarBuffeo()
    {
        tieneBuffeoActivo = true;
    }

    public void DesactivarBuffeo()
    {
        tieneBuffeoActivo = false;
    }
}
