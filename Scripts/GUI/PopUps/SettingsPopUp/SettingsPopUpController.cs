using UnityEngine;
using System.Collections;
using GeekBox.Ads;

[RequireComponent(typeof(SettingsPopUpView), typeof(SettingsPopUpModel))]
public class SettingsPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties

    private SettingsPopUpModel CurrentModel
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<SettingsPopUpModel>();
    }

    public void OnDropdownChanged(int index)
    {
        CurrentModel.ChangeLanguage(index);
    }

    public void ResetGame()
    {
        GetModel<SettingsPopUpModel>().ResetGamePlayerProgress();
    }

    public void OnSoundSwitchStateChanged(bool isOn)
    {
        CurrentModel.SetSoundSettings(isOn);
    }

    public void ShowCredits()
    {
        PopUpManager.Instance.ShowCreditsPopUp();
    }

    public void ShowGdprDialog()
    {
        EasyMobileManager.Instance.ShowGdprDialog(true);
    }

    #endregion

    #region Enums



    #endregion
}
