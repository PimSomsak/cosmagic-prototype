using UnityEngine;
using UnityEngine.SceneManagement;

public class PayAndLoadScene : MonoBehaviour
{
    public float exitPrice;
    public string sceneToLoad = "OutroScene";
    void OnMouseDown()
    {
        if (Player.Instance.money >= exitPrice)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
