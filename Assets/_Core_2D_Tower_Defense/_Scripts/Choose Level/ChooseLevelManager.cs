public class ChooseLevelManager : Singleton<ChooseLevelManager>
{
    public ChooseSeasonPanel chooseSeasonPanel;
    public ChooseLevelPanel chooseLevelPanel;
    public GameData gameData;

    private void OnEnable()
    {
        AudioManager.Instance.PlayMusic("Choose_Level", 0.5f);
        chooseSeasonPanel.Init();
        chooseSeasonPanel.gameObject.SetActive(true);
        chooseLevelPanel.gameObject.SetActive(false);
        EventDispatcher.Instance.RegisterListener(EventID.On_Choose_Season, OnChooseSeason);
        EventDispatcher.Instance.RegisterListener(EventID.On_Choose_Level, OnChooseLevel);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.On_Choose_Season, OnChooseSeason);
        EventDispatcher.Instance.RemoveListener(EventID.On_Choose_Level, OnChooseLevel);
    }

    private void OnChooseLevel(object param)
    {
        chooseSeasonPanel.gameObject.SetActive(false);
        chooseLevelPanel.gameObject.SetActive(true);
    }

    private void OnChooseSeason(object param)
    {
        chooseSeasonPanel.gameObject.SetActive(true);
        chooseLevelPanel.gameObject.SetActive(false);
    }
}
