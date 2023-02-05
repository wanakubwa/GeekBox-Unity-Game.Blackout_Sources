using AISystem;
using System;
using TMPro;
using UnityEngine;

[Serializable]
public class AIParentBehaviorSettings : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI targetParentIdText;

    #endregion

    #region Propeties

    public TextMeshProUGUI TargetParentIdText { get => targetParentIdText; }

    private AIParentSettings CachedSettings {
        get;
        set;
    }

    #endregion

    #region Methods

    public void RefreshData(AIParentSettings currentSettings)
    {
        CachedSettings = currentSettings;
        TargetParentIdText.text = currentSettings.ParentId.ToString();
    }

    public void ShowEditorAISetupPopUp()
    {
        PopUpManager.Instance.ShowEditorAISetupPopUp(CachedSettings);
    }

    #endregion

    #region Enums



    #endregion
}
