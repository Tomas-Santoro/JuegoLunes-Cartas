using UnityEngine;
using TMPro;
using System;

public class DistanceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] nodosTexto; // Textos que mostrarán los nombres
    [SerializeField] private Transform[] nodosTransform; // Transforms de los nodos
    [SerializeField] private Transform jugadorTransform; // Transform del jugador
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
        // Creamos un arreglo temporal de distancias
        int[] distanciasTemporales = new int[nodosDistancia.Length];
        for (int i = 0; i < nodosDistancia.Length; i++)
        {
            distanciasTemporales[i] = nodosDistancia[i].distancia;
        }

        // Ordenamos el arreglo y guardamos el resultado
        int[] distanciasOrdenadas = _quicksort.QSort(distanciasTemporales, 0, distanciasTemporales.Length - 1);

        // Creamos un nuevo arreglo ordenado de nodosDistancia
        NodoDistancia[] ordenados = new NodoDistancia[nodosDistancia.Length];
        for (int i = 0; i < distanciasOrdenadas.Length; i++)
        {
            // Buscar el nodo original con esa distancia
            for (int j = 0; j < nodosDistancia.Length; j++)
            {
                if (nodosDistancia[j] != null && nodosDistancia[j].distancia == distanciasOrdenadas[i])
                {
                    ordenados[i] = nodosDistancia[j];
                    nodosDistancia[j] = null; // Evitar duplicados si hay distancias iguales
                    break;
                }
            }
        }

        nodosDistancia = ordenados;
    }

    private void MostrarNombresOrdenados()
    {
        for (int i = 0; i < nodosTexto.Length && i < nodosDistancia.Length; i++)
        {
            nodosTexto[i].text = nodosDistancia[i].transform.name;
        }
    }

    private class NodoDistancia
    {
        public Transform transform;
        public int distancia;
    }
}

