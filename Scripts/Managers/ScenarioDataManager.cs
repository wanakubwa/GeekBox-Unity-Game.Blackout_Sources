using BOBCheats.Utils;
using PointsSystem.RewardKPoints;
using System;
using UnityEngine;

public class ScenarioDataManager : SingletonScenarioSaveableManager<ScenarioDataManager, ScenarioDataManagerMemento>
{
    #region Fields

    public const int MAX_STARS_REWARD = 3;
    public const float LVL_PASSED_MULTIPLIER = 0.5f;

    [SerializeField]
    private RewardSettings oneStarRewardData = new RewardSettings(RewardType.ONE_STAR, Constants.DEFAULT_VALUE);
    [SerializeField]
    private RewardSettings twoStarRewardData = new RewardSettings(RewardType.TWO_STARS, Constants.DEFAULT_VALUE);
    [SerializeField]
    private RewardSettings threeStarRewardData = new RewardSettings(RewardType.THREE_STARS, Constants.DEFAULT_VALUE);

    [SerializeField, ReadOnly]
    private float devTimeMs = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public RewardSettings OneStarRewardData { 
        get => oneStarRewardData; 
        private set => oneStarRewardData = value; 
    }

    public RewardSettings TwoStarRewardData { 
        get => twoStarRewardData; 
        private set => twoStarRewardData = value; 
    }

    public RewardSettings ThreeStarRewardData { 
        get => threeStarRewardData; 
        private set => threeStarRewardData = value; 
    }

    public float DevTimeMs { 
        get => devTimeMs; 
        private set => devTimeMs = value; 
    }

    private RewardController RewardPointsController {
        get;
        set;
    } = new RewardController();

    #endregion

    #region Methods

    public void SetDevTime(float timeMs)
    {
        DevTimeMs = timeMs;
    }

    public RewardType GetRewardForTime(float timeMs)
    {
        if(timeMs > OneStarRewardData.TimeThresholdMs)
        {
            return RewardType.ZERO_STARS;
        }

        if(timeMs > TwoStarRewardData.TimeThresholdMs)
        {
            return RewardType.ONE_STAR;
        }

        if(timeMs > ThreeStarRewardData.TimeThresholdMs)
        {
            return RewardType.TWO_STARS;
        }

        return RewardType.THREE_STARS;
    }

    public int GetKPointsRewardForTime(float timeMs)
    {
        RewardType rewardLvl = GetRewardForTime(timeMs);
        int kPointsReward = RewardPointsController.GetRewardForCurrentLvl(rewardLvl);

        int currentLvlId = ScenariosManager.Instance.CurrentScenarioInfo.ScenarioId;
        PlayerData.PlayerLvlInfo lvlInfo = PlayerManager.Instance.Wallet.FinishedLvls.GetElementByID(currentLvlId);
        if (lvlInfo != null && lvlInfo.RewardStars <= rewardLvl)
        {
            // Gracz wczesniej ukonczyl ten poziom i uzyskal ponownie nagrode taka sama lub nizej.
            kPointsReward = (int)(kPointsReward * LVL_PASSED_MULTIPLIER);
        }

        return kPointsReward;
    }

    public float GetThresholdForStarReward(RewardType reward)
    {
        switch (reward)
        {
            case ScenarioDataManager.RewardType.ONE_STAR:
                return OneStarRewardData.TimeThresholdMs;
            case ScenarioDataManager.RewardType.TWO_STARS:
                return TwoStarRewardData.TimeThresholdMs;
            case ScenarioDataManager.RewardType.THREE_STARS:
                return ThreeStarRewardData.TimeThresholdMs;
        }

        return Constants.DEFAULT_VALUE;
    }

    public override void CreateNewScenario()
    {
        OneStarRewardData = new RewardSettings(RewardType.ONE_STAR, Constants.DEFAULT_VALUE);
        TwoStarRewardData = new RewardSettings(RewardType.TWO_STARS, Constants.DEFAULT_VALUE);
        ThreeStarRewardData = new RewardSettings(RewardType.THREE_STARS, Constants.DEFAULT_VALUE);

        DevTimeMs = Constants.DEFAULT_VALUE;
    }

    public override void LoadManager(ScenarioDataManagerMemento memento)
    {
        OneStarRewardData = new RewardSettings(memento.OneStarRewardDataSave);
        TwoStarRewardData = new RewardSettings(memento.TwoStarRewardDataSave);
        ThreeStarRewardData = new RewardSettings(memento.ThreeStarRewardDataSave);
        DevTimeMs = memento.DevTimeMsSave;
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        GameEventsManager.Instance.OnPlayerWinScenario += HandlePlayerWinScenario;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        GameEventsManager.Instance.OnPlayerWinScenario -= HandlePlayerWinScenario;
    }


    private void HandlePlayerWinScenario(float timeMs)
    {
        int kPointsReward = GetKPointsRewardForTime(timeMs);
        PlayerManager.Instance.Wallet.AddKPoints(kPointsReward);
    }

    #endregion

    #region Enums

    [Serializable]
    public class RewardSettings
    {
        #region Fields

        [SerializeField, ReadOnly]
        RewardType targetReward;
        [SerializeField, ReadOnly]
        private float timeThresholdMs;

        #endregion

        #region Propeties

        public RewardType TargetReward
        {
            get => targetReward;
            private set => targetReward = value;
        }

        public float TimeThresholdMs
        {
            get => timeThresholdMs;
            private set => timeThresholdMs = value;
        }

        #endregion

        #region Methods

        public RewardSettings() { }

        public RewardSettings(RewardType targetReward, float timeThresholdMs)
        {
            TargetReward = targetReward;
            TimeThresholdMs = timeThresholdMs;
        }

        public RewardSettings(RewardSettings source)
        {
            TargetReward = source.TargetReward;
            TimeThresholdMs = source.TimeThresholdMs;
        }

        public void SetTimeThreshold(float timeMs)
        {
            TimeThresholdMs = timeMs;
        }

        #endregion

        #region Enums



        #endregion
    }

    public enum RewardType
    {
        ZERO_STARS = 0,
        ONE_STAR = 1,
        TWO_STARS = 2,
        THREE_STARS = 3
    }

    #endregion
}
