using UnityEngine;

public class DragWithLayer : MonoBehaviour
{
    private int originalLayer;

    void OnMouseDown()
    {
        
        originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Dragging");
    }

    void OnMouseUp()
    {
        gameObject.layer = originalLayer;
    }
}
