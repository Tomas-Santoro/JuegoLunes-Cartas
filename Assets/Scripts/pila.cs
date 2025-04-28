using System;
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
    public T Desapilar()
    {
        if (PilaVacia())
            Debug.Log("La pila est� vac�a.");

        T valor = tope.valor;   //Guarda el valor que est� arriba
        tope = tope.siguiente;  //Avanza la cima (elimina el nodo)
        return valor;           //Retorna el valor guardado
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
