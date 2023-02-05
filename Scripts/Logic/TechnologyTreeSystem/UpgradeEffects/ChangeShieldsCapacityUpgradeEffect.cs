using System;
using TechnologyTree.Attributes;
using UnityEngine;

[Serializable, UpgradeEffect]
public class ChangeShieldsCapacityUpgradeEffect : UpgradeEffectBase
{
    #region Fields

    [SerializeField, EffectField]
    private int capacityValue = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public int CapacityValue { 
        get => capacityValue; 
        private set => capacityValue = value; 
    }

    #endregion

    #region Methods

    public override void Apply(ParentValues targetValues)
    {
        base.Apply(targetValues);

        targetValues.ShieldsCapacityPerksValue.AddNormalValue(CapacityValue);
    }

    public override float GetValue()
    {
        return CapacityValue;
    }

    #endregion

    #region Enums



    #endregion
}
