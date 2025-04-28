using System;
using UnityEngine;
using System.Collections.Generic;

public class Pila<T>
{
    private Nodo<T> tope;

    public Pila()
    {
        tope = null;
    }

    public Pila(T[] arreglo)
    {
        tope = null;

        // Recorrer el arreglo desde el final al principio
        for (int i = arreglo.Length - 1; i >= 0; i--)
        {
            Apilar(arreglo[i]);
        }
    }

    public Pila(List<T> lista)
    {
        tope = null;

        // Recorrer la lista desde el final hacia el principio
        for (int i = lista.Count - 1; i >= 0; i--)
        {
            Apilar(lista[i]);
        }
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

    public int Contar()
    {
        int cantidad = 0;
        Nodo<T> actual = tope;

        while (actual != null)
        {
            cantidad++;
            actual = actual.siguiente;
        }

        return cantidad;
    }

    public List<T> ALista()
    {
        List<T> lista = new List<T>();
        Nodo<T> actual = tope;

        while (actual != null)
        {
            lista.Add(actual.valor);
            actual = actual.siguiente;
        }

        return lista;
    }


}
