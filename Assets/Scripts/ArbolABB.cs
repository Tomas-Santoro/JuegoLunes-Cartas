using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodoABB
{
    public Enemigo enemigo;
    public NodoABB izquierda;
    public NodoABB derecha;

    public NodoABB(Enemigo enemigo)
    {
        this.enemigo = enemigo;
        izquierda = null;
        derecha = null;
    }
}

public class ArbolABB
{
    public NodoABB raiz;

    public void Insertar(Enemigo enemigo)
    {
        raiz = InsertarRec(raiz, enemigo);
    }

    private NodoABB InsertarRec(NodoABB nodo, Enemigo enemigo)
    {
        if (nodo == null) return new NodoABB(enemigo);

        if (enemigo.poder < nodo.enemigo.poder)
            nodo.izquierda = InsertarRec(nodo.izquierda, enemigo);
        else
            nodo.derecha = InsertarRec(nodo.derecha, enemigo);

        return nodo;
    }

    private void RecorrerEnOrden(NodoABB nodo, List<Enemigo> lista)
    {
        if (nodo == null) return;
        RecorrerEnOrden(nodo.izquierda, lista);
        lista.Add(nodo.enemigo);
        RecorrerEnOrden(nodo.derecha, lista);
    }

    public List<Enemigo> ObtenerEnemigosOrdenados()
    {
        List<Enemigo> lista = new List<Enemigo>();
        RecorrerEnOrden(raiz, lista);
        return lista;
    }
}
