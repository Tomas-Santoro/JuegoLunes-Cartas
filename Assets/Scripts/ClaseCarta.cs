public class Carta
{
    //holas
    public string tipo; // "DA�O", "BUFO", "DEFENSA", etc.

    public Carta(string tipo)
    {
        this.tipo = tipo;
    }

    public override string ToString()
    {
        return tipo;
    }
}