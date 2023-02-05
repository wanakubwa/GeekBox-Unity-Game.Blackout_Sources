using GeekBox.OdinSerializer;
using System;
using System.Collections.Generic;

[Serializable]
public class ParentsManagerMemento : MementoBase
{
    #region Fields

    [OdinSerialize]
    private List<NodeParent> parentsCollectionSaved = new List<NodeParent>();

    #endregion

    #region Propeties

    public List<NodeParent> ParentsCollectionSaved { get => parentsCollectionSaved; private set => parentsCollectionSaved = value; }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        ParentsManager manager = sourceManager as ParentsManager;
        ParentsCollectionSaved = new List<NodeParent>(manager.ParentsCollection);
    }

    #endregion

    #region Enums



    #endregion
}
