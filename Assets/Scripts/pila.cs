using System.Collections.Generic;
using UnityEngine;

public class Pila<T>
{
    private NodoGenerico<T> tope;

    public Pila()
    {
        tope = null;
    }

    public Pila(T[] arreglo)
    {
        tope = null;

        for (int i = arreglo.Length - 1; i >= 0; i--)
        {
            Apilar(arreglo[i]);
        }
    }

    public Pila(List<T> lista)
    {
        tope = null;

        for (int i = lista.Count - 1; i >= 0; i--)
        {
            Apilar(lista[i]);
        }
    }

    public void Apilar(T elemento)
    {
        NodoGenerico<T> nuevo = new NodoGenerico<T>(elemento, tope);
        tope = nuevo;
    }

    public T Desapilar()
    {
        if (PilaVacia())
        {
            Debug.Log("La pila está vacía.");
            return default(T);
        }

        T valor = tope.valor;
        tope = tope.siguiente;
        return valor;
    }

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

    public bool PilaVacia()
    {
        return tope == null;
    }

    public int Contar()
    {
        int cantidad = 0;
        NodoGenerico<T> actual = tope;

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
        NodoGenerico<T> actual = tope;

        while (actual != null)
        {
            lista.Add(actual.valor);
            actual = actual.siguiente;
        }

        return lista;
    }
}
