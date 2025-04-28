public enum TipoCarta
{
    Ataque,
    Defensa,
    Buffeo
}

public class Carta
{
    public TipoCarta tipo;

    public Carta(TipoCarta tipo)
    {
        this.tipo = tipo;
    }

    public override string ToString()
    {
        return tipo.ToString();
    }
}
