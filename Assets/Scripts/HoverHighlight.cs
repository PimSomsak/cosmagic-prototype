using SpriteGlow;
using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    private SpriteGlowEffect glow;

    private void Start()
    {
        glow = GetComponent<SpriteGlowEffect>();
        if (glow != null)
        {
            glow.enabled = false;
        }
    }
    private void OnMouseEnter()
    {
        if (glow != null)
        {
            glow.enabled = true;
        }
    }
    private void OnMouseExit()
    {
        if (glow != null)
        {
            glow.enabled = false;
        }
    }
}
