using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Order Paper Info")]

    public TextMeshProUGUI customerNameText;
    public string customerName;

    public TextMeshProUGUI customerFeedbackText;
    public string customerFeedback;

    public TextMeshProUGUI budgetValueText;
    public int budgetValue;

    public TextMeshProUGUI terminationValueText;
    public int terminationValue;

    public TextMeshProUGUI reputationValueText;
    public int reputationValue;

    [Header("Player Info")]

    public TextMeshProUGUI playerMoneyText;
    public int playerMoney;

    public TextMeshProUGUI playerReputationText;
    public int playerReputationValue;


    void Start()
    {
        
    }

    
    void Update()
    {
        //customerNameText.text = customerName;
        //customerFeedbackText.text = customerFeedback;
        //budgetValueText.text = budgetValue.ToString();
        //terminationValueText.text = terminationValue.ToString();
        //reputationValueText.text = reputationValue.ToString();
    }
}
