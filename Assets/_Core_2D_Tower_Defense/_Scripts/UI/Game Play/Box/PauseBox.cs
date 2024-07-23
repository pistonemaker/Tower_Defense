using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseBox : MonoBehaviour
{
    public Button closeButton;
    public Button restartButton;
    public Button menuButton;
    
    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        Time.timeScale = 0f;
        closeButton.onClick.AddListener(CloseBox);
        restartButton.onClick.AddListener(RestartLevel);
        menuButton.onClick.AddListener(ReturnMenu);
    }
    
    private void CloseBox()
    {
        gameObject.SetActive(false);
        UIManager.Instance.ResumeGame();
    }

    private void RestartLevel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Game");
    }

    private void ReturnMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Menu");
    }
}
