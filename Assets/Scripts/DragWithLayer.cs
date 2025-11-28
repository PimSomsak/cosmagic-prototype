using UnityEngine;

public class DragWithLayer : MonoBehaviour
{
    private int originalLayer;
    private bool isInMortar = false;
    void OnMouseDown()
    {
        
        originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Dragging");
    }

    void OnMouseUp()
    {
        if (originalLayer == LayerMask.NameToLayer("Crushable"))
        {
            if (!isInMortar)
            {
                gameObject.layer = LayerMask.NameToLayer("Ingredient");
            }
            else
            {
                gameObject.layer = originalLayer;
            }
        }
        else if (originalLayer == LayerMask.NameToLayer("Ingredient"))
        {
            if (isInMortar)
            {
                gameObject.layer = LayerMask.NameToLayer("Crushable");
            }
            else
            {
                gameObject.layer = originalLayer;
            }
        }
        else
        {
            gameObject.layer = originalLayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MortarZone"))
        {
            isInMortar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MortarZone"))
        {
            isInMortar = false;
        }
    }
}
