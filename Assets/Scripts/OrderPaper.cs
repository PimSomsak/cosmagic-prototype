using TMPro;
using UnityEngine;

public class OrderPaper : MonoBehaviour
{
    public Customer customerData;

    [Header("Order Paper Info")]

    public TextMeshProUGUI customerNameText;
    public TextMeshProUGUI customerRequestText;
    public TextMeshProUGUI budgetValueText;
    public TextMeshProUGUI terminationValueText;
    public TextMeshProUGUI reputationValueText;

    void Start()
    {

    }

    void Update()
    {
        customerNameText.text = customerData.customerName;
        customerRequestText.text = customerData.description;
        budgetValueText.text = customerData.budget.ToString();
        terminationValueText.text = customerData.terminationFee.ToString();
        reputationValueText.text = customerData.reputation.ToString();
    }
}
