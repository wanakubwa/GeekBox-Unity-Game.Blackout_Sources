using System;
using UnityEngine;

[Serializable]
public class NodeSecondChanceModeEffect : NodeModeEffectBase
{
    #region Fields

    [SerializeField]
    private int chargeAfterRespawn = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public int ChargeAfterRespawn { 
        get => chargeAfterRespawn; 
        private set => chargeAfterRespawn = value; 
    }

    #endregion

    #region Methods

    public NodeSecondChanceModeEffect()
    {
        DebugName = typeof(NodeSecondChanceModeEffect).Name;
    }

    public NodeSecondChanceModeEffect(NodeSecondChanceModeEffect source) : base(source) 
    {
        ChargeAfterRespawn = source.ChargeAfterRespawn;
    }

    public override NodeModeEffectBase Copy()
    {
        return new NodeSecondChanceModeEffect(this);
    }

    public override bool CheckTakeoverPossible(MapNode target)
    {
        target.Values.SetChargeValue(ChargeAfterRespawn);
        SetNormalProfileToNode(target);

        return false;
    }

    #endregion

    #region Enums



    #endregion
}
