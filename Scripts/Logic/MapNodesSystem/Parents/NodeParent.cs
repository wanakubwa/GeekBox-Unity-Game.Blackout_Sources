using AISystem;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeParent : IIDEquatable, ICleanable, IAIParentActor
{
    #region Fields

    [SerializeField]
    private int parentId = Constants.DEFAULT_ID;
    [SerializeField]
    private List<int> nodesIdCollection = new List<int>();
    [SerializeField]
    private ParentSettings settings = new ParentSettings();
    [SerializeField]
    private ParentValues modifiersValues = new ParentValues();

    #endregion

    #region Propeties

    public event Action<MapNode> OnNodeAdded = delegate { };
    public event Action<MapNode> OnNodeRemoved = delegate { };

    private Action<NodeParent> OnNodesCollectionEmptyHandler { get; set; } = delegate { };

    public int ID { 
        get => parentId; 
        private set => parentId = value; 
    }

    public List<int> NodesIdCollection { 
        get => nodesIdCollection; 
        private set => nodesIdCollection = value; 
    }

    public ParentSettings Settings { 
        get => settings; 
        private set => settings = value; 
    }

    public ParentValues ModifiersValues {
        get => modifiersValues;
        protected set => modifiersValues = value;
    }

    public NodeParent AIActor => this;

    private int ActiveCharges
    {
        get;
        set;
    } = Constants.DEFAULT_VALUE;

    #endregion

    #region Methods

    public NodeParent()
    {

    }

    public NodeParent(int id)
    {
        ID = id;
    }

    public NodeParent(NodeParent source)
    {
        ID = source.ID;
        Settings = source.settings;
        ModifiersValues = source.ModifiersValues;
        NodesIdCollection = new List<int>(source.NodesIdCollection);
    }

    public bool IDEqual(int otherId)
    {
        return ID == otherId;
    }

    public bool IsPlayerOrNeutralParent()
    {
        return IDEqual(Constants.NODE_PLAYER_PARENT_ID) || IsNeutralParent();
    }

    public bool IsNeutralParent()
    {
        return IDEqual(Constants.NODE_NEUTRAL_PARENT_ID);
    }

    public virtual void Initialize(Action<NodeParent> OnNodesEmptyCalback)
    {
        RegisterAIActor();
        InitializeNodes();

        OnNodesCollectionEmptyHandler = OnNodesEmptyCalback;
    }

    public void CleanData()
    {
        UnregisterAIActor();
    }

    public void RemoveNode(MapNode node)
    {
        ModifiersValues.RemoveModifiers(node, ID);
        NodesIdCollection.Remove(node.NodeId);
        OnNodeRemoved(node);

        CheckParentLoose();
    }

    public void AddNode(MapNode node)
    {
        if(NodesIdCollection.Contains(node.NodeId) == false)
        {
            ModifiersValues.ApplyModifiers(node, ID);
            NodesIdCollection.Add(node.NodeId);
            OnNodeAdded(node);
        }
    }

    public List<MapNode> GetMapNodesReferences()
    {
        List<MapNode> output = new List<MapNode>();

        MapManager mapManager = MapManager.Instance;
        for(int i =0; i < NodesIdCollection.Count; i++)
        {
            output.Add(mapManager.MapNodesCollection.GetElementByID(NodesIdCollection[i]));
        }

        return output;
    }

    public void RegisterAIActor()
    {
        if(IsPlayerOrNeutralParent() == false)
        {
            AIManager.Instance.RegisterParentActor(this);
        }
    }

    public void UnregisterAIActor()
    {
        AIManager.Instance.UnregisterParentActor(this);
    }

    public void IncreaseActiveCharges()
    {
        ActiveCharges += 1;
    }

    public void DecreaseActiveCharges()
    {
        ActiveCharges -= 1;

        CheckParentLoose();
    }

    private void CheckParentLoose()
    {
        if(ActiveCharges == Constants.DEFAULT_VALUE && NodesIdCollection.Count < 1)
        {
            OnNodesCollectionEmptyHandler(this);
        }
    }

    private void InitializeNodes()
    {
        for(int i = 0; i < NodesIdCollection.Count; i++)
        {
            MapNode targetNode = MapManager.Instance.MapNodesCollection.GetElementByID(NodesIdCollection[i]);
            ModifiersValues.ApplyModifiers(targetNode, ID);
        }
    }

    #endregion

    #region Enums



    #endregion
}
