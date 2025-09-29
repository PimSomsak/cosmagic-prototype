using UnityEngine;
[CreateAssetMenu(fileName = "NewCustomer", menuName = "Cosmagic/Customer")]
public class Customer : ScriptableObject
{
    public string customerName;
    public float budget;

    // Color Check
    public Vector3 colorMix = new Vector3(0, 0, 0);

    // Active
    public bool isCurseResist;
    public UniqueMagic magic;

    // Solvent
    public Vector2 moistureRange = new Vector2(1, 4);
    public Vector2 durabilityRange = new Vector2(1, 4);

    // Additive
    public bool isGloss;
    public Vector2 allergyRange = new Vector2(1, 4);
}
