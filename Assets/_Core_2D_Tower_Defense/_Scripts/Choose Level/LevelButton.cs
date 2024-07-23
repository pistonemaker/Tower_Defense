using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button levelButton;
    public Image check;
    public TextMeshProUGUI levelName;

    public void Init(int levelID, bool pass)
    {
        levelName.text = (levelID + 1).ToString();
        levelButton.onClick.AddListener(() => LoadLevel(levelID));
        if (pass)
        {
            check.gameObject.SetActive(true);
        }
        else
        {
            check.gameObject.SetActive(false);
        }
    }

    private void LoadLevel(int levelID)
    {
        PlayerPrefs.SetInt(DataKey.Cur_Level, levelID);
        SceneManager.LoadSceneAsync("Game");
    }
}
