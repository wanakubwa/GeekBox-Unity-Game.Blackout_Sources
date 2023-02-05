using System;
using TechnologyTree.Attributes;
using UnityEngine;

[Serializable, UpgradeEffect]
public class UnlockNodeModeUpgradeEffect : UpgradeEffectBase
{
    #region Fields

    [SerializeField, EffectField]
    private NodeModeType targetMode;

    #endregion

    #region Propeties

    public NodeModeType TargetMode { 
        get => targetMode; 
        set => targetMode = value; 
    }

    #endregion

    #region Methods

    public override void Apply(ParentValues targetValues)
    {
        base.Apply(targetValues);

        PlayerManager.Instance.Wallet.AddUnlockedMode(TargetMode);
    }

    public override string GetValueText()
    {
        return EnumLocalizationUtil.LocalizeNodeMode(TargetMode);
    }

    #endregion

    #region Enums



    #endregion
}
