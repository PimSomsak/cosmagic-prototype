using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.PlayMusic("Game");
    }
}