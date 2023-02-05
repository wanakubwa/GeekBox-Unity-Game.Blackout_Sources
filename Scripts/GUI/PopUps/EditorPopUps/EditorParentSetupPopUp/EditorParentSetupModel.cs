using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EditorParentSetupModel : ScenarioSetupBaseModel<NodeParent>
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public override void SaveTarget()
    {
        if (Target.IsPlayerOrNeutralParent() == false)
        {
            EditorParentSetupView currentView = GetView<EditorParentSetupView>();

            ParentValues currentValues = Target.ModifiersValues;

            currentValues.ChargeCapacityPerksValue.SetNormalValue(currentView.ChargeNormalValueInput.text.ParseToInt());
            currentValues.ChargeCapacityPerksValue.SetPercentValue(currentView.ChargePercentValueInput.text.ParseToInt());

            currentValues.ShieldsCapacityPerksValue.SetNormalValue(currentView.ShieldsNormalValueInput.text.ParseToInt());
            currentValues.ShieldsCapacityPerksValue.SetPercentValue(currentView.ShieldsPercentValueInput.text.ParseToInt());

            currentValues.ChargeRegenMsPerksValue.SetNormalValue(currentView.ChargeRegenMsNormalValueInput.text.ParseToFloat());
            currentValues.ChargeRegenMsPerksValue.SetPercentValue(currentView.ChargeRegenMsPercentValueInput.text.ParseToFloat());

            currentValues.ShieldsRegenMsPerksValue.SetNormalValue(currentView.ShieldsRegenMsValueInput.text.ParseToFloat());
            currentValues.ShieldsRegenMsPerksValue.SetPercentValue(currentView.ShieldsRegenMsPercentValueInput.text.ParseToFloat());
        }
    }

    public void RemoveMode()
    {
        string modeToRemove = GetView<EditorParentSetupView>().GetRemoveModeDropdownValue();
        NodeModeType selectedMode = (NodeModeType)Enum.Parse(typeof(NodeModeType), modeToRemove);

        Target.ModifiersValues.AvailableModes.Remove(selectedMode);
    }

    public void AddMode()
    {
        string modeToAdd = GetView<EditorParentSetupView>().GetAddModeDropdownValue();
        NodeModeType selectedMode = (NodeModeType)Enum.Parse(typeof(NodeModeType), modeToAdd);
        
        if(Target.ModifiersValues.AvailableModes.Contains(selectedMode) == false)
        {
            Target.ModifiersValues.AvailableModes.Add(selectedMode);
        }
    }

    #endregion

    #region Enums



    #endregion
}
