using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance;
    public float money = 4000f;

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
}
