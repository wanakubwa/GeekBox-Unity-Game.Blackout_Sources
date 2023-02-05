using UnityEngine;
using System.Collections;
using UnityEngine.Video;

[RequireComponent(typeof(GameMenuPopUpModel), typeof(GameMenuPopUpView))]
public class GameMenuPopUpController : PopUpController
{

    #region Fields



    #endregion

    #region Propeties

    private GameMenuPopUpModel CurrentModel
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<GameMenuPopUpModel>();
    }

    public void OpenMainMenuScene()
    {
        CurrentModel.ShowMainMenuScene();
    }

    public void OnSoundSwitchStateChanged(bool isOn)
    {
        CurrentModel.SetSoundSettings(isOn);
    }

    public void SaveCurrentScenario()
    {
        CurrentModel.SaveCurrentScenario();
    }

    public void RetryCurrentScenario()
    {
        CurrentModel.RetryCurrentScenario();
    }

    #endregion

    #region Enums



    #endregion
}
