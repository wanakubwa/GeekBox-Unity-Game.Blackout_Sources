using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewNodeProfile.asset", menuName = "Custom/Node/NodeProfile")]
public class NodeProfileBase : SerializedScriptableObject
{
    #region Fields

    [SerializeField]
    private NodeModeType modeType = NodeModeType.DEFAULT;
    [SerializeField]
    private string modeNameKey = string.Empty;
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private Costs profileCost = new Costs();

    [SerializeField]
    private NodeModeValues values = new NodeModeValues();
    [SerializeField]
    private NodeModeVisualizationBase visualizationPrefab;

    [Space]
    [OdinSerialize]
    private NodeModeEffectBase modeEffect = new NodeModeEffectBase();

    #endregion

    #region Propeties

    public NodeModeType ModeType { 
        get => modeType; 
    }

    public string ModeNameKey { 
        get => modeNameKey; 
    }

    public Sprite Icon { 
        get => icon; 
    }

    public NodeModeVisualizationBase VisualizationPrefab { 
        get => visualizationPrefab; 
    }

    public Costs ProfileCost { 
        get => profileCost; 
    }

    private NodeModeValues Values {
        get => values;
    }

    private NodeModeEffectBase ModeEffect { 
        get => modeEffect; 
        set => modeEffect = value; 
    }

    #endregion

    #region Methods

    public NodeModeValues GetValues()
    {
        return new NodeModeValues(Values);
    }

    public NodeModeEffectBase GetModeEffect()
    {
        return ModeEffect.Copy();
    }

    public NodeModeVisualizationBase GetVisualization()
    {
        return VisualizationPrefab;
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        ValidateModeEffect();
    }

    // To nie jest najlepsze rozwiazanie ale bez custom edytora jest wystarczajace.
    private void ValidateModeEffect()
    {
        NodeModeEffectBase newModeEffect = null;

        switch (ModeType)
        {
            case NodeModeType.SLOWNING:
                newModeEffect = new NodeSlowModeEffect();
                break;
            case NodeModeType.SPEED:
                newModeEffect = new NodeSpeedModeEffect();
                break;
            case NodeModeType.SUICIDE:
                newModeEffect = new NodeSuicideModeEffect();
                break;
            case NodeModeType.ATTACK:
                newModeEffect = new NodeAttackModeEffect();
                break;
            case NodeModeType.PARASITE:
                newModeEffect = new NodeParasiteModeEffect();
                break;
            case NodeModeType.SECOND_CHANCE:
                newModeEffect = new NodeSecondChanceModeEffect();
                break;
            default:
                newModeEffect = new NodeModeEffectBase();
                break;
        }

        ModeEffect = newModeEffect.GetType() == ModeEffect.GetType() ? ModeEffect : newModeEffect;
    }

#endif

    #endregion

    #region Enums

    [Serializable]
    public class Costs
    {
        #region Fields

        [SerializeField]
        private int chargeCost;

        #endregion

        #region Propeties

        public int ChargeCost { 
            get => chargeCost; 
            private set => chargeCost = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Enums



        #endregion
    }

    #endregion
}
