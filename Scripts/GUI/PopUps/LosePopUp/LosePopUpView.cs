using UnityEngine;
using System.Collections;
using TMPro;

public class LosePopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI mainTimeText;
    [SerializeField]
    private TextMeshProUGUI devTimeText;

    [Space]
    [SerializeField]
    private StartElementInfo firstStarElement;
    [SerializeField]
    private StartElementInfo secondStarElement;
    [SerializeField]
    private StartElementInfo thirdStarElement;

    #endregion

    #region Propeties

    private TextMeshProUGUI MainTimeText { get => mainTimeText; }
    private TextMeshProUGUI DevTimeText { get => devTimeText; }
    private StartElementInfo FirstStarElement { get => firstStarElement; }
    private StartElementInfo SecondStarElement { get => secondStarElement; }
    private StartElementInfo ThirdStarElement { get => thirdStarElement; }

    // Variables.
    private LosePopUpModel CurrentModel
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<LosePopUpModel>();
    }

    public void RefreshView()
    {
        SetMainTime(CurrentModel.LvlTime);
        SetDevTime(CurrentModel.GetDevTimeMsForCurrentLvl());

        RefreshRewardStars();
    }

    private void SetMainTime(float valueMs)
    {
        MainTimeText.text = valueMs.ToTimeFormatt("mm:ss");
    }

    private void SetDevTime(float valueMs)
    {
        DevTimeText.text = valueMs.ToTimeFormatt("mm:ss");
    }

    private void RefreshRewardStars()
    {
        // To jest okropne. !!!!!!!
        float oneStarThreshold = CurrentModel.GetThresholdForStarReward(ScenarioDataManager.RewardType.ONE_STAR);
        float twoStarThreshold = CurrentModel.GetThresholdForStarReward(ScenarioDataManager.RewardType.TWO_STARS);
        float threeStarThreshold = CurrentModel.GetThresholdForStarReward(ScenarioDataManager.RewardType.THREE_STARS);

        FirstStarElement.SetInfo(oneStarThreshold, false);
        SecondStarElement.SetInfo(twoStarThreshold, false);
        ThirdStarElement.SetInfo(threeStarThreshold, false);
    }

    #endregion

    #region Enums



    #endregion
}
