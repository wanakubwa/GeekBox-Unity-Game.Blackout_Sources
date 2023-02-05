using System;
using UnityEngine;

[Serializable]
public class NodeModeValues
{
    #region Fields

    [SerializeField]
    private int shieldsLimit;
    [SerializeField]
    private float shieldsRegenerationDelayMs;
    [SerializeField]
    private float chargeGrowthDelayMs;
    [SerializeField]
    private int chargeLimit;

    #endregion

    #region Propeties

    public int ShieldsLimit { 
        get => shieldsLimit; 
        private set => shieldsLimit = value; 
    }

    public float ChargeDelayMs { 
        get => chargeGrowthDelayMs; 
        private set => chargeGrowthDelayMs = value; 
    }

    public float ShieldsRegenerationDelayMs { 
        get => shieldsRegenerationDelayMs; 
        private set => shieldsRegenerationDelayMs = value; 
    }

    public int ChargeLimit { 
        get => chargeLimit; 
        private set => chargeLimit = value; 
    }

    #endregion

    #region Methods

    public NodeModeValues()
    {

    }

    public NodeModeValues(NodeModeValues source)
    {
        ShieldsLimit = source.ShieldsLimit;
        ChargeDelayMs = source.ChargeDelayMs;
        ShieldsRegenerationDelayMs = source.ShieldsRegenerationDelayMs;
        ChargeLimit = source.ChargeLimit;
    }


    #endregion

    #region Enums



    #endregion
}
