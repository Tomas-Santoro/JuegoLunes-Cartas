using UnityEngine;
using System.Collections.Generic;

public class NodoVisual : MonoBehaviour
{
    [Tooltip("Arrastrá aquí los GameObjects a los que este nodo se conecta.")]
    public List<GameObject> conexionesGO = new List<GameObject>();
}