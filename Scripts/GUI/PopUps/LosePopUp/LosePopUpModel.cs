using UnityEngine;
using System.Collections;

public class LosePopUpModel : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties

    public float LvlTime
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void RefreshModel(float timeMs)
    {
        LvlTime = timeMs;
    }

    public float GetDevTimeMsForCurrentLvl()
    {
        return ScenarioDataManager.Instance.DevTimeMs;
    }

    public float GetThresholdForStarReward(ScenarioDataManager.RewardType reward)
    {
        return ScenarioDataManager.Instance.GetThresholdForStarReward(reward);
    }

    public void LoadMainMenuScene()
    {
        GameManager.Instance.LoadMenuScene();
    }
    
    public void RestartCurrentLvl()
    {
        ScenariosManager.Instance.RestartCurrentLvl();
    }

    #endregion

    #region Enums



    #endregion
}
