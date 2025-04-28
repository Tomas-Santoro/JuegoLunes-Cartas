using UnityEngine;
public class Nodo<T>
{
    public T valor;
    public Nodo<T> siguiente;

    //Constructor principal --- valor y siguiente
    public Nodo(T valor, Nodo<T> siguiente)
    {
        this.valor = valor;
        this.siguiente = siguiente;
    }

    //Sobrecarga --- solo valor, el nodo siguiente queda en null
    public Nodo(T valor) : this(valor, null)
    {
    }
}
