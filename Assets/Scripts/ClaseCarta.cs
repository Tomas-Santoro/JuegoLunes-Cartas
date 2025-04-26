public class Carta
{
    //holas
    public string tipo; // "DAÑO", "BUFO", "DEFENSA", etc.

    public Carta(string tipo)
    {
        this.tipo = tipo;
    }

    public override string ToString()
    {
        return tipo;
    }
}