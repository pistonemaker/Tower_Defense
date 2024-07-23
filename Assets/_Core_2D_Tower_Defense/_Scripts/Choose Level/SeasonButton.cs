using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeasonButton : MonoBehaviour
{
    public Button seasonButton;
    public TextMeshProUGUI seasonName;
    public TextMeshProUGUI seasonProcess;

    public void Init(int seasonID)
    {
        seasonName.text = "Season " + (seasonID + 1);
        seasonButton.onClick.AddListener(() => LoadLevelPanel(seasonID));
    }

    private void LoadLevelPanel(int seasonID)
    {
        PlayerPrefs.SetInt(DataKey.Cur_Season, seasonID);
        ChooseLevelPanel.Instance.Init(seasonID);
        EventDispatcher.Instance.PostEvent(EventID.On_Choose_Level);
    }
}
