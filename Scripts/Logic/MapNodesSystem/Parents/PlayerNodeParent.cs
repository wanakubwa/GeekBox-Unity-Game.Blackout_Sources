using System;

[Serializable]
public class PlayerNodeParent : NodeParent
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods
    public PlayerNodeParent() : base() { /* Serialization purposes */ }

    public PlayerNodeParent(NodeParent source) : base(source) { }

    public override void Initialize(Action<NodeParent> OnNodesEmptyCalback)
    {
        // Modyfikatory gracza przechowywane sa w dedykowanym managerze aby mozliwa byla obsluga np. drzewa technologicznego.
        // Rodzic gracza w grze zawsze na starcie dostaje modyfikatory, ktore zgromadzil gracz odblokowujac drzewo.
        ModifiersValues = PlayerManager.Instance.PlayerParentValues;

        base.Initialize(OnNodesEmptyCalback);
    }

    #endregion

    #region Enums



    #endregion
}
