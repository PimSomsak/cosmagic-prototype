using UnityEngine;
public enum IngredientType { Active, Additive, Solvent }
public enum UniqueMagic { None, Invisible, Luminous, Regenerate, StoneSkin}
[CreateAssetMenu(fileName = "New Ingredient", menuName = "Cosmagic/Ingredient")]
public class Ingredients : ScriptableObject
{
    public string itemName;
    public IngredientType type; // Active, Additive, Solvent

    // Color Ex. 1 0 0 Red, 0 1 0 Yellow, 0 0 1 Blue | 1 1 0 Orange = Red + Yellow
    public Vector3 color = new Vector3(0, 0, 0);

    // Active
    public bool curseResistance;
    public UniqueMagic uniqueMagic;

    // Solvent
    public int moisture; // 1-4
    public int durability; // 1-3

    // Additive
    public bool gloss; // 1-4
    public int allergy; // 1-4
}