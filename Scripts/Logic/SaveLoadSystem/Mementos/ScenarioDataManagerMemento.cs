using AISystem;
using System;
using UnityEngine;

[Serializable]
public class ScenarioDataManagerMemento : MementoBase
{
    #region Fields

    [SerializeField]
    private ScenarioDataManager.RewardSettings oneStarRewardDataSave;
    [SerializeField]
    private ScenarioDataManager.RewardSettings twoStarRewardDataSave;
    [SerializeField]
    private ScenarioDataManager.RewardSettings threeStarRewardDataSave;
    [SerializeField]
    private float devTimeMsSave;

    #endregion

    #region Propeties

    public ScenarioDataManager.RewardSettings OneStarRewardDataSave { 
        get => oneStarRewardDataSave; 
        private set => oneStarRewardDataSave = value; 
    }

    public ScenarioDataManager.RewardSettings TwoStarRewardDataSave { 
        get => twoStarRewardDataSave; 
        private set => twoStarRewardDataSave = value; 
    }

    public ScenarioDataManager.RewardSettings ThreeStarRewardDataSave { 
        get => threeStarRewardDataSave; 
        private set => threeStarRewardDataSave = value; 
    }

    public float DevTimeMsSave { 
        get => devTimeMsSave; 
        private set => devTimeMsSave = value; 
    }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        ScenarioDataManager manager = sourceManager as ScenarioDataManager;

        OneStarRewardDataSave = new ScenarioDataManager.RewardSettings(manager.OneStarRewardData);
        TwoStarRewardDataSave = new ScenarioDataManager.RewardSettings(manager.TwoStarRewardData);
        ThreeStarRewardDataSave = new ScenarioDataManager.RewardSettings(manager.ThreeStarRewardData);
        DevTimeMsSave = manager.DevTimeMs;
    }

    #endregion

    #region Enums



    #endregion
}
