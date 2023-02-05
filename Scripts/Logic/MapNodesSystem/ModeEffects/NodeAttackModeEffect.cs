using GeekBox.Scripts.ValuesSystem;
using System;

[Serializable]
public class NodeAttackModeEffect : NodeModeEffectBase
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public NodeAttackModeEffect()
    {
        DebugName = typeof(NodeAttackModeEffect).Name;
    }

    public NodeAttackModeEffect(NodeAttackModeEffect source) : base(source) { }

    public override NodeModeEffectBase Copy()
    {
        return new NodeAttackModeEffect(this);
    }

    public override void Apply(MapNode target)
    {
        base.Apply(target);

        target.Values.ValuesModifiers.AttackChargeValueModifier.AddModsModifier(ModifiersUtils.GetFloatPercentModifier(PercentFactor, Id));
    }

    public override void Revert(MapNode target)
    {
        base.Revert(target);

        target.Values.ValuesModifiers.AttackChargeValueModifier.RemoveModsModifier(Id);
    }

    #endregion

    #region Enums



    #endregion
}
