using GeekBox.Scripts.ValuesSystem;
using System;

[Serializable]
public class ConnectionValues
{
    #region Fields



    #endregion

    #region Propeties

    public TargetModifiableFloatValue Speed {
        get;
        private set;
    } = new TargetModifiableFloatValue();

    #endregion

    #region Methods

    public void RegisterParents(NodeParent firstParent, NodeParent secondParent)
    {
        RegisterParent(firstParent);
        RegisterParent(secondParent);
    }

    public void RegisterParent(NodeParent parent)
    {
        Speed.AddTarget(parent.ID, Constants.CONNECTION_SPEED_BASE);
        ApplyParentPerks(parent);
    }

    public void UnregisterParent(int parentId)
    {
        Speed.RemoveTarget(parentId);
    }

    public void ApplyParentPerks(NodeParent parent)
    {
        //todo;
    }

    #endregion

    #region Enums



    #endregion
}
