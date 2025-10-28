using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOnClick : MonoBehaviour
{
    [Header("prefabToSpawn")]
    public GameObject prefabToSpawn;

    [Header("spawnPoint")]
    public Transform spawnPoint;

    public TextMeshProUGUI priceText;
    public float price;

    void Start()
    {
        if (priceText != null)
        {
            priceText.text = price.ToString();
        }
    }
    void OnMouseDown()
    {
        if (prefabToSpawn != null)
        {
            if (Player.Instance.money >= price)
            {
                Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
                GameObject newObj = Instantiate(prefabToSpawn, pos, Quaternion.identity);
                Debug.Log($"Spawned prefab with tag {newObj.tag}!");

                Player.Instance.Spend(price);
                Debug.Log($"Spend {price}, Total Money: {Player.Instance.money}");
            }
            else { Debug.Log("Not Enough Money"); }
        }
    }
}
