using GeekBox.Scripts.ValuesSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class ParentValues
{
    #region Fields

    [SerializeField]
    private SplittedIntValue chargeCapacityPerksValue = new SplittedIntValue();
    [SerializeField]
    private SplittedFloatValue chargeRegenMsPerksValue = new SplittedFloatValue();
    [SerializeField]
    private SplittedIntValue shieldsCapacityPerksValue = new SplittedIntValue();
    [SerializeField]
    private SplittedFloatValue shieldsRegenMsPerksValue = new SplittedFloatValue();

    [SerializeField]
    private SplittedFloatValue connectionSpeedPerksValue = new SplittedFloatValue();
    [SerializeField]
    private SplittedFloatValue attackMultiplierPerksValue = new SplittedFloatValue();

    [SerializeField]
    private List<NodeModeType> availableModes = new List<NodeModeType>();

    #endregion

    #region Propeties

    public SplittedIntValue ChargeCapacityPerksValue { 
        get => chargeCapacityPerksValue; 
        private set => chargeCapacityPerksValue = value; 
    }

    public SplittedFloatValue ChargeRegenMsPerksValue { 
        get => chargeRegenMsPerksValue; 
        private set => chargeRegenMsPerksValue = value; 
    }

    public SplittedIntValue ShieldsCapacityPerksValue { 
        get => shieldsCapacityPerksValue; 
        private set => shieldsCapacityPerksValue = value; 
    }

    public SplittedFloatValue ShieldsRegenMsPerksValue { 
        get => shieldsRegenMsPerksValue; 
        private set => shieldsRegenMsPerksValue = value; 
    }

    public SplittedFloatValue ConnectionSpeedPerksValue { 
        get => connectionSpeedPerksValue; 
        private set => connectionSpeedPerksValue = value; 
    }

    public SplittedFloatValue AttackMultiplierPerksValue { 
        get => attackMultiplierPerksValue; 
        private set => attackMultiplierPerksValue = value; 
    }

    public List<NodeModeType> AvailableModes { 
        get => availableModes; 
        private set => availableModes = value; 
    }

    #endregion

    #region Methods

    public void ApplyModifiers(MapNode target, int parentId)
    {
        target.Values.ValuesModifiers.ChargeLimitModifier.AddPerksModifier(ModifiersUtils.GetIntNormalModifier(ChargeCapacityPerksValue.NormalValue, parentId));
        target.Values.ValuesModifiers.ChargeLimitModifier.AddPerksModifier(ModifiersUtils.GetIntPercentModifier(ChargeCapacityPerksValue.PercentValue, parentId));

        target.Values.ValuesModifiers.ShieldsLimitModifier.AddPerksModifier(ModifiersUtils.GetIntNormalModifier(ShieldsCapacityPerksValue.NormalValue, parentId));
        target.Values.ValuesModifiers.ShieldsLimitModifier.AddPerksModifier(ModifiersUtils.GetIntPercentModifier(ShieldsCapacityPerksValue.PercentValue, parentId));

        target.Values.ValuesModifiers.ChargeRegenerationMsModifier.AddPerksModifier(ModifiersUtils.GetFloatNormalModifier(ChargeRegenMsPerksValue.NormalValue, parentId));
        target.Values.ValuesModifiers.ChargeRegenerationMsModifier.AddPerksModifier(ModifiersUtils.GetFloatPercentModifier(ChargeRegenMsPerksValue.PercentValue, parentId));

        target.Values.ValuesModifiers.ShieldsRegenerationMsModifier.AddPerksModifier(ModifiersUtils.GetFloatNormalModifier(ShieldsRegenMsPerksValue.NormalValue, parentId));
        target.Values.ValuesModifiers.ShieldsRegenerationMsModifier.AddPerksModifier(ModifiersUtils.GetFloatPercentModifier(ShieldsRegenMsPerksValue.PercentValue, parentId));

        target.Values.ValuesModifiers.AttackChargeValueModifier.AddPerksModifier(ModifiersUtils.GetFloatNormalModifier(AttackMultiplierPerksValue.NormalValue, parentId));
        target.Values.ValuesModifiers.AttackChargeValueModifier.AddPerksModifier(ModifiersUtils.GetFloatPercentModifier(AttackMultiplierPerksValue.PercentValue, parentId));
    }

    public void RemoveModifiers(MapNode target, int parentId)
    {
        target.Values.ValuesModifiers.ChargeLimitModifier.RemoveAllPerksModifiers(parentId);
        target.Values.ValuesModifiers.ShieldsLimitModifier.RemoveAllPerksModifiers(parentId);
        target.Values.ValuesModifiers.ChargeRegenerationMsModifier.RemoveAllPerksModifiers(parentId);
        target.Values.ValuesModifiers.ShieldsRegenerationMsModifier.RemoveAllPerksModifiers(parentId);
        target.Values.ValuesModifiers.AttackChargeValueModifier.RemoveAllPerksModifiers(parentId);
    }

    #endregion

    #region Enums


    #endregion
}
