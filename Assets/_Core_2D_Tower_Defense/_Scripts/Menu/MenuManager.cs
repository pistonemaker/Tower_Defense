using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    public Button startButton;
    public Button exitButton;
    public Button infoButton;
    public Button closeInfoButton;

    [SerializeField] private GameObject infoPopup;

    private void Start()
    {
        startButton.onClick.AddListener(LoadChooseLevel);
        
        exitButton.onClick.AddListener(ExitGame);
        
        infoButton.onClick.AddListener(ShowInfo);
        
        closeInfoButton.onClick.AddListener(HideInfo);
    }

    private void LoadChooseLevel()
    {
        SceneManager.LoadSceneAsync("Choose Level");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ShowInfo()
    {
        infoPopup.SetActive(true);
    }

    private void HideInfo()
    {
        infoPopup.SetActive(false);
    }
}
