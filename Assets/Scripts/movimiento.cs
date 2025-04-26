using System.Collections;  // Agrega esta l�nea
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public Transform player;  // El objeto del jugador
    public float moveSpeed = 5f;  // Velocidad de movimiento
    private Transform targetObject;  // Objeto al que se mover� el jugador

    // Asigna el objeto de inicio al principio
    public Transform startPosition;

    void Start()
    {
        // Establece la posici�n inicial del jugador
        if (startPosition != null)
        {
            player.position = startPosition.position;
        }
    }

    // M�todo para mover el jugador hacia el objetivo
    public void MoverJugador(Transform objetivo)
    {
        targetObject = objetivo;
        StartCoroutine(MoverHaciaObjeto());
    }

    // Corrutina para mover al jugador suavemente hacia el objetivo
    private IEnumerator MoverHaciaObjeto()
    {
        while (Vector3.Distance(player.position, targetObject.position) > 0.1f)
        {
            player.position = Vector3.MoveTowards(player.position, targetObject.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}