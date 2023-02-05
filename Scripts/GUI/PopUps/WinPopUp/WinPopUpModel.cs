using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GeekBox.Utils;
using GeekBox.Ads;

public class WinPopUpModel : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties

    public float LvlTime
    {
        get;
        private set;
    }

    private bool WasADShown
    {
        get;
        set;
    } = false;


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

    public int GetKPointsReward()
    {
        return ScenarioDataManager.Instance.GetKPointsRewardForTime(LvlTime);
    }

    public ScenarioDataManager.RewardType GetRewardType()
    {
        return ScenarioDataManager.Instance.GetRewardForTime(LvlTime);
    }

    public float GetThresholdForStarReward(ScenarioDataManager.RewardType reward)
    {
        return ScenarioDataManager.Instance.GetThresholdForStarReward(reward);
    }

    public void LoadMainMenuScene()
    {
        GameManager.Instance.LoadMenuScene();
    }

    public void LoadNextAvaibleScenario()
    {
        // todo; zastanowic sie czy nie bedzie systemu ulockowania poziomow.
        ScenarioInfo nextScenarioInfo = ScenariosManager.Instance.GetNextScenarioInfo();
        
        if(nextScenarioInfo != null)
        {
            GameManager.Instance.LoadGameScene(nextScenarioInfo.ScenarioDirectoryName);
        }
        else
        {
            LoadMainMenuScene();
            // todo; wyswietlic info popa ze koniec poziomow i przeniesienie do menu.
        }
    }

    public void ShareResult()
    {
        ScenarioInfo currentInfo = ScenariosManager.Instance.CurrentScenarioInfo;
        string bodyInfo = string.Format(Constants.LOC_SHARE_INFO.Localize(), currentInfo.ScenarioNameKey.Localize(), LvlTime.ToTimeFormatt("mm:ss"));
        AndroidUtils.Share("Share your progress.", "Share your progress.", bodyInfo);
    }

    public void ShowRewardAdToTripleReward()
    {
        if(WasADShown == false)
        {
            EasyMobileManager.Instance.ShowRewardedAD(() =>
            {
                WasADShown = true;
                int reward = GetKPointsReward();
                PlayerManager.Instance.Wallet.AddKPoints(reward*2);
            });
        }   
    }

    #endregion

    #region Enums



    #endregion
}
