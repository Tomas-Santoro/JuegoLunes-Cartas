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

    // Conecta dos nodos de manera bidireccional con peso
    public void ConectarNodos(Nodo a, Nodo b)
    {
        float peso = Vector3.Distance(a.posicion, b.posicion); // ✅ Usa distancia real como peso
        a.AgregarConexion(b, peso);
        b.AgregarConexion(a, peso); // Bidireccional
    }
}
