using GeekBox.Scripts.ValuesSystem;
using System;
using UnityEngine;

[Serializable]
public class NodeParasiteModeEffect : NodeModeEffectBase
{
    #region Fields

    [SerializeField]
    private float productivityModifierPercent = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public float ProductivityModifierPercent { 
        get => productivityModifierPercent; 
        private set => productivityModifierPercent = value; 
    }

    #endregion

    #region Methods

    public NodeParasiteModeEffect()
    {
        DebugName = typeof(NodeParasiteModeEffect).Name;
    }

    public NodeParasiteModeEffect(NodeParasiteModeEffect source) : base(source) 
    {
        ProductivityModifierPercent = source.ProductivityModifierPercent;
    }

    public override NodeModeEffectBase Copy()
    {
        return new NodeParasiteModeEffect(this);
    }

    public override void Apply(MapNode target)
    {
        base.Apply(target);

        FloatValueModifier chargeRegenerationModifier = ModifiersUtils.GetFloatPercentModifier(ProductivityModifierPercent, Id);

        foreach (MapConnection connection in target.ConnectionsReference)
        {
            // Znaleziona noda jest innego parenta niz rodzic sendera oraz neutralny, wiec nalezy nalozyc efekt.
            MapNode oppositeNode = connection.GetNodeOppositeTo(target.NodeId);
            if (oppositeNode.ParentId != target.ParentId && oppositeNode.ParentId != Constants.NODE_NEUTRAL_PARENT_ID)
            {
                ModifiableFloatValue chargeRegerationFloatValue = GetFloatValueForNode(oppositeNode);
                chargeRegerationFloatValue.AddModsModifier(chargeRegenerationModifier);
            }
        }
    }

    public override void Revert(MapNode target)
    {
        base.Revert(target);

        foreach (MapConnection connection in target.ConnectionsReference)
        {
            // Znaleziona noda jest innego parenta niz rodzic sendera oraz neutralny, wiec nalezy usunac efekt.
            MapNode oppositeNode = connection.GetNodeOppositeTo(target.NodeId);
            if (oppositeNode.ParentId != target.ParentId && oppositeNode.ParentId != Constants.NODE_NEUTRAL_PARENT_ID)
            {
                ModifiableFloatValue chargeRegerationFloatValue = GetFloatValueForNode(oppositeNode);
                chargeRegerationFloatValue.RemoveModsModifier(Id);
            }
        }
    }

    private ModifiableFloatValue GetFloatValueForNode(MapNode target)
    {
        return target.Values.ValuesModifiers.ChargeRegenerationMsModifier;
    }

    #endregion

    #region Enums



    #endregion
}
