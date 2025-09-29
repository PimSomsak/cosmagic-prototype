using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public float price;
    [TextArea] public string description;

    [Header("Attributes")]
    public QualityStats quality;
    public BeautyStats beauty;
    public MagicStats magic;
}