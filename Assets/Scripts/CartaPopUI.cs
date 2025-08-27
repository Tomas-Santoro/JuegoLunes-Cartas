using UnityEngine;
using UnityEngine.UI; // Por si después querés usar componentes de UI
using System.Collections;

public class CartaPopUI : MonoBehaviour
{
    private Vector3 escalaOriginal;
    public float escalaFactor = 1.15f;
    public float duracion = 0.08f;

    void Start()
    {
        escalaOriginal = transform.localScale;
    }

    // Este método lo vas a asignar en el OnClick del Button
    public void Pop()
    {
        StopAllCoroutines(); // Por si se clickea varias veces
        StartCoroutine(PopEffect());
    }

    IEnumerator PopEffect()
    {
        Vector3 escalaMax = escalaOriginal * escalaFactor;

        float t = 0;
        while (t < duracion)
        {
            transform.localScale = Vector3.Lerp(escalaOriginal, escalaMax, t / duracion);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale = escalaMax;

        t = 0;
        while (t < duracion)
        {
            transform.localScale = Vector3.Lerp(escalaMax, escalaOriginal, t / duracion);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale = escalaOriginal;
    }
}
