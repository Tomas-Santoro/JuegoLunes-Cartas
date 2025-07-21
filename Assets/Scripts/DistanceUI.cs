using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DistanceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] nodosTexto;
    [SerializeField] private Transform[] nodosTransform;
    [SerializeField] private Transform jugadorTransform;

    private NodoDistancia[] nodosDistancia;
    private QuickSort _quicksort = new QuickSort();

    private void Start()
    {
        nodosDistancia = new NodoDistancia[nodosTransform.Length];
        for (int i = 0; i < nodosTransform.Length; i++)
        {
            nodosDistancia[i] = new NodoDistancia
            {
                transform = nodosTransform[i],
                distancia = 0
            };
        }
    }

    private void Update()
    {
        MedirDistancias();
        OrdenarPorDistancia();
        MostrarNombresOrdenados();
    }

    private void MedirDistancias()
    {
        for (int i = 0; i < nodosDistancia.Length; i++)
        {
            nodosDistancia[i].distancia = (int)Vector3.Distance(jugadorTransform.position, nodosDistancia[i].transform.position);
        }
    }

    private void OrdenarPorDistancia()
    {
        // Creamos un array de distancia con índices de referencia
        (int distancia, NodoDistancia nodo)[] pares = new (int, NodoDistancia)[nodosDistancia.Length];
        for (int i = 0; i < nodosDistancia.Length; i++)
        {
            pares[i] = (nodosDistancia[i].distancia, nodosDistancia[i]);
        }

        // Extraemos solo las distancias y las ordenamos
        int[] distancias = new int[pares.Length];
        for (int i = 0; i < pares.Length; i++)
        {
            distancias[i] = pares[i].distancia;
        }
        int[] ordenadas = _quicksort.QSort(distancias, 0, distancias.Length - 1);

        // Reasignamos nodos respetando distancias y sin duplicados
        NodoDistancia[] resultado = new NodoDistancia[pares.Length];
        HashSet<int> usados = new HashSet<int>();

        for (int i = 0; i < ordenadas.Length; i++)
        {
            for (int j = 0; j < pares.Length; j++)
            {
                if (pares[j].distancia == ordenadas[i] && !usados.Contains(j))
                {
                    resultado[i] = pares[j].nodo;
                    usados.Add(j);
                    break;
                }
            }
        }

        nodosDistancia = resultado;
    }

    /*private void MostrarNombresOrdenados()
    {
        for (int i = 0; i < nodosTexto.Length && i < nodosDistancia.Length; i++)
        {
            nodosTexto[i].text = nodosDistancia[i].transform.name;
        }
    }*/
    private void MostrarNombresOrdenados()
    {
        for (int i = 0; i < nodosTexto.Length && i < nodosDistancia.Length; i++)
        {
            string nombre = nodosDistancia[i].transform.name;
            int distancia = nodosDistancia[i].distancia;
            nodosTexto[i].text = $"{nombre} - {distancia}m";
        }
    }


    private class NodoDistancia
    {
        public Transform transform;
        public int distancia;
    }
}
