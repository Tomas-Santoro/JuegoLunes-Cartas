public class Carta
{
    //holas
    //hola soy pepe
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