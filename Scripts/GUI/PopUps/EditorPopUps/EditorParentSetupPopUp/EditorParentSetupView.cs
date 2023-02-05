using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class EditorParentSetupView : ScenarioSetupBaseView<NodeParent>
{
    #region Fields

    [SerializeField]
    private TMP_InputField chargeNormalValueInput;
    [SerializeField]
    private TMP_InputField chargePercentValueInput;
    [SerializeField]
    private TMP_InputField shieldsNormalValueInput;
    [SerializeField]
    private TMP_InputField shieldsPercentValueInput;

    [SerializeField]
    private TMP_InputField chargeRegenMsNormalValueInput;
    [SerializeField]
    private TMP_InputField chargeRegenMsPercentValueInput;
    [SerializeField]
    private TMP_InputField shieldsRegenMsValueInput;
    [SerializeField]
    private TMP_InputField shieldsRegenMsPercentValueInput;

    [SerializeField]
    private TMP_Dropdown addModesDropdown;
    [SerializeField]
    private TMP_Dropdown removeModesDropdown;
    [SerializeField]
    private TextMeshProUGUI availableModesText;

    #endregion

    #region Propeties

    public TMP_InputField ChargeNormalValueInput
    {
        get => chargeNormalValueInput;
    }

    public TMP_InputField ChargePercentValueInput
    {
        get => chargePercentValueInput;
    }

    public TMP_InputField ShieldsNormalValueInput
    {
        get => shieldsNormalValueInput;
    }

    public TMP_InputField ShieldsPercentValueInput
    {
        get => shieldsPercentValueInput;
    }

    public TMP_InputField ChargeRegenMsNormalValueInput
    {
        get => chargeRegenMsNormalValueInput;
    }

    public TMP_InputField ChargeRegenMsPercentValueInput
    {
        get => chargeRegenMsPercentValueInput;
    }

    public TMP_InputField ShieldsRegenMsValueInput
    {
        get => shieldsRegenMsValueInput;
    }

    public TMP_InputField ShieldsRegenMsPercentValueInput
    {
        get => shieldsRegenMsPercentValueInput;
    }

    public TMP_Dropdown AddModesDropdown {
        get => addModesDropdown;
    }

    public TMP_Dropdown RemoveModesDropdown
    {
        get => removeModesDropdown;
    }
    public TextMeshProUGUI AvailableModesText { 
        get => availableModesText; 
    }

    #endregion

    #region Methods

    public override void RefreshView(NodeParent target)
    {
        ParentValues currentValues = target.ModifiersValues;

        ChargeNormalValueInput.SetTextWithoutNotify(currentValues.ChargeCapacityPerksValue.NormalValue.ToString());
        ChargePercentValueInput.SetTextWithoutNotify(currentValues.ChargeCapacityPerksValue.PercentValue.ToString());
        ChargeRegenMsNormalValueInput.SetTextWithoutNotify(currentValues.ChargeRegenMsPerksValue.NormalValue.ToString());
        ChargeRegenMsPercentValueInput.SetTextWithoutNotify(currentValues.ChargeRegenMsPerksValue.PercentValue.ToString());

        ShieldsNormalValueInput.SetTextWithoutNotify(currentValues.ShieldsCapacityPerksValue.NormalValue.ToString());
        ShieldsPercentValueInput.SetTextWithoutNotify(currentValues.ShieldsCapacityPerksValue.PercentValue.ToString());
        ShieldsRegenMsValueInput.SetTextWithoutNotify(currentValues.ShieldsRegenMsPerksValue.NormalValue.ToString());
        ShieldsRegenMsPercentValueInput.SetTextWithoutNotify(currentValues.ShieldsRegenMsPerksValue.PercentValue.ToString());

        RefreshAddModeDropdown();
        RefreshRemoveModeDropdown();
        RefreshAvailableNodeModes();
    }

    public string GetRemoveModeDropdownValue()
    {
        return RemoveModesDropdown.options[RemoveModesDropdown.value].text;
    }

    public string GetAddModeDropdownValue()
    {
        return AddModesDropdown.options[AddModesDropdown.value].text;
    }

    public void RefreshAvailableNodeModes()
    {
        NodeParent target = GetModel<EditorParentSetupModel>().Target;
        AvailableModesText.text = string.Empty;

        foreach (NodeModeType mode in target.ModifiersValues.AvailableModes)
        {
            AvailableModesText.text += $"{mode}; ";
        }
    }

    private void RefreshAddModeDropdown()
    {
        int[] parentsIds = ParentsManager.Instance.GetParentsIds();
        AddModesDropdown.ClearOptions();
        AddModesDropdown.AddOptions(Enum.GetNames(typeof(NodeModeType)).ToList());
    }

    private void RefreshRemoveModeDropdown()
    {
        int[] parentsIds = ParentsManager.Instance.GetParentsIds();
        RemoveModesDropdown.ClearOptions();
        RemoveModesDropdown.AddOptions(Enum.GetNames(typeof(NodeModeType)).ToList());
    }

    #endregion

    #region Enums



    #endregion
}
