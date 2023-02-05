using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class SettingsPopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private UISwitch soundSwitch;
    [SerializeField]
    private TMP_Dropdown languageDropdown;

    #endregion

    #region Propeties

    public UISwitch SoundSwitch { get => soundSwitch; }
    public TMP_Dropdown LanguageDropdown { get => languageDropdown; }
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
        InitializeSoundSwitchState();
        InitializeLanguageDropdown();
    }

    private void InitializeSoundSwitchState()
    {
        SoundSwitch.SetIsOnWithoutNotify(CurrentModel.IsSoundsOnForPlayer());
    }

    private void InitializeLanguageDropdown()
    {
        string[] languages = Enum.GetNames(typeof(Language));
        LanguageDropdown.ClearOptions();
        LanguageDropdown.AddOptions(languages.ToList());
        LanguageDropdown.SetValueWithoutNotify(languages.IndexOf(LanguageManager.Instance.CurrentLanguage.ToString()));
    }

    #endregion

    #region Enums



    #endregion
}
