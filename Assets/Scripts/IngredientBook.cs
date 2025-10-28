using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class IngredientBook : MonoBehaviour
{

    public GameObject IngredientGuideBookPanel;
    void OnMouseDown()
    {
        if (IngredientGuideBookPanel != null)
        {
            IngredientGuideBookPanel.SetActive(!IngredientGuideBookPanel.activeSelf);
        }
    }
}
