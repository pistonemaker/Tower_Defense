using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelPanel : Singleton<ChooseLevelPanel>
{
    [SerializeField] private Transform content;
    [SerializeField] private LevelButton levelButtonPrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private List<LevelButton> listLevelButton;

    public void Init(int seasonID)
    {
        var seasonData = ChooseLevelManager.Instance.gameData.listSeasonData[seasonID];
        closeButton.onClick.AddListener(ClosePanel);
        
        for (int i = 0; i < seasonData.listLevelData.Count; i++)
        {
            var levelButton = PoolingManager.Spawn(levelButtonPrefab, transform.position, Quaternion.identity);
            var pass = seasonData.listLevelData[i].isPass;
            levelButton.transform.SetParent(content);
            levelButton.transform.localScale = Vector3.one;
            levelButton.Init(i, pass);
            listLevelButton.Add(levelButton);
        }
    }

    private void ClosePanel()
    {
        for (int i = 0; i < listLevelButton.Count; i++)
        {
            PoolingManager.Despawn(listLevelButton[i].gameObject);
        }
        
        listLevelButton.Clear();
        EventDispatcher.Instance.PostEvent(EventID.On_Choose_Season);
    }
}
