using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using PlayerData;

public class MainMenuLvlSelectTopPanel : MainMenuTopPanelBase
{
    #region Fields

    private const string STARS_FORMAT_INFO = "{0}/{1}";

    [SerializeField]
    private TextMeshProUGUI starsInfo;

    #endregion

    #region Propeties

    public TextMeshProUGUI StarsInfo { 
        get => starsInfo; 
    }

    #endregion

    #region Methods

    protected override void RefreshPanel()
    {
        base.RefreshPanel();

        StarsInfo.SetText(string.Format(STARS_FORMAT_INFO, GetCurrentGainedStars(), GetMaxStarsAmmount()));
    }

    private int GetCurrentGainedStars()
    {
        int output = Constants.DEFAULT_VALUE;
        if(PlayerManager.Instance == null)
        {
            return output;
        }

        IEnumerable<PlayerLvlInfo> finishedLvls = PlayerManager.Instance.Wallet.FinishedLvls;
        foreach(PlayerLvlInfo lvlInfo in finishedLvls)
        {
            output += (int)lvlInfo.RewardStars;
        }

        return output;
    }

    private int GetMaxStarsAmmount()
    {
        return ScenariosContentSettings.Instance.ScenariosCollection.Count * ScenarioDataManager.MAX_STARS_REWARD;
    }

    #endregion

    #region Enums



    #endregion
}
