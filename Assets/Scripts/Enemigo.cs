public class Enemigo
{
    public string nombre;
    public int vida;
    public int poder;
    public Enemigo enemigo;

    public Enemigo(string nombre, int vida, int poder)
    {
        this.nombre = nombre;
        this.vida = vida;
        this.poder = poder;
    }
}
