using UnityEngine;
using UnityEngine.UI;

public class ScenarioDataSettingsBookmark : ScenarioEditorBookmarkBase
{
    #region Fields

    [SerializeField]
    private Button scenarioTimeSetupButton;

    #endregion

    #region Propeties

    public Button ScenarioTimeSetupButton { get => scenarioTimeSetupButton; }

    #endregion

    #region Methods

    public void ShowEditorScenarioTimeSetupPopUp()
    {
        PopUpManager.Instance.ShowEditorScenarioTimeSetupPopUp();
    }

    #endregion

    #region Enums



    #endregion
}