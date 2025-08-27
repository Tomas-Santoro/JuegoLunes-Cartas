public class NodoGenerico<T>
{
    public T valor;
    public NodoGenerico<T> siguiente;

    public NodoGenerico(T valor, NodoGenerico<T> siguiente)
    {
        this.valor = valor;
        this.siguiente = siguiente;
    }
}
