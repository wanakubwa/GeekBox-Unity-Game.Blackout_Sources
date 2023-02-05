using GeekBox.Scripts.ValuesSystem;
using System;
using UnityEngine;

[Serializable]
public class NodeValues
{
    #region Fields

    [SerializeField]
    private NodeModeType profileModeType = NodeModeType.DEFAULT;
    [SerializeField]
    private int chargeValue = Constants.DEFAULT_VALUE;
    [SerializeField]
    private int currentShields = Constants.DEFAULT_VALUE;
    [SerializeField]
    private float chargeTimeCounter = Constants.DEFAULT_VALUE;
    [SerializeField]
    private float shieldsTimeCounter = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public event Action<int> OnValueChanged = delegate { };

    private Action<int> OnChargeValueChanged { get; set; } = delegate { };
    private Action<int> OnShieldsChanged { get; set; } = delegate { };

    public NodeModeType ProfileModeType { 
        get => profileModeType; 
        private set => profileModeType = value; 
    }

    public NodeModeValues CurrentModeValues
    {
        get;
        private set;
    }

    public int ChargeValue { 
        get => chargeValue; 
        private set => chargeValue = value; 
    }

    public float ChargeTimeCounter { 
        get => chargeTimeCounter; 
        private set => chargeTimeCounter = value; 
    }

    public int CurrentShields { 
        get => currentShields; 
        private set => currentShields = value; 
    }

    public float ShieldsTimeCounter { 
        get => shieldsTimeCounter; 
        private set => shieldsTimeCounter = value; 
    }

    public Modifiers ValuesModifiers {
        get;
        private set;
    } = new Modifiers();

    #endregion

    #region Methods

    public NodeValues()
    {

    }

    public NodeValues(NodeValues source)
    {
        ProfileModeType = source.ProfileModeType;
        ChargeValue = source.ChargeValue;
        ChargeTimeCounter = source.ChargeTimeCounter;
        ShieldsTimeCounter = source.ShieldsTimeCounter;
        CurrentShields = source.CurrentShields;
    }

    public int GetTotalValue()
    {
        return CurrentShields + ChargeValue;
    }

    public int GetChargePercent(float chargeFactorNormalized)
    {
        int chargeToSend = Mathf.FloorToInt(ChargeValue * chargeFactorNormalized);
        return chargeToSend;
    }

    public void SetListeners(Action<int> onChargeChangedCallback, Action<int> onShieldsChangeCallback)
    {
        OnChargeValueChanged = onChargeChangedCallback;
        OnShieldsChanged = onShieldsChangeCallback;
    }

    public void SetNodeMode(NodeModeType modeType, NodeModeValues modeValues)
    {
        CurrentModeValues = modeValues;
        ProfileModeType = modeType;
        RefreshLimits();
    }

    public void RefreshValues(float deltaTimeMs)
    {
        RefreshChargeValue(deltaTimeMs);
        RefreshShieldsValue(deltaTimeMs);
    }

    public int GetChargeMaxValue()
    {
        return ValuesModifiers.ChargeLimitModifier.FinalValue + CurrentModeValues.ChargeLimit;
    }

    public int GetShieldsMaxValue()
    {
        return ValuesModifiers.ShieldsLimitModifier.FinalValue + CurrentModeValues.ShieldsLimit;
    }

    public void SetShieldsValue(int value)
    {
        // todo; przeniesc wszystko do modyfikatorow.
        int limit = GetShieldsMaxValue();
        value = value > limit ? limit : value;
        value = value < 0 ? Constants.DEFAULT_VALUE : value;

        CurrentShields = value;
        OnShieldsChanged(CurrentShields);
    }

    public void SetChargeValue(int value)
    {
        int chargeLimit = GetChargeMaxValue();
        value = value > chargeLimit ? chargeLimit : value;
        value = value < 0 ? Constants.DEFAULT_VALUE : value;

        ChargeValue = value;
        OnChargeValueChanged(ChargeValue);
        OnValueChanged(ChargeValue);
    }

    public bool CheckValueTakeover(int value)
    {
        if(value >= ChargeValue + CurrentShields)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Odejmuje i zwraca ladunek z uwzglednieniem aktualego modyfikatora ataku.
    /// </summary>
    public int ExtractChargeValue(float chargeFactorNormalized, bool isSendBetweenSameParent)
    {
        int chargeValue = GetChargePercent(chargeFactorNormalized);
        SubstractCharge(chargeValue);

        // Obliczanie wzmocnienia ladunku do wyslania.
        if(isSendBetweenSameParent == false)
        {
            return (int)(chargeValue * ValuesModifiers.AttackChargeValueModifier.FinalValue);
        }

        return chargeValue;
    }

    public void SubstractCharge(int value)
    {
        SetChargeValue(ChargeValue - value);
    }

    public void SubstractReceivedChargeValue(int value)
    {
        int cachedShields = CurrentShields;
        SubstractShields(value);

        value = value - (cachedShields - CurrentShields);
        SetChargeValue(ChargeValue - value);
    }

    public void SubstractShields(int value)
    {
        if(CurrentShields - value <= Constants.DEFAULT_VALUE)
        {
            value = CurrentShields;
        }

        SetShieldsValue(CurrentShields - value);
    }

    public void AddChargeValue(int value)
    {
        SetChargeValue(ChargeValue + value);
    }

    public int CalculateChargeValueAfterTakeover(int value)
    {
        return value - (CurrentShields + ChargeValue);
    }

    public override string ToString()
    {
        return ValuesModifiers.ToString();
    }

    private void RefreshLimits()
    {
        SetShieldsValue(CurrentShields);
        SetChargeValue(ChargeValue);
    }

    private void RefreshShieldsValue(float deltaTimeMs)
    {
        float delayMs = ValuesModifiers.ChargeRegenerationMsModifier.FinalValue + CurrentModeValues.ShieldsRegenerationDelayMs;

        ShieldsTimeCounter += deltaTimeMs;
        if (ShieldsTimeCounter >= delayMs)
        {
            IncreaseShieldsValue();
            ShieldsTimeCounter = Constants.DEFAULT_VALUE;
        }
    }

    private void RefreshChargeValue(float deltaTimeMs)
    {
        float delayMs = ValuesModifiers.ChargeRegenerationMsModifier.FinalValue + CurrentModeValues.ChargeDelayMs;
        ChargeTimeCounter += deltaTimeMs;
        if(ChargeTimeCounter >= delayMs)
        {
            IncreaseChargeValue();
            ChargeTimeCounter = Constants.DEFAULT_VALUE;
        }
    }

    private void IncreaseShieldsValue()
    {
        SetShieldsValue(CurrentShields + 1);
    }

    private void IncreaseChargeValue()
    {
        SetChargeValue(ChargeValue + 1);
    }

    #endregion

    #region Enums

    [Serializable]
    public class Modifiers
    {
        #region Fields



        #endregion

        #region Propeties

        public ModifiableIntValue ShieldsLimitModifier {
            get;
            private set;
        } = new ModifiableIntValue();

        public ModifiableIntValue ChargeLimitModifier {
            get;
            private set;
        } = new ModifiableIntValue();

        public ModifiableFloatValue ShieldsRegenerationMsModifier {
            get;
            private set;
        } = new ModifiableFloatValue();

        public ModifiableFloatValue ChargeRegenerationMsModifier {
            get;
            private set;
        } = new ModifiableFloatValue();

        /// <summary>
        /// Modifikator reprezetujacy wartosc procentowa, gdzie 1 = 100%;
        /// </summary>
        public ModifiableFloatValue AttackChargeValueModifier {
            get;
            private set;
        } = new ModifiableFloatValue();

        #endregion

        #region Methods

        public Modifiers()
        {
            ShieldsLimitModifier = new ModifiableIntValue(Constants.SHIELDS_DEFAULT_VALUE);
            ChargeLimitModifier = new ModifiableIntValue(Constants.CHARGE_DEFAULT_VALUE);
            ShieldsRegenerationMsModifier = new ModifiableFloatValue(Constants.SHIELDS_REGEN_MS_DEFAULT);
            ChargeRegenerationMsModifier = new ModifiableFloatValue(Constants.CHARGE_REGEN_MS_DEFAULT);
            AttackChargeValueModifier = new ModifiableFloatValue(Constants.CHARGE_ATTACK_MODIFIER_DEFAULT);
        }

        public override string ToString()
        {
            return string.Format("[ShieldsLimitModifier: {0} \n ChargeLimitModifier: {1} \n ShieldsRegenerationMsModifier: {2} \n ChargeRegenerationMsModifier: {3} \n AttackChargeValueModifier: {4} \n]",
                ShieldsLimitModifier.ToString(),
                ChargeLimitModifier.ToString(),
                ShieldsRegenerationMsModifier.ToString(),
                ChargeRegenerationMsModifier.ToString(),
                AttackChargeValueModifier.ToString()
                );
        }

        #endregion

        #region Enums



        #endregion
    }


    #endregion
}
