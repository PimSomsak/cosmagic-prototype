using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float money = 50f;
    public float playerReputation = 0;

    private void Awake()
    {
        Instance = this;
        Debug.Log($"Total balance: {money}");
    }

    public bool Spend(float amount)
    {
        if (money >= amount)
        {
            money -= amount;
            Debug.Log($"Spent {amount} coins. Remaining: {money}");
            return true;
        }
        return false;
    }

    public void AddMoney(float amount)
    {
        money += amount;
        Debug.Log($"Received {amount} coins. Total: {money}");
    }

    public void AddReputation(float amount)
    {
        playerReputation += amount;
        Debug.Log($"Recieved {amount} of repuations. Total: {playerReputation}");
    }
}
