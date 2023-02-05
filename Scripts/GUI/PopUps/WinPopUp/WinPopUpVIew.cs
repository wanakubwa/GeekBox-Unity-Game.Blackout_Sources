using UnityEngine;
using System.Collections;
using TMPro;

public class WinPopUpVIew : PopUpView
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI kPointsValueText;
    [SerializeField]
    private TextMeshProUGUI mainTimeText;
    [SerializeField]
    private TextMeshProUGUI devTimeText;
    [SerializeField]
    private ButtonBase nextLvlButton;
    [SerializeField]
    private TextMeshProUGUI nextLvlDelayTimeText;

    [Space]
    [SerializeField]
    private StartElementInfo firstStarElement;
    [SerializeField]
    private StartElementInfo secondStarElement;
    [SerializeField]
    private StartElementInfo thirdStarElement;

    [Space]
    [SerializeField]
    private float nextLvlWaitS = 5f;

    #endregion

    #region Propeties

    private TextMeshProUGUI KPointsValueText { get => kPointsValueText; }
    private TextMeshProUGUI MainTimeText { get => mainTimeText; }
    private TextMeshProUGUI DevTimeText { get => devTimeText; }
    private StartElementInfo FirstStarElement { get => firstStarElement; }
    private StartElementInfo SecondStarElement { get => secondStarElement; }
    private StartElementInfo ThirdStarElement { get => thirdStarElement; }
    private float NextLvlWaitS { get => nextLvlWaitS; }
    private ButtonBase NextLvlButton { get => nextLvlButton; }
    public TextMeshProUGUI NextLvlDelayTimeText { get => nextLvlDelayTimeText; }

    // Variables.
    private WinPopUpModel CurrentModel
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<WinPopUpModel>();
        NextLvlButton.SetInteractable(false);
    }

    public void RefreshView()
    {
        SetMainTime(CurrentModel.LvlTime);
        SetDevTime(CurrentModel.GetDevTimeMsForCurrentLvl());
        SetKPoints(CurrentModel.GetKPointsReward());

        RefreshRewardStars();
    }

    private void Start()
    {
        StartCoroutine(_WaitAndEnableNextLvlButton(NextLvlWaitS));
    }

    private void SetMainTime(float valueMs)
    {
        MainTimeText.text = valueMs.ToTimeFormatt("mm:ss");
    }

    private void SetDevTime(float valueMs)
    {
        DevTimeText.text = valueMs.ToTimeFormatt("mm:ss");
    }

    private void SetKPoints(int value)
    {
        KPointsValueText.text = value.ToString();
    }

    private void RefreshRewardStars()
    {
        // To jest okropne. !!!!!!!
        float oneStarThreshold = CurrentModel.GetThresholdForStarReward(ScenarioDataManager.RewardType.ONE_STAR);
        float twoStarThreshold = CurrentModel.GetThresholdForStarReward(ScenarioDataManager.RewardType.TWO_STARS);
        float threeStarThreshold = CurrentModel.GetThresholdForStarReward(ScenarioDataManager.RewardType.THREE_STARS);

        ScenarioDataManager.RewardType rewardType = CurrentModel.GetRewardType();
        switch (rewardType)
        {
            case ScenarioDataManager.RewardType.ZERO_STARS:
                FirstStarElement.SetInfo(oneStarThreshold, false);
                SecondStarElement.SetInfo(twoStarThreshold, false);
                ThirdStarElement.SetInfo(threeStarThreshold, false);
                break;
            case ScenarioDataManager.RewardType.ONE_STAR:
                FirstStarElement.SetInfo(oneStarThreshold, true);
                SecondStarElement.SetInfo(twoStarThreshold, false);
                ThirdStarElement.SetInfo(threeStarThreshold, false);
                break;
            case ScenarioDataManager.RewardType.TWO_STARS:
                FirstStarElement.SetInfo(oneStarThreshold, true);
                SecondStarElement.SetInfo(twoStarThreshold, true);
                ThirdStarElement.SetInfo(threeStarThreshold, false);
                break;
            case ScenarioDataManager.RewardType.THREE_STARS:
                FirstStarElement.SetInfo(oneStarThreshold, true);
                SecondStarElement.SetInfo(twoStarThreshold, true);
                ThirdStarElement.SetInfo(threeStarThreshold, true);
                break;
        }
    }

    private IEnumerator _WaitAndEnableNextLvlButton(float waitTimeS)
    {
        NextLvlDelayTimeText.gameObject.SetActive(true);

        float counter = Constants.DEFAULT_VALUE;
        while (true)
        {
            if(counter < waitTimeS)
            {
                counter += Time.deltaTime;
                NextLvlDelayTimeText.SetText(((int)(waitTimeS - counter + 1f)).ToString());
                yield return null;
                continue;
            }

            //todo;
            NextLvlButton.SetInteractable(true);
            NextLvlDelayTimeText.gameObject.SetActive(false);
            break;
        }
    }

    #endregion

    #region Enums



    #endregion
}
