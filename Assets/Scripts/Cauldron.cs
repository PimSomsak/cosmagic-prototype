using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [System.Serializable]
    public class ShowIngredient
    {
        public GameObject ingredientAdd;
        public GameObject spriteToShow;
        public Transform spawnPoint;
        [HideInInspector] public GameObject currentSprite;
    }
    public ShowIngredient[] showIngredients;

    public GameObject potionDefaultPrefab;
    public GameObject potionRedPrefab;
    public GameObject potionBluePrefab;
    public GameObject potionYellowPrefab;
    public GameObject potionGreenPrefab;
    public GameObject potionOrangePrefab;
    public GameObject potionPurplePrefab;
    public GameObject potionBlackPrefab;

    private Dictionary<IngredientType, Ingredients> selectedIngredients = new();

    // ช้อนอยู่ในหม้อมั้ย
    private bool spoonInside = false;

    //[Header("UI")]
    //public Slider stirProgressSlider;

    private int hits = 0;
    public int requiredHits = 8;
    public float stirThreshold = 0.1f;

    private Vector3 finalColor;
    private CurseResistace finalCurseResist; private UniqueMagic finalMagic;
    private int finalMoisture; private int finalDurability;
    private Gloss finalGloss; private int finalAllergy;

    /*private int solventRequire = 15;
    private int solventCount = 0;
    private int additiveRequire = 15;
    private int additiveCount = 0;*/


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spoon"))
        {
            spoonInside = true;
            Debug.Log("[CauldronRecipe] Spoon entered cauldron!");
        }

        IngredientData data = other.GetComponent<IngredientData>();
        if (data != null)
        {
            // Replace the Selected one with the new one
            var type = data.ingredientData.type;
            selectedIngredients[type] = data.ingredientData;
            Debug.Log($"{data.ingredientData.itemName} added.");
            Destroy(other.gameObject);
            AddStats();

            foreach (var item in showIngredients)
            {
                if (item.ingredientAdd == null) continue;

                if (other.gameObject.name.Contains(item.ingredientAdd.name))
                {
                    foreach (var newItem in showIngredients)
                    {
                        if (newItem.spawnPoint == item.spawnPoint && newItem.currentSprite != null)
                        {
                            Destroy(newItem.currentSprite);
                            newItem.currentSprite = null;
                        }
                    }

                    if (item.spriteToShow != null && item.spawnPoint != null)
                    {
                        GameObject spawned = Instantiate(item.spriteToShow, item.spawnPoint.position, Quaternion.identity);
                        spawned.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        item.currentSprite = spawned;

                        Debug.Log($"Spawned {other.gameObject.name}");
                    }
                }
            }
        }
        /*else if (data == null)
        {
            if (gameObject.name == "Water" || gameObject.name == "Water(Clone)")
            {
                solventCount++;
                if (solventCount == solventRequire)
                {
                }
            }
            else if ()
            {

            }
        }*/
    }
    void AddStats()
    {
        // Get 0 Stats
        finalColor = new Vector3(0, 0, 0);
        finalCurseResist = CurseResistace.Any; finalMagic = UniqueMagic.Any;
        finalMoisture = 0; finalDurability = 0;
        finalGloss = Gloss.Any; finalAllergy = 0;

        foreach (var ingredient in selectedIngredients.Values)
        {
            finalColor += ingredient.color;
            finalColor = new Vector3(
                Mathf.Clamp(finalColor.x, 0f, 1f),
                Mathf.Clamp(finalColor.y, 0f, 1f),
                Mathf.Clamp(finalColor.z, 0f, 1f));

            if (ingredient.type == IngredientType.Active && ingredient.curseResistance != CurseResistace.Any)
            {
                finalCurseResist = ingredient.curseResistance;
            }
            if (ingredient.type == IngredientType.Active && ingredient.uniqueMagic != UniqueMagic.Any)
            {
                finalMagic = ingredient.uniqueMagic;
            }

            finalMoisture += ingredient.moisture;
            finalDurability += ingredient.durability;
            
            if (ingredient.type == IngredientType.Additive && ingredient.gloss != Gloss.Any)
            {
                finalGloss = ingredient.gloss;
            }
            finalAllergy += ingredient.allergy;
        }
    }

    public void ResetCauldron()
    {
        // Reset totalAttributes
        finalColor = new Vector3(0, 0, 0);
        finalCurseResist = new CurseResistace(); finalMagic = new UniqueMagic();
        finalMoisture = 0; finalDurability = 0;
        finalGloss = new Gloss(); finalAllergy = 0;

        hits = 0;
        selectedIngredients.Clear();
    }

    public void SpawnPotion()
    {
        GameObject prefabToSpawn;

        if (finalColor == new Vector3(1, 0, 0))
            prefabToSpawn = potionRedPrefab;
        else if (finalColor == new Vector3(0, 0, 1))
            prefabToSpawn = potionBluePrefab;
        else if (finalColor == new Vector3(0, 1, 0))
            prefabToSpawn = potionYellowPrefab;
        else if (finalColor == new Vector3(1, 1, 0))
            prefabToSpawn = potionOrangePrefab;
        else if (finalColor == new Vector3(0, 1, 1))
            prefabToSpawn = potionGreenPrefab;
        else if (finalColor == new Vector3(1, 0, 1))
            prefabToSpawn = potionPurplePrefab;
        else if (finalColor == new Vector3(1, 1, 1))
            prefabToSpawn = potionBlackPrefab;
        else {prefabToSpawn = potionDefaultPrefab; }
            


        GameObject newPotion = Instantiate(prefabToSpawn, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        Potion potionObj = newPotion.GetComponent<Potion>();
        potionObj.Initialize(finalColor, finalCurseResist, finalMagic, finalMoisture, finalDurability, finalGloss, finalAllergy);
        Debug.Log($"Mixing Complete! Potion has been created!");
    }
    public void Stir()
    {
        if (selectedIngredients.Count < 3) return;
        hits++;
        Debug.Log("Hit");

        if (hits == requiredHits)
        {
            SpawnPotion();
            ResetCauldron();
            DestroyAllCurrentSprites();
        }
    }
    public void OnSpoonStir(float strength)
    {
        float minStrength = stirThreshold;
        if (strength >= minStrength && spoonInside)
        {
            Debug.Log($"Spoon stir detected! Strength: {strength:F2}");
            Stir();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Spoon"))
        {
            spoonInside = false;
        }
    }

    public void DestroyAllCurrentSprites()
    {
        foreach (var item in showIngredients)
        {
            if (item.currentSprite != null)
            {
                Destroy(item.currentSprite);
                item.currentSprite = null;
            }
        }
    }
}