using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MainMenuCenterModel), typeof(MainMenuCenterView))]
public class MainMenuCenterController : UIController
{
    #region Fields



    #endregion

    #region Propeties

    private MainMenuCenterModel CurrentModel
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<MainMenuCenterModel>();
    }

    public void PlayButtonClicked()
    {
        CurrentModel.SwitchScreenToLvlSelect();
    }

    public void UpgradeButtonClicked()
    {
        CurrentModel.SwitchScreenToUpgrades();
    }

    public void OptionsButtonClicked()
    {
        CurrentModel.ShowSettingsPopUp();
    }

    public void ShowLeaderboard()
    {
        GeekBox.Ads.EasyMobileManager.Instance.ShowLeaderboard();
    }

    public void ShowRatingDialog()
    {
        GeekBox.Ads.EasyMobileManager.Instance.ShowRatingDialog();
    }

    #endregion

    #region Enums



    #endregion
}
