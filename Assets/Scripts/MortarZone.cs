using UnityEngine;

public class MortarZone : MonoBehaviour
{
    public string targetTag = "Ingredient";       
    public string crushableLayer = "Crushable";    
    public string defaultLayer = "Ingredient";     

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            other.gameObject.layer = LayerMask.NameToLayer(crushableLayer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            other.gameObject.layer = LayerMask.NameToLayer(defaultLayer);
        }
    }
}