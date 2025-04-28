using UnityEngine;

public class Pila<T>
{
    private Nodo<T> tope;

    public Pila()
    {
        tope = null;
    }

    //Apilar --- mete un nuevo elemento arriba de todo
    public void Apilar(T elemento)
    {
        Nodo<T> nuevo = new Nodo<T>(elemento, tope);
        tope = nuevo;
    }

    //Desapilar --- saca el de m�s arriba
    public void Desapilar()
    {
        if (!PilaVacia())
        {
            tope = tope.siguiente;
        }
        else
        {
            Debug.Log("Pila vac�a.");
        }
    }

    //Tope --- muestra el de m�s arriba sin sacarlo
    public T Tope()
    {
        if (!PilaVacia())
        {
            return tope.valor;
        }
        else
        {
            Debug.Log("Pila vac�a.");
            return default(T);
        }
    }

    //Pila vac�a --- verifica si la pila est� vac�a
    public bool PilaVacia()
    {
        return tope == null;
    }
}
