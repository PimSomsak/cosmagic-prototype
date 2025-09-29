using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Customer customerData;

    public void Initialize(Customer data)
    {
        customerData = data;
        Debug.Log($"Customer {customerData.customerName} is ready with budget {customerData.budget}");
    }

    public bool EvaluatePotion(Potion potion)
    {
        float colorDistance = Vector3.Distance(potion.color, customerData.colorMix);
        bool colorOK = colorDistance <= 0;
        bool curseOK = potion.curseResistance || !customerData.isCurseResist;
        bool magicOK = potion.uniqueMagic == customerData.magic|| customerData.magic == UniqueMagic.None;
        bool moistureOK = potion.moisture >= customerData.moistureRange.x && potion.moisture <= customerData.moistureRange.y;
        bool durabilityOK = potion.durability >= customerData.durabilityRange.x && potion.durability <= customerData.durabilityRange.y;
        bool glossOK = potion.gloss || !customerData.isGloss;
        bool allergyOK = potion.allergy >= customerData.allergyRange.x && potion.allergy <= customerData.allergyRange.y;

        return colorOK && curseOK && magicOK && moistureOK && durabilityOK && glossOK && allergyOK;
    }

    private void OnTriggerEnter(Collider other)
    {
        Potion potion = other.GetComponent<Potion>();
        if (potion != null)
        {
            bool result = EvaluatePotion(potion);
            if (result)
            {
                Debug.Log("Customer is satisfied with the potion!");
                Wallet.Instance.AddMoney(customerData.budget);
            }
            else
            {
                Debug.Log("Customer is not satisfied...");
            }

            Destroy(other.gameObject);
        }
    }
}