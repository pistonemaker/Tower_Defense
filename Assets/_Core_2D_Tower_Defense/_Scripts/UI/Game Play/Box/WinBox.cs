using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinBox : Singleton<WinBox>
{
    public Button closeButton;
    private bool canClose = false;
    public TextMeshProUGUI coinText;
    public Sprite starYellow;
    public List<Image> starImgs;
    public int coinGet;
    public int star;
    
    private void OnEnable()
    {
        Init();
    }
    
    public void Init()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySFX("Victory");
        ShowCoin();
        closeButton.onClick.AddListener(CloseBox);
    }

    private void ShowCoin()
    {
        DOTween.To(value => coinText.text = ((int)value).ToString(), 0, 
            coinGet, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(ShowStar);
    }

    private void ShowStar()
    {
        StartCoroutine(GainStars());
    }

    private IEnumerator GainStars()
    {
        for (int i = 0; i < star; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);  
            GainStar(i);
        }

        canClose = true;
    }

    private void GainStar(int id)
    {
        starImgs[id].sprite = starYellow;
    }

    private void CloseBox()
    {
        if (canClose)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Choose Level");
        }
    }
}