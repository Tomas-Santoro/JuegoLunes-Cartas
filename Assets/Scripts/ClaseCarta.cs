public class Carta
{
    //holas
    //hola soy pepe
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