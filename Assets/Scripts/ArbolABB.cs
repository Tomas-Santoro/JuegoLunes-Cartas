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

    // 🔥 MÉTODO NUEVO: eliminar enemigo por referencia
    public void Eliminar(Enemigo enemigo)
    {
        raiz = EliminarRec(raiz, enemigo);
    }

    private NodoABB EliminarRec(NodoABB nodo, Enemigo enemigo)
    {
        if (nodo == null) return null;

        if (enemigo.poder < nodo.enemigo.poder)
        {
            nodo.izquierda = EliminarRec(nodo.izquierda, enemigo);
        }
        else if (enemigo.poder > nodo.enemigo.poder)
        {
            nodo.derecha = EliminarRec(nodo.derecha, enemigo);
        }
        else if (enemigo == nodo.enemigo) // Comparación directa por instancia
        {
            // Caso 1: sin hijos
            if (nodo.izquierda == null && nodo.derecha == null)
                return null;

            // Caso 2: un solo hijo
            if (nodo.izquierda == null)
                return nodo.derecha;
            if (nodo.derecha == null)
                return nodo.izquierda;

            // Caso 3: dos hijos
            NodoABB sucesor = EncontrarMin(nodo.derecha);
            nodo.enemigo = sucesor.enemigo;
            nodo.derecha = EliminarRec(nodo.derecha, sucesor.enemigo);
        }

        return nodo;
    }

    private NodoABB EncontrarMin(NodoABB nodo)
    {
        while (nodo.izquierda != null)
            nodo = nodo.izquierda;
        return nodo;
    }
}
