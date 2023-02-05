using System;
using System.Collections.Generic;

[Serializable]
public class NodeSuicideModeEffect : NodeModeEffectBase
{
    #region Fields

    private const int AFTER_SUICIDE_VALUE = 0;

    #endregion

    #region Propeties


    #endregion

    #region Methods

    public NodeSuicideModeEffect()
    {
        DebugName = typeof(NodeSuicideModeEffect).Name;
    }

    public NodeSuicideModeEffect(NodeModeEffectBase source) : base(source){ }

    public override NodeModeEffectBase Copy()
    {
        return new NodeSuicideModeEffect(this);
    }

    public override void Apply(MapNode target)
    {
        base.Apply(target);

        int otherNodesCount = ParentsManager.Instance.GetAllParentsNodesCountExceptNeutral(target.ParentId);
        if(otherNodesCount == 0)
        {
            return;
        }

        int currentNodeCharge = target.Values.ChargeValue;
        int chargeToSubstract = currentNodeCharge / otherNodesCount;

        List<MapNode> allNodes = MapManager.Instance.MapNodesCollection;
        for(int i =0; i < allNodes.Count; i++)
        {
            if(allNodes[i].ParentId != Constants.NODE_NEUTRAL_PARENT_ID && allNodes[i].ParentId != target.ParentId)
            {
                allNodes[i].CheckChargeCollide(chargeToSubstract, target.ParentId);
            }
        }

        target.Values.SetChargeValue(AFTER_SUICIDE_VALUE);
        target.SetNodeModeProfile(NodeContentSettings.Instance.GetNodeProfileByModeType(NodeModeType.NORMAL));

        // Do osiagniec.
        InGameEvents.Instance.NotifyKamikazeEffectUse(target.ParentId);
    }

    #endregion

    #region Enums



    #endregion
}
