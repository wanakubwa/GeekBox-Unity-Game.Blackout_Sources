using UnityEngine;
using System.Collections;

public class GameMenuPopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private UISwitch soundSwitch;

    #endregion

    #region Propeties

    public UISwitch SoundSwitch { get => soundSwitch; }

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
        InitializeSoundSwitchState();
    }

    private void InitializeSoundSwitchState()
    {
        SoundSwitch.SetIsOnWithoutNotify(CurrentModel.IsSoundsOnForPlayer());
    }

    #endregion

    #region Enums



    #endregion
}
