using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System.Collections;

public class UIEnemigosRanking : MonoBehaviour
{
    //public Text textoRanking; // Asignar desde el Inspector
    public TextMeshProUGUI textoRanking;
    /*void Start()
    {
        if (textoRanking == null)
        {
            Debug.LogError("No se asignó el Text para mostrar el ranking.");
            return;
        }

        MostrarRanking();
    }*/
    
    void Start()
    {
        StartCoroutine(EsperarYMostrarRanking());
    }

    private IEnumerator EsperarYMostrarRanking()
    {
        //yield return new WaitForEndOfFrame(); // También podés probar con new WaitForSeconds(0.1f)
        yield return new WaitForSeconds(0.1f);
        MostrarRanking();
    }


    void MostrarRanking()
    {
        if (MapaManager.arbolPoderEnemigos != null)
        {
            List<Enemigo> enemigosOrdenados = MapaManager.arbolPoderEnemigos.ObtenerEnemigosOrdenados();

            textoRanking.text = "RANKING DE ENEMIGOS\n(Poder de menor a mayor)\n\n";

            int posicion = 1;
            foreach (var enemigo in enemigosOrdenados)
            {
                //textoRanking.text += $"{posicion}. {enemigo.nombre}:\n - Vida: {enemigo.vida} \n - Poder: {enemigo.poder}\n";
                textoRanking.text += $"{posicion}. {enemigo.nombre}:\n - Poder: {enemigo.poder}\n";
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
