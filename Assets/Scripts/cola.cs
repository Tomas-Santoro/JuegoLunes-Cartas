using UnityEngine;

public class Cola<T>
{
    private NodoGenerico<T> primero;
    private NodoGenerico<T> ultimo;

    public Cola()
    {
        primero = null;
        ultimo = null;
    }

    public bool EstaVacia()
    {
        return primero == null;
    }

    public void Encolar(T valor)
    {
        NodoGenerico<T> nuevo = new NodoGenerico<T>(valor, null);

        if (EstaVacia())
        {
            primero = nuevo;
            ultimo = nuevo;
        }
        else
        {
            ultimo.siguiente = nuevo;
            ultimo = nuevo;
        }
    }

    public T Desencolar()
    {
        if (EstaVacia())
        {
            Debug.Log("La cola está vacía.");
            return default(T);
        }

        T valor = primero.valor;
        primero = primero.siguiente;

        if (primero == null)
        {
            ultimo = null;
        }

        return valor;
    }

    public T Primero()
    {
        if (EstaVacia())
        {
            Debug.Log("La cola está vacía.");
            return default(T);
        }

        return primero.valor;
    }

    public void Limpiar()
    {
        primero = null;
        ultimo = null;
    }
}
