using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    

    public int playerMoney = 0;

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log("Money: " + playerMoney);
    }
}
