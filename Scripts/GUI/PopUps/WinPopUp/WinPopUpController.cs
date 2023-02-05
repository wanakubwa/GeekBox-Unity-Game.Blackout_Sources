using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WinPopUpModel), typeof(WinPopUpVIew))]
public class WinPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties

    private WinPopUpModel CurrentModel {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<WinPopUpModel>();
    }

    public void RefreshPopUp(float lvlTimeMs)
    {
        CurrentModel.RefreshModel(lvlTimeMs);
        GetView<WinPopUpVIew>().RefreshView();
    }

    public void ExitButtonClick()
    {
        CurrentModel.LoadMainMenuScene();
    }

    public void PlayButtonClick()
    {
        CurrentModel.LoadNextAvaibleScenario();
    }

    public void ShareButtonClick()
    {
        CurrentModel.ShareResult();
    }

    public void ShowRewardedAd()
    {
        CurrentModel.ShowRewardAdToTripleReward();
    }

    #endregion

    #region Enums



    #endregion
}
