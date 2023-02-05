using GeekBox.Scripts.ValuesSystem;
using System;
using UnityEngine;

[Serializable]
public class NodeSpeedModeEffect : NodeModeEffectBase
{
    #region Fields



    #endregion

    #region Propeties


    #endregion

    #region Methods

    public NodeSpeedModeEffect()
    {
        DebugName = typeof(NodeSpeedModeEffect).Name;
    }

    public NodeSpeedModeEffect(NodeModeEffectBase source) : base(source){ }

    public override NodeModeEffectBase Copy()
    {
        return new NodeSpeedModeEffect(this);
    }

    public override void Apply(MapNode target)
    {
        base.Apply(target);

        foreach(MapConnection connection in target.ConnectionsReference)
        {
            // Speed nakladany jest na naszego parent bo musi dzialac tylko na nasze polaczenia.
            ModifiableFloatValue speedValue = GetFloatValueForParent(connection, target.ParentId);
            speedValue.AddModsModifier(ModifiersUtils.GetFloatPercentModifier(PercentFactor, Id));
        }
    }

    public override void Revert(MapNode target)
    {
        base.Revert(target);

        foreach (MapConnection connection in target.ConnectionsReference)
        {
            ModifiableFloatValue speedValue = GetFloatValueForParent(connection, target.ParentId);
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
