using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseBox : Singleton<LoseBox>
{
    public Button closeButton;
    public Button restartButton;
    public Button menuButton;
    private bool canClick = false;
    public TextMeshProUGUI coinText;
    public int coinGet;
    
    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySFX("Defeat");
        ShowCoin();
        closeButton.onClick.AddListener(CloseBox);
        restartButton.onClick.AddListener(RestartLevel);
        menuButton.onClick.AddListener(ReturnMenu);
    }
    
    private void ShowCoin()
    {
        DOTween.To(value => coinText.text = ((int)value).ToString(), 0,
            coinGet, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => { canClick = true;});
    }
    
    private void CloseBox()
    {
        if (canClick)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Choose Level");
        }
    }

    private void RestartLevel()
    {
        if (canClick)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Game");
        }
    }

    private void ReturnMenu()
    {
        if (canClick)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}