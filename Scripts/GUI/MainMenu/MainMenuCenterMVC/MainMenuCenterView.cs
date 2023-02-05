using UnityEngine;
using TMPro;

public class MainMenuCenterView : UIView
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI currentVersionInfoText;

    #endregion

    #region Propeties

    public TextMeshProUGUI CurrentVersionInfoText {
        get => currentVersionInfoText;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentVersionInfoText.SetText(GetModel<MainMenuCenterModel>().GetCurrentAppVersionInfo());
    }

    #endregion

    #region Enums



    #endregion
}
