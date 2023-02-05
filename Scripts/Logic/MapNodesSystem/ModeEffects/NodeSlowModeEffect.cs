using GeekBox.Scripts.ValuesSystem;
using System;
using UnityEngine;

[Serializable]
public class NodeSlowModeEffect : NodeModeEffectBase
{
    #region Fields


    #endregion

    #region Propeties


    #endregion

    #region Methods

    public NodeSlowModeEffect() : base()
    {
        DebugName = typeof(NodeSlowModeEffect).Name;
    }

    public NodeSlowModeEffect(NodeModeEffectBase source) : base(source)
    {
    }

    public override NodeModeEffectBase Copy()
    {
        return new NodeSlowModeEffect(this);
    }

    public override void Apply(MapNode target)
    {
        base.Apply(target);

        foreach (MapConnection connection in target.ConnectionsReference)
        {
            if(connection.IsConnectBewtweenSameParent() == false)
            {
                // Slow nakladany jest na przeciwnika dla naszego parenta.
                ModifiableFloatValue speedValue = GetFloatValueForParent(connection, connection.GetNodeOppositeTo(target.NodeId).ParentId);
                speedValue.AddModsModifier(ModifiersUtils.GetFloatPercentModifier(-PercentFactor, Id));
            }
        }
    }

    public override void Revert(MapNode target)
    {
        base.Revert(target);

        foreach (MapConnection connection in target.ConnectionsReference)
        {
            ModifiableFloatValue speedValue = GetFloatValueForParent(connection, connection.GetNodeOppositeTo(target.NodeId).ParentId);
            speedValue.RemoveModsModifier(Id);
        }
    }

    private ModifiableFloatValue GetFloatValueForParent(MapConnection connection, int parentId)
    {
        return connection.Values.Speed.GetValueForTarget(parentId);
    }

    #endregion

    #region Enums



    #endregion
}
