using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string frontSceneName = "FrontScene";
    public string backSceneName = "BackScene";

    private bool isFrontActive = true;

    private void Start()
    {
        
        if (!SceneManager.GetSceneByName(frontSceneName).isLoaded)
            SceneManager.LoadSceneAsync(frontSceneName, LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName(backSceneName).isLoaded)
            SceneManager.LoadSceneAsync(backSceneName, LoadSceneMode.Additive);

        
        SetSceneActive(frontSceneName, true);
        SetSceneActive(backSceneName, false);
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        if (isFrontActive)
        {
            SetSceneActive(frontSceneName, false);
            SetSceneActive(backSceneName, true);
        }
        else
        {
            SetSceneActive(frontSceneName, true);
            SetSceneActive(backSceneName, false);
        }

        isFrontActive = !isFrontActive;
    }

    private void SetSceneActive(string sceneName, bool isActive)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            foreach (GameObject go in scene.GetRootGameObjects())
            {
                go.SetActive(isActive);
            }
        }
    }
}
