using System.Collections.Generic;
using UnityEngine;

public class GrafoMapa
{
    public List<Nodo> nodos = new List<Nodo>(); // Lista de todos los nodos del grafo

    // Agrega un nodo al grafo
    public void AgregarNodo(Nodo nodo)
    {
        nodos.Add(nodo);
    }

    // Conecta dos nodos de manera bidireccional
    public void ConectarNodos(Nodo a, Nodo b)
    {
        a.AgregarConexion(b);
        b.AgregarConexion(a); // Si querés que sea bidireccional
    }
}
