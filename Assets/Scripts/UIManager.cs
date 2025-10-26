using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player Info")]
    //public Player player;

    public TextMeshProUGUI playerMoneyText;
    public TextMeshProUGUI playerReputationText;

    //public GameObject gameOverPanel;
    //public CustomerBehaviour feedback;

    void Start()
    {
        //player = FindAnyObjectByType<Player>();
    }

    
    void Update()
    {
        playerMoneyText.text = Player.Instance.money.ToString();
        playerReputationText.text = Player.Instance.Reputation.ToString();
    }
}
