using UnityEngine;

public class Potion : MonoBehaviour
{
    public string itemName;
    public IngredientType type; // Active, Additive, Solvent

    // Color Ex. 1 0 0 Red, 0 1 0 Yellow, 0 0 1 Blue | 1 1 0 Orange = Red + Yellow
    public Vector3 color = new Vector3(0, 0, 0);

    // Active
    public CurseResistace curseResistance;
    public UniqueMagic uniqueMagic;

    // Solvent
    public int moisture; // 1-4
    public int durability; // 1-4

    // Additive
    public Gloss gloss;
    public int allergy; // 1-4

    public void Initialize(Vector3 color, CurseResistace curse, UniqueMagic magic, int moisture, int durability, Gloss gloss, int allergy)
    {
        this.color = color;
        curseResistance = curse;
        uniqueMagic = magic;
        this.moisture = moisture;
        this.durability = durability;
        this.gloss = gloss;
        this.allergy = allergy;
    }
    public float GetTotalCost()
    {
        return 100f;
    }
}
