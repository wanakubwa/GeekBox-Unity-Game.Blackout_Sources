using UnityEngine;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;
using SaveLoadSystem;

public class MapManager : SingletonScenarioSaveableManager<MapManager, MapManagerMemento>, IContentLoadable
{
    #region Fields

    [SerializeField]
    private MapNodeVisualization nodeVisualizationPrefab;
    [SerializeField]
    private MapConnectionVisualization connectionVisualization;
    [SerializeField]
    private List<MapNode> mapNodesCollection = new List<MapNode>();
    [SerializeField]
    private List<MapConnection> mapConnectionsCollection = new List<MapConnection>();

    [Title("Containers)")]
    [SerializeField]
    private Transform nodesContainer;
    [SerializeField]
    private Transform connectionsContainer;

    #endregion

    #region Propeties

    public event Action OnMapNodeDeleted = delegate { };

    public MapNodeVisualization NodeVisualizationPrefab { 
        get => nodeVisualizationPrefab;
    }

    public List<MapNode> MapNodesCollection { 
        get => mapNodesCollection; 
        private set => mapNodesCollection = value; 
    }

    public MapConnectionVisualization ConnectionVisualization { get => connectionVisualization; }

    public List<MapConnection> MapConnectionsCollection { 
        get => mapConnectionsCollection; 
        private set => mapConnectionsCollection = value; 
    }

    public Transform NodesContainer { get => nodesContainer; }

    public Transform ConnectionsContainer { get => connectionsContainer; }

    public float ChargeSendPercentFactor
    {
        get;
        set;
    } = 100f;

    private SceneLabel ActualScene { get; set; } = SceneLabel.NO_SET;

    #endregion

    #region Methods

    public void SetChargePercentFactor(float valuePercent)
    {
        ChargeSendPercentFactor = valuePercent;
    }

    public bool CheckNodesHasConnection(MapNode firstNode, MapNode secondNode)
    {
        bool isConnected = false;

        for(int i =0; i < firstNode.ConnectionsIds.Count; i++)
        {
            MapConnection connection = MapConnectionsCollection.GetElementByID(firstNode.ConnectionsIds[i]);
            if(connection != null && connection.IsConnectionBetweenNodes(firstNode.ID, secondNode.ID) == true)
            {
                isConnected = true;
                break;
            }
        }

        return isConnected;
    }

    public MapConnection TryGetNodesConnection(MapNode firstNode, MapNode secondNode)
    {
        MapConnection output = null;

        for (int i = 0; i < firstNode.ConnectionsIds.Count; i++)
        {
            MapConnection connection = MapConnectionsCollection.GetElementByID(firstNode.ConnectionsIds[i]);
            if (connection != null && connection.IsConnectionBetweenNodes(firstNode.ID, secondNode.ID) == true)
            {
                output = connection;
                break;
            }
        }

        return output;
    }

    public void CreateNewMapConnection(MapNode firstNode, MapNode secondNode)
    {
        MapConnectionVisualization newVisualization = GetMapConnectionVisualization();

        MapConnection connection = new MapConnection(Guid.NewGuid().GetHashCode(), newVisualization, firstNode, secondNode);
        firstNode.AddConnectionId(connection.ID);
        secondNode.AddConnectionId(connection.ID);
        connection.Initialize();

        MapConnectionsCollection.Add(connection);
    }

    public MapNode CreateNode(Vector3 position, NodeParent parent)
    {
        MapNodeVisualization newNodeVisualization = GetNodeVisualization(position);

        MapNode nodeInfo = new MapNode(parent, Guid.NewGuid().GetHashCode(), newNodeVisualization, new Vector3(position.x, position.y, Constants.DEFAULT_VALUE));
        nodeInfo.SetListeners(TakeoverNodeBySenderNodeId);
        nodeInfo.SetNodeModeProfile(NodeContentSettings.Instance.GetDefaultProfile());

        newNodeVisualization.SetNodeReference(nodeInfo);
        MapNodesCollection.Add(nodeInfo);

        return nodeInfo;
    }

    public void CreateNode(NodeParent parent, MapNode node)
    {
        MapNodeVisualization newNodeVisualization = GetNodeVisualization(node.MapPostion);
        newNodeVisualization.SetNodeReference(node);

        node.SetVisualization(newNodeVisualization);
        node.SetListeners(TakeoverNodeBySenderNodeId);
        node.SetParent(parent);

        NodeModeType modeType = node.Values == null? NodeContentSettings.Instance.DefaultType : node.Values.ProfileModeType;
        node.SetNodeModeProfile(NodeContentSettings.Instance.GetNodeProfileByModeType(modeType));

        mapNodesCollection.Add(node);
    }

    private MapNodeVisualization GetNodeVisualization(Vector3 position)
    {
        MapNodeVisualization newNodeVisualization = Instantiate(NodeVisualizationPrefab);
        newNodeVisualization.ResetPosition(NodesContainer, position);

        return newNodeVisualization;
    }

    public void DeleteMapConnectionById(int connectionId)
    {
        MapConnection connection = MapConnectionsCollection.GetElementByID(connectionId);
        DeleteMapConnection(connection);
    }

    public void DeleteMapConnection(MapConnection connection)
    {
        if (connection != null)
        {
            connection.RemoveConnectionFromNodes();
            connection.CleanData();
        }

        MapConnectionsCollection.RemoveElementByID(connection.ID);
    }

    public void ChangeNodeParent(MapNode node, NodeParent newParent)
    {
        ParentsManager parentsManager = ParentsManager.Instance;
        parentsManager.SwapNodeParents(node.ParentId, newParent.ID, node);

        node.SetParent(newParent);
        node.SetNodeModeProfile(NodeContentSettings.Instance.GetNodeProfileByModeType(NodeModeType.NORMAL));

        MapNode[] neighbors = node.GetNeighborsNodes();
        foreach (MapNode mapNode in neighbors)
        {
            mapNode.RefreshCurrentEffect();
        }
    }

    public void TakeoverNodeBySenderNodeId(MapNode node, int senderParentId, int restValue)
    {
        NodeParent newParent = ParentsManager.Instance.ParentsCollection.GetElementByID(senderParentId);
        if(newParent != null)
        {
            ChangeNodeParent(node, newParent);
            node.Values.SetChargeValue(restValue);
            node.Values.SetShieldsValue(Constants.DEFAULT_VALUE);
        }
    }

    public void DeleteMapNodeByID(int nodeId)
    {
        MapNode node = MapNodesCollection.GetElementByID(nodeId);
        if(node != null)
        {
            for(int i = node.ConnectionsIds.Count -1; i >= 0; i--)
            {
                DeleteMapConnectionById(node.ConnectionsIds[i]);
            }

            node.CleanData();
        }

        MapNodesCollection.RemoveElementByID(nodeId);
        OnMapNodeDeleted();
    }

    public override void CreateNewScenario()
    {
        for(int i =0; i < MapNodesCollection.Count; i++)
        {
            if(MapNodesCollection[i].CachedNodeVisualization != null)
            {
                Destroy(MapNodesCollection[i].CachedNodeVisualization);
            }
        }

        MapNodesCollection.Clear();
    }

    public override void LoadManager(MapManagerMemento memento)
    {
        DeserializeNodesCollection(memento.MapNodesSave);
        DeserializeConnectionsCollection(memento.MapConnectionsSave);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        GameManager.Instance.OnSceneLabelChanged += OnSceneLabelChangedHandler;
    }

    public void SendChargeBetweenNodesWithFactor(MapNode sender, MapNode reciverNode)
    {
        SendChargeBetweenNodes(sender, reciverNode, ChargeSendPercentFactor * Constants.PERCENT_TO_DECIMAL_FACTOR);
    }

    public void SendChargeBetweenNodes(MapNode sender, MapNode reciverNode, float chargeFactorNormalized = 1f)
    {
        if(CheckNodesHasConnection(sender, reciverNode) == false)
        {
            return;
        }

        bool isSendBetweenSameParent = sender.ParentId == reciverNode.ParentId;
        int chargeValue = sender.ExtractChargeValueToSend(chargeFactorNormalized, isSendBetweenSameParent);
        if(chargeValue == Constants.DEFAULT_VALUE)
        {
            return;
        }

        MapConnection connection = TryGetNodesConnection(sender, reciverNode);
        if(connection != null)
        {
            connection.CreateChargeBetweenNodes(sender, reciverNode, chargeValue, isSendBetweenSameParent);
        }
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        GameManager.Instance.OnSceneLabelChanged -= OnSceneLabelChangedHandler;
    }

    private void Update()
    {
        if(ActualScene != SceneLabel.GAME)
        {
            return;
        }

        if(TimeManager.Instance.IsTimeCounting == true)
        {
            RefreshNodes(Time.deltaTime * 1000f);
            RefreshConnections(Time.deltaTime);
        }
    }

    private void RefreshConnections(float deltaTimeS)
    {
        foreach (MapConnection connection in MapConnectionsCollection)
        {
            connection.CustomUpdate(deltaTimeS);
        }
    }

    private void RefreshNodes(float deltaTimeMs)
    {
        foreach(MapNode node in MapNodesCollection)
        {
            node.RefreshNode(deltaTimeMs);
        }
    }

    private MapConnectionVisualization GetMapConnectionVisualization()
    {
        MapConnectionVisualization newVisualization = Instantiate(ConnectionVisualization);
        newVisualization.transform.ResetParent(ConnectionsContainer);
        return newVisualization;
    }

    private void DeserializeNodesCollection(List<MapNode> savedNodesCollection)
    {
        for(int i =0; i < savedNodesCollection.Count; i++)
        {
            NodeParent parent = ParentsManager.Instance.ParentsCollection.GetElementByID(savedNodesCollection[i].ParentId);
            if(parent != null)
            {
                MapNode node = new MapNode(savedNodesCollection[i]);
                CreateNode(parent, node);
            }
            else
            {
                Debug.LogFormat("Brak rodzica o ID: {0}".SetColor(Color.red), savedNodesCollection[i].ParentId);
            }
        }
    }

    private void DeserializeConnectionsCollection(List<MapConnection> connections)
    {
        for(int i =0; i < connections.Count; i++)
        {
            MapConnection connection = new MapConnection(connections[i]);
            connection.SetVisualization(GetMapConnectionVisualization());
            connection.SetNodes(MapNodesCollection.GetElementByID(connection.FirstNodeId), MapNodesCollection.GetElementByID(connection.SecondNodeId));
            connection.Initialize();

            MapConnectionsCollection.Add(connection);
        }
    }

    public void LoadContent()
    {
        foreach (MapNode node in MapNodesCollection)
        {
            node.Initialize();
        }
    }

    public void UnloadContent()
    {
        MapNodesCollection.ClearDestroy();
        MapConnectionsCollection.ClearClean();
    }

    private void OnSceneLabelChangedHandler(SceneLabel label)
    {
        ActualScene = label;
    }

    #endregion

    #region Enums



    #endregion
}
