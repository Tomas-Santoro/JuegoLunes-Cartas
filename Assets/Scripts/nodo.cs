using UnityEngine;
using System.Collections.Generic;

public class Nodo
{
    public string nombre;
    public Vector3 posicion;
    public List<Arista> conexiones; // ✅ Ahora conexiones son aristas con peso
    public GameObject go; // Referencia al GameObject visual (nodo)

    public Nodo(string nombre, Vector3 posicion, GameObject go)
    {
        this.nombre = nombre;
        this.posicion = posicion;
        this.go = go;
        conexiones = new List<Arista>();
    }

    public void AgregarConexion(Nodo destino, float peso)
    {
        if (!conexiones.Exists(a => a.destino == destino))
        {
            conexiones.Add(new Arista(destino, peso));
        }
    }
}
