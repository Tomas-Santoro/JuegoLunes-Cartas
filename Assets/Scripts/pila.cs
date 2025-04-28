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

    //Desapilar --- saca el de más arriba
    public T Desapilar()
    {
        if (PilaVacia())
            Debug.Log("La pila está vacía.");

        T valor = tope.valor;   //Guarda el valor que está arriba
        tope = tope.siguiente;  //Avanza la cima (elimina el nodo)
        return valor;           //Retorna el valor guardado
    }

    //Tope --- muestra el de más arriba sin sacarlo
    public T Tope()
    {
        if (!PilaVacia())
        {
            return tope.valor;
        }
        else
        {
            Debug.Log("Pila vacía.");
            return default(T);
        }
    }

    //Pila vacía --- verifica si la pila está vacía
    public bool PilaVacia()
    {
        return tope == null;
    }
}
