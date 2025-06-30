using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class UIEnemigosRanking : MonoBehaviour
{
    public Text textoRanking; // Asignar desde el Inspector

    void Start()
    {
        if (textoRanking == null)
        {
            Debug.LogError("❌ No se asignó el Text para mostrar el ranking.");
            return;
        }

        MostrarRanking();
    }

    void MostrarRanking()
    {
        if (MapaManager.arbolPoderEnemigos != null)
        {
            List<Enemigo> enemigosOrdenados = MapaManager.arbolPoderEnemigos.ObtenerEnemigosOrdenados();

            textoRanking.text = "🏆 RANKING DE ENEMIGOS (Poder de menor a mayor)\n\n";

            int posicion = 1;
            foreach (var enemigo in enemigosOrdenados)
            {
                textoRanking.text += $"{posicion}. {enemigo.nombre} - Poder: {enemigo.poder}\n";
                posicion++;
            }

            if (enemigosOrdenados.Count == 0)
                textoRanking.text = "No hay enemigos en el ranking.";
        }
        else
        {
            textoRanking.text = "Error: Árbol no inicializado.";
        }
    }
}
