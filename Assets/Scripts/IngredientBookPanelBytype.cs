using UnityEngine;
using UnityEngine.UI;

public class IngredientBookPanelBytype : MonoBehaviour
{
    [Header("Active Pages")]
    public GameObject[] pagesActive;

    [Header("Solvent Pages")]
    public GameObject[] pagesSolvent;

    [Header("Additive Pages")]
    public GameObject[] pagesAdditive;

    [Header("Bookmark Buttons")]
    public Button buttonActive;
    public Button buttonSolvent;
    public Button buttonAdditive;

    [Header("Navigation Buttons")]
    public Button buttonPrev;
    public Button buttonNext;

    
    private enum BookType { Active, Solvent, Additive }
    private BookType currentBook = BookType.Active;

    
    private int pageActive = 0;
    private int pageSolvent = 0;
    private int pageAdditive = 0;


    void Start()
    {
        
        buttonPrev.onClick.AddListener(PrevPage);
        buttonNext.onClick.AddListener(NextPage);

        
        buttonActive.onClick.AddListener(() => SwitchBook(BookType.Active));
        buttonSolvent.onClick.AddListener(() => SwitchBook(BookType.Solvent));
        buttonAdditive.onClick.AddListener(() => SwitchBook(BookType.Additive));

        
        SwitchBook(BookType.Active);
    }

    void SwitchBook(BookType book)
    {
        currentBook = book;

        
        HideAllPages();

        
        switch (book)
        {
            case BookType.Active:
                ShowPageFromSet(pagesActive, pageActive);
                break;

            case BookType.Solvent:
                ShowPageFromSet(pagesSolvent, pageSolvent);
                break;

            case BookType.Additive:
                ShowPageFromSet(pagesAdditive, pageAdditive);
                break;
        }

        SFXManager.Instance.PlaySFX("FlippingBook");
    }

    
    void NextPage()
    {
        switch (currentBook)
        {
            case BookType.Active:
                if (pageActive < pagesActive.Length - 1) pageActive++;
                ShowPageFromSet(pagesActive, pageActive);
                break;

            case BookType.Solvent:
                if (pageSolvent < pagesSolvent.Length - 1) pageSolvent++;
                ShowPageFromSet(pagesSolvent, pageSolvent);
                break;

            case BookType.Additive:
                if (pageAdditive < pagesAdditive.Length - 1) pageAdditive++;
                ShowPageFromSet(pagesAdditive, pageAdditive);
                break;
        }

        SFXManager.Instance.PlaySFX("FlippingBook");
    }

    void PrevPage()
    {
        switch (currentBook)
        {
            case BookType.Active:
                if (pageActive > 0) pageActive--;
                ShowPageFromSet(pagesActive, pageActive);
                break;

            case BookType.Solvent:
                if (pageSolvent > 0) pageSolvent--;
                ShowPageFromSet(pagesSolvent, pageSolvent);
                break;

            case BookType.Additive:
                if (pageAdditive > 0) pageAdditive--;
                ShowPageFromSet(pagesAdditive, pageAdditive);
                break;
        }

        SFXManager.Instance.PlaySFX("FlippingBook");
    }

    
    void ShowPageFromSet(GameObject[] pages, int index)
    {
        for (int i = 0; i < pages.Length; i++)
            pages[i].SetActive(i == index);

        buttonPrev.interactable = index > 0;
        buttonNext.interactable = index < pages.Length - 1;
    }

    void HideAllPages()
    {
        foreach (GameObject p in pagesActive) p.SetActive(false);
        foreach (GameObject p in pagesSolvent) p.SetActive(false);
        foreach (GameObject p in pagesAdditive) p.SetActive(false);
    }
}

