using System;
using TechnologyTree.Attributes;
using UnityEngine;

[Serializable, UpgradeEffect]
public class ChangeSpeedPercentUpgradeEffect : UpgradeEffectBase
{
    #region Fields

    [SerializeField, EffectField]
    private float speedPercentValue = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public float SpeedPercentValue { 
        get => speedPercentValue; 
        private set => speedPercentValue = value; 
    }

    #endregion

    #region Methods

    public override void Apply(ParentValues targetValues)
    {
        base.Apply(targetValues);

        targetValues.ConnectionSpeedPerksValue.AddPercentValue(SpeedPercentValue);
    }

    public override float GetValue()
    {
        return SpeedPercentValue;
    }

    #endregion

    #region Enums



    #endregion
}
