using AISystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorAISetupView : ScenarioSetupBaseView<AIParentSettings>
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI parentIdText;

    [Title("Common Settings")]
    [SerializeField]
    private TMP_InputField refreshDelayMinS;
    [SerializeField]
    private TMP_InputField refreshDelayMaxS;
    [SerializeField]
    private TMP_InputField targetChargeOverflowPercent;

    [Title("Attack Settings")]
    [SerializeField]
    private TMP_InputField checkNeighbourNeutralAICondition;
    [SerializeField]
    private TMP_InputField checkNeighbourOccupatedAICondition;
    [SerializeField]
    private TMP_InputField checkNeighbourParentIsSmallerAICondition;
    [SerializeField]
    private Toggle checkNestedNodes;

    [Title("Deff Settings")]
    [SerializeField]
    private TMP_InputField checkNodeWithBiggestChargeAICondition;
    [SerializeField]
    private TMP_InputField checkNodeIncreaseAttackPotentialAICondition;

    #endregion

    #region Propeties

    public TextMeshProUGUI ParentIdText { get => parentIdText; }

    public TMP_InputField RefreshDelayMinS { get => refreshDelayMinS; }
    public TMP_InputField RefreshDelayMaxS { get => refreshDelayMaxS; }
    public TMP_InputField TargetChargeOverflowPercent { get => targetChargeOverflowPercent; }

    public TMP_InputField CheckNeighbourNeutralAICondition { get => checkNeighbourNeutralAICondition; }
    public TMP_InputField CheckNeighbourOccupatedAICondition { get => checkNeighbourOccupatedAICondition; }
    public TMP_InputField CheckNeighbourParentIsSmallerAICondition { get => checkNeighbourParentIsSmallerAICondition; }
    public Toggle CheckNestedNodes { get => checkNestedNodes; set => checkNestedNodes = value; }
    public TMP_InputField CheckNodeWithBiggestChargeAICondition { get => checkNodeWithBiggestChargeAICondition; }
    public TMP_InputField CheckNodeIncreaseAttackPotentialAICondition { get => checkNodeIncreaseAttackPotentialAICondition; }

    #endregion

    #region Methods

    public override void RefreshView(AIParentSettings target)
    {
        ParentIdText.text = target.ParentId.ToString();
        RefreshDelayMaxS.text = target.RefreshDelayMaxS.ToString();
        RefreshDelayMinS.text = target.RefreshDelayMinS.ToString();
        TargetChargeOverflowPercent.text = target.TargetChargeOverflowPercent.ToString();

        CheckNeighbourNeutralAICondition.text = target.CheckNeighbourNeutralAIConditionValue.ToString();
        CheckNeighbourOccupatedAICondition.text = target.CheckNeighbourOccupatedAIConditionValue.ToString();
        CheckNeighbourParentIsSmallerAICondition.text = target.CheckNeighbourParentIsSmallerAIConditionValue.ToString();
        CheckNodeWithBiggestChargeAICondition.text = target.CheckNodeWithBiggestChargeAIConditionValue.ToString();
        CheckNodeIncreaseAttackPotentialAICondition.text = target.CheckNodeIncreaseAttackPotentialAIConditionValue.ToString();
        CheckNestedNodes.isOn = target.CheckNestedNodes;
    }

    #endregion

    #region Enums



    #endregion
}
