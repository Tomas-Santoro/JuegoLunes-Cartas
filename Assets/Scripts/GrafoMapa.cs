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

    public List<Nodo> ObtenerCaminoMasCorto(Nodo origen, Nodo destino)
    {
        Dictionary<Nodo, float> distancias = new Dictionary<Nodo, float>();
        Dictionary<Nodo, Nodo> anteriores = new Dictionary<Nodo, Nodo>();
        List<Nodo> noVisitados = new List<Nodo>(nodos);

        foreach (Nodo nodo in nodos)
        {
            distancias[nodo] = float.MaxValue;
            anteriores[nodo] = null;
        }

        distancias[origen] = 0;

        while (noVisitados.Count > 0)
        {
            noVisitados.Sort((a, b) => distancias[a].CompareTo(distancias[b]));
            Nodo actual = noVisitados[0];
            noVisitados.RemoveAt(0);

            if (actual == destino)
                break;

            foreach (Nodo vecino in actual.conexiones)
            {
                float distancia = Vector3.Distance(actual.posicion, vecino.posicion);
                float nuevaDistancia = distancias[actual] + distancia;

                if (nuevaDistancia < distancias[vecino])
                {
                    distancias[vecino] = nuevaDistancia;
                    anteriores[vecino] = actual;
                }
            }
        }

        // Reconstruir el camino
        List<Nodo> camino = new List<Nodo>();
        Nodo nodoActual = destino;
        while (nodoActual != null)
        {
            camino.Insert(0, nodoActual);
            nodoActual = anteriores[nodoActual];
        }

        return camino;
    }
}
