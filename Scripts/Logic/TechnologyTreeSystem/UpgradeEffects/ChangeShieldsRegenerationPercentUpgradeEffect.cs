using System;
using TechnologyTree.Attributes;
using UnityEngine;

[Serializable, UpgradeEffect]
public class ChangeShieldsRegenerationPercentUpgradeEffect : UpgradeEffectBase
{
    #region Fields

    [SerializeField, EffectField]
    private float percentFactor = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public float PercentFactor { 
        get => percentFactor; 
        private set => percentFactor = value; 
    }

    #endregion

    #region Methods

    public override void Apply(ParentValues targetValues)
    {
        base.Apply(targetValues);

        targetValues.ShieldsRegenMsPerksValue.AddPercentValue(-PercentFactor);
    }

    public override float GetValue()
    {
        return PercentFactor;
    }

    #endregion

    #region Enums



    #endregion
}
