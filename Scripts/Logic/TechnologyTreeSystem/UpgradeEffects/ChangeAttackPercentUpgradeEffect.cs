using System;
using TechnologyTree.Attributes;
using UnityEngine;

[Serializable, UpgradeEffect]
public class ChangeAttackPercentUpgradeEffect : UpgradeEffectBase
{
    #region Fields

    [SerializeField, EffectField]
    private float attackPercentValue = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public float AttackPercentValue { 
        get => attackPercentValue; 
        private set => attackPercentValue = value; 
    }

    #endregion

    #region Methods

    public override void Apply(ParentValues targetValues)
    {
        base.Apply(targetValues);

        targetValues.AttackMultiplierPerksValue.AddPercentValue(AttackPercentValue);
    }

    public override float GetValue()
    {
        return AttackPercentValue;
    }

    #endregion

    #region Enums



    #endregion
}
