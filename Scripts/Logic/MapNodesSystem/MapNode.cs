using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class MapNode : IIDEquatable, ICleanable, IDestroyable
{
    #region Fields

    [SerializeField]
    private Vector3 mapPostion;
    [SerializeField]
    private int parentId = Constants.DEFAULT_ID;
    [SerializeField]
    private int nodeId = Constants.DEFAULT_ID;
    [SerializeField]
    private List<int> connectionsIds = new List<int>();
    [SerializeField]
    private NodeValues values = new NodeValues();

    #endregion

    #region Propeties

    public event Action<Vector3> OnMapPositionChanged = delegate { };
    public event Action<MapNode> OnRetake = delegate { };

    /// <summary>
    /// Parametry: MapNode - aktualna referencja, int - id rodzica przejmujacego, int - ladunek pozostaly po przejeciu.
    /// </summary>
    private Action<MapNode, int, int> OnTakeoverHandler { get; set; } = delegate { };

    public Vector3 MapPostion { 
        get => mapPostion; 
        private set => mapPostion = value; 
    }

    public int ParentId { 
        get => parentId; 
        private set => parentId = value; 
    }

    public int NodeId { 
        get => nodeId; 
        private set => nodeId = value; 
    }

    public MapNodeVisualization CachedNodeVisualization
    {
        get;
        private set;
    }

    public NodeParent CachedParentReference
    {
        get;
        set;
    }

    public int ID => NodeId;

    public List<int> ConnectionsIds { 
        get => connectionsIds; 
        private set => connectionsIds = value; 
    }

    public NodeValues Values { 
        get => values; 
        private set => values = value; 
    }

    public List<MapConnection> ConnectionsReference
    {
        get;
        private set;
    } = new List<MapConnection>();

    private NodeModeEffectBase CurrentEffect
    {
        get; set;
    } = new NodeModeEffectBase();

    #endregion

    #region Methods

    public MapNode()
    {
        // Serialize.
    }

    public MapNode(NodeParent newParent, int newNodeId, MapNodeVisualization nodeVisualization, Vector3 mapPos)
    {
        CachedNodeVisualization = nodeVisualization;
        NodeId = newNodeId;
        MapPostion = mapPos;

        SetParent(newParent);
    }

    public MapNode(MapNode source)
    {
        MapPostion = source.MapPostion;
        ParentId = source.ParentId;
        NodeId = source.NodeId;
        ConnectionsIds = source.ConnectionsIds;
        Values = new NodeValues(source.Values);
    }

    public void SetMapPosition(Vector2 newPosition)
    {
        MapPostion = new Vector3(newPosition.x, newPosition.y, Constants.DEFAULT_NODE_Z_POSITION);
        CachedNodeVisualization.SetMapPosition(newPosition);
        OnMapPositionChanged(MapPostion);
    }

    public void SetSelected(bool isSelected)
    {
        CachedNodeVisualization.SetSelectionVisualization(isSelected);
    }
    
    public List<MapConnection> GetConnectionsWithEnemy()
    {
        List<MapConnection> connectionsWithEnemy = new List<MapConnection>();
        for (int i = 0; i < ConnectionsReference.Count; i++)
        {
            if(ConnectionsReference[i].IsConnectBewtweenSameParent() == false)
            {
                connectionsWithEnemy.Add(ConnectionsReference[i]);
            }
        }

        return connectionsWithEnemy;
    }

    public List<MapConnection> GetConnectionsWithAlly()
    {
        List<MapConnection> connectionsWithAlly = new List<MapConnection>();
        for (int i = 0; i < ConnectionsReference.Count; i++)
        {
            if (ConnectionsReference[i].IsConnectBewtweenSameParent() == true)
            {
                connectionsWithAlly.Add(ConnectionsReference[i]);
            }
        }

        return connectionsWithAlly;
    }

    public MapNode[] GetNeighborsNodes()
    {
        MapNode[] neighbours = new MapNode[ConnectionsReference.Count];
        for(int i =0; i < ConnectionsReference.Count; i++)
        {
            neighbours[i] = ConnectionsReference[i].GetNodeOppositeTo(NodeId);
        }

        return neighbours;
    }

    public bool HasOnlyAllyOrNeutralConnections()
    {
        bool hasOnlySaveConnections = true;
        List<MapConnection> enemyConnections = GetConnectionsWithEnemy();
        foreach (MapConnection connection in enemyConnections)
        {
            if(connection.GetNodeOppositeTo(ID).CachedParentReference.IsNeutralParent() == false)
            {
                hasOnlySaveConnections = false;
                break;
            }
        }

        return hasOnlySaveConnections;
    }

    public bool HasOnlyAllyConnections()
    {
        return GetConnectionsWithAlly().Count == ConnectionsReference.Count;
    }

    public int ExtractChargeValueToSend(float chargeFactorNormalized, bool isSendBetweenSameParent)
    {
        return Values.ExtractChargeValue(chargeFactorNormalized, isSendBetweenSameParent);
    }

    public void AddConnectionId(int connectionId)
    {
        ConnectionsIds.Add(connectionId);
    }

    public void RemoveConnectionId(int connectionId)
    {
        ConnectionsIds.Remove(connectionId);
    }

    public void SetListeners(Action<MapNode, int, int> takeoverCallback)
    {
        OnTakeoverHandler = takeoverCallback;
    }

    public bool IDEqual(int otherId)
    {
        return ID == otherId;
    }

    public void CleanData()
    {
        Destroy();
        ParentsManager.Instance.DeleteNodeFromParentByID(ParentId, this);
    }

    public void Destroy()
    {
        GameObject.Destroy(CachedNodeVisualization.gameObject);
    }

    public void SetParent(NodeParent parent)
    {
        NodeParent oldParent = CachedParentReference;
        CachedParentReference = parent;
        ParentId = parent.ID;
        CachedNodeVisualization.RefreshVisualization(parent.Settings);

        RefreshConnections(oldParent);
    }

    public void Initialize()
    {
        Values.SetListeners(CachedNodeVisualization.RefreshChargeValue, CachedNodeVisualization.RefreshShieldsValue);
        OnChargeChangedHandler(Values.ChargeValue);
        OnShieldsChangedHandler(Values.CurrentShields);

        // todo obsluzyc usuwanie polaczen jak znika.
        for(int i =0; i < ConnectionsIds.Count; i++)
        {
            ConnectionsReference.Add(MapManager.Instance.MapConnectionsCollection.GetElementByID(ConnectionsIds[i]));
        }
    }

    public void SetVisualization(MapNodeVisualization nodeVisualization)
    {
        if(CachedNodeVisualization != null)
        {
            GameObject.Destroy(CachedNodeVisualization.gameObject);
        }

        CachedNodeVisualization = nodeVisualization;
    }

    public void SetNodeModeProfile(NodeProfileBase currentProfile)
    {
        Values.SetNodeMode(currentProfile.ModeType, currentProfile.GetValues());
        CachedNodeVisualization.SetModeVisualization(currentProfile.GetVisualization());
        ChangeModeEffect(currentProfile.GetModeEffect());
    }

    /// <summary>
    /// Wywolywane co klatke w celu przeliczenia ilosci ladunku.
    /// </summary>
    public void RefreshNode(float deltaTimeMs)
    {
        Values.RefreshValues(deltaTimeMs);
    }

    public void CheckChargeCollide(int value, int senderParentId)
    {
        if (senderParentId == ParentId)
        {
            Values.AddChargeValue(value);
            return;
        }
        else if(CurrentEffect.CheckTakeoverPossible(this) == false)
        {
            return;
        }
        else if(Values.CheckValueTakeover(value) == true)
        {
            int valueAfterTakeover = Values.CalculateChargeValueAfterTakeover(value);
            OnTakeoverHandler(this, senderParentId, valueAfterTakeover);
            OnRetake(this);
        }
        else
        {
            Values.SubstractReceivedChargeValue(value);
        }
    }

    public void ChangeModeEffect(NodeModeEffectBase newEffect)
    {
        CurrentEffect.Revert(this);
        newEffect.Apply(this);
        CurrentEffect = newEffect;
    }

    public void RefreshCurrentEffect()
    {
        CurrentEffect.Refresh(this);
    }

    public override string ToString()
    {
        return string.Format("Parent ID: {0} \n {1}", ParentId, Values.ToString());
    }

    private void OnShieldsChangedHandler(int value)
    {
        CachedNodeVisualization.RefreshShieldsValue(value);
    }

    private void OnChargeChangedHandler(int value)
    {
        CachedNodeVisualization.RefreshChargeValue(value);
    }

    private void RefreshConnections(NodeParent oldParent)
    {
        foreach (MapConnection connection in ConnectionsReference)
        {
            connection.Refresh(CachedParentReference, oldParent);
        }
    }

    #endregion

    #region Enums



    #endregion
}
