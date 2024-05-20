using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu1 : MonoBehaviour
{

    [SerializeField] private Button continueGameButton;
    public void Start()
    {
        if (!DataPresistantManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        DataPresistantManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Level");
    }

    public void OnContinueClicked()
    {
        SceneManager.LoadSceneAsync("Level");
    }
}
