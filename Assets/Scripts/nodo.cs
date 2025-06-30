using UnityEngine;
using System.Collections.Generic;

public class Nodo
{
    public string nombre;
    public Vector3 posicion;
    public List<Nodo> conexiones;
    public GameObject go; // ✅ Referencia al GameObject visual (nodo)
    public Enemigo enemigo;

    public Nodo(string nombre, Vector3 posicion, GameObject go, Enemigo enemigo = null)
    {
        this.nombre = nombre;
        this.posicion = posicion;
        this.go = go;
        this.enemigo = enemigo;
        conexiones = new List<Nodo>();
    }

    public void AgregarConexion(Nodo destino)
    {
        if (!conexiones.Contains(destino))
        {
            conexiones.Add(destino);
        }
    }
}
