using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player Info")]

    public TextMeshProUGUI playerMoneyText;
    public Player playerMoney;

    public TextMeshProUGUI playerReputationText;
    public int playerReputationValue;

    //public CustomerBehaviour feedback;

    void Start()
    {

    }

    
    void Update()
    {
        playerMoneyText.text = playerMoney.money.ToString();
    }
}
