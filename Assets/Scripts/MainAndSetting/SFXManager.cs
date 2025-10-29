using UnityEngine;

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
    public AudioClip rubbishBin;

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
            case "RubbishBin":
                PlaySFX(rubbishBin);
                break;
            default:
                Debug.LogWarning("SFX not found: " + clipName);
                break;
        }
    }
}