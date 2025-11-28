using UnityEngine;

public class OpenTutorial : MonoBehaviour
{
    public GameObject tutorialPanel; // �ҡ TutorialBookPanel ŧ Inspector

    void OnMouseDown()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }
    }
}