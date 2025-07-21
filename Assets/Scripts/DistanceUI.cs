/*using UnityEngine;
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

*/
/*using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DistanceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] nodosTexto; // Textos que mostrarán los nombres
    [SerializeField] private Transform[] nodosTransform;   // Transforms de los nodos
    [SerializeField] private Transform jugadorTransform;   // Transform del jugador

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
        // Creamos un arreglo de distancias
        int[] distanciasTemporales = new int[nodosDistancia.Length];
        for (int i = 0; i < nodosDistancia.Length; i++)
        {
            distanciasTemporales[i] = nodosDistancia[i].distancia;
        }

        // Ordenamos el arreglo
        int[] distanciasOrdenadas = _quicksort.QSort(distanciasTemporales, 0, distanciasTemporales.Length - 1);

        // Diccionario de listas para manejar duplicados
        Dictionary<int, Queue<NodoDistancia>> mapaDistancia = new Dictionary<int, Queue<NodoDistancia>>();
        foreach (var nodo in nodosDistancia)
        {
            if (!mapaDistancia.ContainsKey(nodo.distancia))
                mapaDistancia[nodo.distancia] = new Queue<NodoDistancia>();

            mapaDistancia[nodo.distancia].Enqueue(nodo);
        }

        // Arreglo final ordenado
        NodoDistancia[] ordenados = new NodoDistancia[nodosDistancia.Length];
        for (int i = 0; i < distanciasOrdenadas.Length; i++)
        {
            int dist = distanciasOrdenadas[i];
            if (mapaDistancia.ContainsKey(dist) && mapaDistancia[dist].Count > 0)
            {
                ordenados[i] = mapaDistancia[dist].Dequeue();
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
*/
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
