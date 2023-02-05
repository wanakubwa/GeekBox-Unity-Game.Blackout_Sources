using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class NodeModeEffectBase
{
    #region Fields

    [SerializeField, ReadOnly]
    private string debugName = "NONE";

    [ReadOnly]
    [SerializeField]
    private int id = Constants.DEFAULT_ID;

    [Space]
    [SerializeField]
    private float percentFactor;

    #endregion

    #region Propeties

    public float PercentFactor {
        get => percentFactor;
        private set => percentFactor = value;
    }

    public string DebugName { 
        get => debugName; 
        protected set => debugName = value; 
    }

    public int Id { 
        get => id; 
        private set => id = value; 
    }

    #endregion

    #region Methods

    public NodeModeEffectBase() 
    {

    }

    public NodeModeEffectBase(NodeModeEffectBase source)
    {
        PercentFactor = source.PercentFactor;
        DebugName = source.DebugName;
        Id = Guid.NewGuid().GetHashCode();
    }

    public virtual NodeModeEffectBase Copy()
    {
        return new NodeModeEffectBase(this);
    }

    public virtual void Apply(MapNode target)
    {

    }

    public virtual void Revert(MapNode target)
    {

    }

    public virtual void Refresh(MapNode target)
    {
        Revert(target);
        Apply(target);
    }

    public virtual bool CheckTakeoverPossible(MapNode target)
    {
        return true;
    }

    protected void SetNormalProfileToNode(MapNode target)
    {
        target.SetNodeModeProfile(NodeContentSettings.Instance.GetNodeProfileByModeType(NodeModeType.NORMAL));
    }

    #endregion

    #region Enums



    #endregion
}
