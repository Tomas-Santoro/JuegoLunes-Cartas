using System.Collections.Generic;
using UnityEngine;

public class Nodo
{
    public string nombre;           // Nombre del nodo
    public Vector3 posicion;        // Posición en la escena
    public List<Nodo> conexiones;   // Lista de conexiones
    public GameObject gameObjectNodo; // ✅ Nuevo: referencia al GameObject del nodo

    // Constructor actualizado con 3 parámetros
    public Nodo(string nombre, Vector3 posicion, GameObject gameObjectNodo)
    {
        this.nombre = nombre;
        this.posicion = posicion;
        this.gameObjectNodo = gameObjectNodo;
        conexiones = new List<Nodo>();
    }

    // Método para agregar conexión
    public void AgregarConexion(Nodo destino)
    {
        if (!conexiones.Contains(destino))
        {
            conexiones.Add(destino);
        }
    }
}
