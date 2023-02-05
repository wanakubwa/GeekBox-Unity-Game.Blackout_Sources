using AISystem;

public class EditorAISetupModel : ScenarioSetupBaseModel<AIParentSettings>
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public override void SaveTarget()
    {
        EditorAISetupView currentView = GetView<EditorAISetupView>();

        Target.SetMinDelayS(currentView.RefreshDelayMinS.text.ParseToFloat());
        Target.SetMaxDelayS(currentView.RefreshDelayMaxS.text.ParseToFloat());
        Target.SetTargetChargeOverflowPercent(currentView.TargetChargeOverflowPercent.text.ParseToFloat());

        Target.SetCheckNeighbourNeutralAIConditionValue(currentView.CheckNeighbourNeutralAICondition.text.ParseToInt());
        Target.SetCheckNeighbourOccupatedAIConditionValue(currentView.CheckNeighbourOccupatedAICondition.text.ParseToInt());
        Target.SetCheckNeighbourParentIsSmallerAIConditionValue(currentView.CheckNeighbourParentIsSmallerAICondition.text.ParseToInt());
        Target.SetCheckNodeWithBiggestChargeAIConditionValue(currentView.CheckNodeWithBiggestChargeAICondition.text.ParseToInt());
        Target.SetCheckNodeIncreaseAttackPotentialAIConditionValue(currentView.CheckNodeIncreaseAttackPotentialAICondition.text.ParseToInt());
        Target.SetCheckNextedNodes(currentView.CheckNestedNodes.isOn);
    }

    #endregion

    #region Enums



    #endregion
}
