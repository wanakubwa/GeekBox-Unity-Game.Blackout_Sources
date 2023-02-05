using UnityEngine;
using System.Collections;
using System;

public class TopPanelUIModel : UIModel
{

    #region Fields



    #endregion

    #region Propeties

    private TimeManager Time
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        Time = TimeManager.Instance;
    }

    public string GetFormattedTime()
    {
        float millisecs = Time.CurrentMilisecondsCounter;
        return millisecs.ToTimeFormatt("mm:ss:fff");
    }

    public void ShowGameMenuPopUp()
    {
        PopUpManager.Instance.ShowGameMenuPopUp();
    }

    #endregion

    #region Enums



    #endregion
}
