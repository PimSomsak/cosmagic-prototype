using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;

    [Header("SFX Clips")]
    public AudioClip grabObject;
    public AudioClip buyIngredient;
    public AudioClip grindingActive;
    public AudioClip pouringSolvent;
    public AudioClip shakingAdditive;
    public AudioClip mixingVessel;
    public AudioClip cauldronBubbling;
    public AudioClip rubbishBin;
    public AudioClip knockTheDoor;
    public AudioClip customerSuccess;
    public AudioClip customerIncorrect;
    public AudioClip flippingBook;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(string clipName)
    {
        switch (clipName)
        {
            case "GrabObject":
                PlaySFX(grabObject);
                break;
            case "BuyIngredient":
                PlaySFX(buyIngredient);
                break;
            case "GrindingActive":
                PlaySFX(grindingActive);
                break;
            case "PouringSolvent":
                PlaySFX(pouringSolvent);
                break;
            case "ShakingAdditive":
                PlaySFX(shakingAdditive);
                break;
            case "MixingVessel":
                PlaySFX(mixingVessel);
                break;
            case "CauldronBubbling":
                PlaySFX(cauldronBubbling);
                break;
            case "FlippingBook":
                PlaySFX(flippingBook);
                break;
            case "RubbishBin":
                PlaySFX(rubbishBin);
                break;
            case "KnockTheDoor":
                PlaySFX(knockTheDoor);
                break;
            case "CustomerSuccess":
                PlaySFX(customerSuccess);
                break;
            case "CustomerIncorrect":
                PlaySFX(customerIncorrect);
                break;
            default:
                Debug.LogWarning("SFX not found: " + clipName);
                break;
        }
    }
}