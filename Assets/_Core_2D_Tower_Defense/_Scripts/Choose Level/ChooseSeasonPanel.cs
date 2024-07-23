using UnityEngine;

public class ChooseSeasonPanel : Singleton<ChooseSeasonPanel>
{
    [SerializeField] private Transform content;
    [SerializeField] private SeasonButton seasonButtonPrefab;

    public void Init()
    {
        int countSeason = ChooseLevelManager.Instance.gameData.listSeasonData.Count;

        for (int i = 0; i < countSeason; i++)
        { 
            var seasonButton = PoolingManager.Spawn(seasonButtonPrefab, transform.position, Quaternion.identity);
            seasonButton.transform.SetParent(content);
            seasonButton.transform.localScale = Vector3.one;
            seasonButton.Init(i);
        }
    }
}
