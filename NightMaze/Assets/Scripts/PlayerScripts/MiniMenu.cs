using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniMenu : MonoBehaviour
{
    [SerializeField] GameObject miniMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] DataPresistantManager dataManager;

    public void Pause()
    {
        miniMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Menu()
    {
        dataManager.SaveGame();
        SceneManager.LoadScene("Main");
    }

    public void Restart()
    {
        dataManager.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Close()
    {
        miniMenu.SetActive(false);
        pauseButton.SetActive(true);
    }
}
