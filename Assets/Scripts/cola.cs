using UnityEngine;

public class Cola<T>
{
    private Nodo<T> primero;
    private Nodo<T> ultimo;

    public Cola()
    {
        primero = null;
        ultimo = null;
    }

    //Cola vacía --- verifica si la cola está vacía
    public bool EstaVacia()
    {
        return primero == null;
    }

    //Encolar --- agrega elementos al final de la cola
    public void Encolar(T valor)
    {
        Nodo<T> nuevo = new Nodo<T>(valor, null);

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

    //Desencolar --- saca el primer elemento de la cola
    public T Desencolar()
    {
        if (EstaVacia())
            Debug.Log("La cola está vacía.");

        T valor = primero.valor;
        primero = primero.siguiente;

        if (primero == null)
        {
            //Si la cola quedó vacía, actualizar "ultimo" también
            ultimo = null;
        }

        return valor;
    }

    //Primero --- devuelve el primer elemento sin desencolar
    public T Primero()
    {
        if (EstaVacia())
            Debug.Log("La cola está vacía.");

        return primero.valor;
    }

    public void Limpiar()
    {
        primero = null;
        ultimo = null;
    }
}
