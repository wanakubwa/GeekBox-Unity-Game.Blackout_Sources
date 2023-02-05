using TMPro;
using UnityEngine;

public class EditorScenarioTimeSetupView : ScenarioSetupBaseView<ScenarioDataManager>
{
    #region Fields

    [SerializeField]
    private TMP_InputField oneStarRewardThresholdMs;
    [SerializeField]
    private TMP_InputField twoStarRewardThresholdMs;
    [SerializeField]
    private TMP_InputField threeStarRewardThresholdMs;
    [SerializeField]
    private TMP_InputField devTimeMs;

    #endregion

    #region Propeties

    public TMP_InputField OneStarRewardThresholdMs { get => oneStarRewardThresholdMs; }
    public TMP_InputField TwoStarRewardThresholdMs { get => twoStarRewardThresholdMs; }
    public TMP_InputField ThreeStarRewardThresholdMs { get => threeStarRewardThresholdMs; }
    public TMP_InputField DevTimeMs { get => devTimeMs; }

    #endregion

    #region Methods

    public override void RefreshView(ScenarioDataManager target)
    {
        OneStarRewardThresholdMs.text = target.OneStarRewardData.TimeThresholdMs.ToString();
        TwoStarRewardThresholdMs.text = target.TwoStarRewardData.TimeThresholdMs.ToString();
        ThreeStarRewardThresholdMs.text = target.ThreeStarRewardData.TimeThresholdMs.ToString();
        DevTimeMs.text = target.DevTimeMs.ToString();
    }

    #endregion

    #region Enums



    #endregion
}
