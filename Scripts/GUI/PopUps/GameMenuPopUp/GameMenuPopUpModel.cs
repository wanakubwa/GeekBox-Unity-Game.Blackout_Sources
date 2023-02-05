using UnityEngine;
using System.Collections;

public class GameMenuPopUpModel : PopUpModel
{

    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void ShowMainMenuScene()
    {
        GameManager.Instance.SaveGame();
        GameManager.Instance.LoadMenuScene();
    }

    public bool IsSoundsOnForPlayer()
    {
        return true;
    }

    public void SetSoundSettings(bool isOn)
    {
        //todo;
    }

    public void SaveCurrentScenario()
    {
        GameManager.Instance.SaveGame();
    }

    public void RetryCurrentScenario()
    {
        ScenariosManager.Instance.RestartCurrentLvl();
    }

    #endregion

    #region Enums



    #endregion
}
