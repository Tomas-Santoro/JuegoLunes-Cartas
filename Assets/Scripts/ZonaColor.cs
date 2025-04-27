using UnityEngine;
using UnityEngine.UI;

public class ZonaColorCambio : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color colorOriginal;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;
    }

 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            spriteRenderer.color = Color.red; 
        }
    }

   
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = colorOriginal;
        }
    }
}