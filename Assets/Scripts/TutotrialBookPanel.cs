using UnityEngine;
using UnityEngine.UI;

public class TutorialBookPanel : MonoBehaviour
{
    [Header("Pages in the book")]
    public GameObject[] pages;

    [Header("Navigation Buttons")]
    public Button buttonPrev;
    public Button buttonNext;

    [Header("Exit Button")]
    public Button buttonExit; 

    private int currentPage = 0;

    void Start()
    {
        ShowPage(currentPage);

        buttonPrev.onClick.AddListener(PrevPage);
        buttonNext.onClick.AddListener(NextPage);

        if (buttonExit != null)
            buttonExit.onClick.AddListener(ClosePanel); 
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }

        buttonPrev.interactable = index > 0;
        buttonNext.interactable = index < pages.Length - 1;
    }

    void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
            SFXManager.Instance.PlaySFX("FlippingBook");
        }
    }

    void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
            SFXManager.Instance.PlaySFX("FlippingBook");
        }
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
        SFXManager.Instance.PlaySFX("GrabObject");
    }
}