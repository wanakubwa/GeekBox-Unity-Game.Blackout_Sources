using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class MapConnection : IIDEquatable, ICleanable
{
    #region Fields

    [SerializeField]
    private int firstNodeId;
    [SerializeField]
    private int secondNodeId;
    [SerializeField]
    private int id;
    [SerializeField]
    private bool isSpecialConnection;

    [SerializeField]
    private List<ConnectionCharge> chargesCollection = new List<ConnectionCharge>();

    #endregion

    #region Propeties

    public int FirstNodeId { 
        get => firstNodeId; 
        private set => firstNodeId = value; 
    }

    public int SecondNodeId { 
        get => secondNodeId; 
        private set => secondNodeId = value; 
    }

    public int ID {
        get => id;
        private set =>id = value;
    }

    public MapNode FirstNodeReference
    {
        get;
        set;
    }

    public MapNode SecondNodeReference
    {
        get;
        set;
    }

    public MapConnectionVisualization ConnectionVisualization
    {
        get;
        private set;
    }

    public List<ConnectionCharge> ChargesCollection { 
        get => chargesCollection; 
        private set => chargesCollection = value; 
    }

    public ConnectionValues Values {
        get;
        set;
    } = new ConnectionValues();

    public bool IsSpecialConnection { 
        get => isSpecialConnection; 
        private set => isSpecialConnection = value; 
    }

    #endregion

    #region Methods

    public MapConnection() { }

    public MapConnection(int nodeID, MapConnectionVisualization visualization, MapNode first, MapNode second)
    {
        ID = nodeID;
        ConnectionVisualization = visualization;

        SetNodes(first, second);
    }

    public MapConnection(MapConnection source)
    {
        ID = source.ID;
        FirstNodeId = source.FirstNodeId;
        SecondNodeId = source.SecondNodeId;
        ChargesCollection = source.ChargesCollection.Clone();
        IsSpecialConnection = source.IsSpecialConnection;
    }

    public void SetIsSpecialConnection(bool isSpecial)
    {
        IsSpecialConnection = isSpecial;
        ConnectionVisualization.Initialize(this);
    }

    public void Initialize()
    {
        for(int i =0; i < ChargesCollection.Count; i++)
        {
            NodeParent parent =  MapManager.Instance.MapNodesCollection.GetElementByID(ChargesCollection[i].SenderNodeId)?.CachedParentReference;
            ChargesCollection[i].SetVisualization(GetConnectionChargeVisualization(parent.Settings));
            ChargesCollection[i].SetListeners(OnChargeHitTargetNodeHandler, OnChargeDestroyHandler);
            ChargesCollection[i].initialize(parent);
        }

        Values.RegisterParents(FirstNodeReference.CachedParentReference, SecondNodeReference.CachedParentReference);
        ConnectionVisualization.Initialize(this);
    }

    public void Refresh(NodeParent updatedParent, NodeParent oldParent)
    {
        RefreshConnectionVisualization();

        Values.UnregisterParent(oldParent.ID);
        Values.RegisterParents(FirstNodeReference.CachedParentReference, SecondNodeReference.CachedParentReference);
    }

    public MapNode GetNodeOppositeTo(int nodeID)
    {
        return FirstNodeId == nodeID ? SecondNodeReference : FirstNodeReference;
    }

    public MapNode GetNodeOwnedByParent(int parentID)
    {
        return FirstNodeReference.ParentId == parentID ? FirstNodeReference : SecondNodeReference;
    }

    public void CreateChargeBetweenNodes(MapNode senderNode, MapNode reciverNode, int chargeValue, bool isSendBetweenSameParent)
    {
        Vector2 direction = GeometryMath.GetDirectionBetweenPoins2D(senderNode.MapPostion, reciverNode.MapPostion);
        ConnectionChargeVisualization chargeVisualization = GetConnectionChargeVisualization(senderNode.CachedParentReference.Settings);

        float speed = Values.Speed.GetValueForTarget(senderNode.ParentId).FinalValue;
        bool isChargeIncreasedByAttackMode = senderNode.Values.ValuesModifiers.AttackChargeValueModifier.FinalValue > Constants.CHARGE_ATTACK_MODIFIER_DEFAULT && isSendBetweenSameParent == false;
        ConnectionCharge charge = new ConnectionCharge(
            Guid.NewGuid().GetHashCode(), 
            chargeValue, 
            speed, 
            senderNode.ID, 
            senderNode.ParentId, 
            ID,
            direction, 
            senderNode.MapPostion, 
            isChargeIncreasedByAttackMode,
            reciverNode.ID
            );

        charge.SetVisualization(chargeVisualization);
        charge.SetListeners(OnChargeHitTargetNodeHandler, OnChargeDestroyHandler);
        charge.initialize(senderNode.CachedParentReference);

        ChargesCollection.Add(charge);
    }

    private ConnectionChargeVisualization GetConnectionChargeVisualization(ParentSettings infoSettings)
    {
        ConnectionChargeVisualization visualization = ConnectionVisualization.GetChargeVisualization();
        visualization.SetSettings(infoSettings);
        return visualization;
    }

    public bool IDEqual(int otherId)
    {
        return ID == otherId;
    }

    public void CleanData()
    {
        if(ConnectionVisualization != null)
        {
            GameObject.Destroy(ConnectionVisualization.gameObject);
        }

        if(FirstNodeReference != null)
        {
            FirstNodeReference.OnMapPositionChanged -= OnConnectedNodeMapPositionChanged;
        }

        if (SecondNodeReference != null)
        {
            SecondNodeReference.OnMapPositionChanged -= OnConnectedNodeMapPositionChanged;
        }
    }

    public void SetVisualization(MapConnectionVisualization visualization)
    {
        ConnectionVisualization = visualization;
    }

    public void SetNodes(MapNode firstNode, MapNode secondNode)
    {
        SetFirstNode(firstNode);
        SetSecondNode(secondNode);
    }

    public bool IsContainsNodeId(int nodeid)
    {
        return FirstNodeId == nodeid || SecondNodeId == nodeid;
    }

    public bool IsConnectionBetweenNodes(int firstId, int secondId)
    {
        return (FirstNodeId == firstId && SecondNodeId == secondId) || (FirstNodeId == secondId && SecondNodeId == firstId);
    }

    public bool IsConnectBewtweenSameParent()
    {
        return FirstNodeReference.ParentId == SecondNodeReference.ParentId;
    }

    public void RemoveConnectionFromNodes()
    {
        FirstNodeReference.RemoveConnectionId(ID);
        SecondNodeReference.RemoveConnectionId(ID);
    }

    public void RefreshConnectionVisualization()
    {
        ConnectionVisualization.RefreshVisualization(FirstNodeReference, SecondNodeReference);
    }

    public void CustomUpdate(float deltaTimeS)
    {
        for(int i =0; i< ChargesCollection.Count; i++)
        {
            ChargesCollection[i].CustomUpdate(deltaTimeS);
        }
    }

    private void OnChargeHitTargetNodeHandler(int chargeId)
    {
        ConnectionCharge charge =  ChargesCollection.GetElementByID(chargeId);
        if(charge != null)
        {
            NodeParent parent = ParentsManager.Instance.ParentsCollection.GetElementByID(charge.SenderParentId);
            parent?.DecreaseActiveCharges();

            charge.CleanData();
            ChargesCollection.Remove(charge);
        }
    }

    private void OnChargeDestroyHandler(int chargeId)
    {
        ConnectionCharge charge = ChargesCollection.GetElementByID(chargeId);
        if (charge != null)
        {
            NodeParent parent = ParentsManager.Instance.ParentsCollection.GetElementByID(charge.SenderParentId);
            parent?.DecreaseActiveCharges();

            charge.CleanData();
            ChargesCollection.Remove(charge);
        }
    }

    private void SetFirstNode(MapNode node)
    {
        FirstNodeReference = node;
        FirstNodeId = node.ID;

        ConnectionVisualization.SetFirstPosition(node.MapPostion);
        node.OnMapPositionChanged += OnConnectedNodeMapPositionChanged;
    }

    private void SetSecondNode(MapNode node)
    {
        SecondNodeReference = node;
        SecondNodeId = node.ID;

        ConnectionVisualization.SetSecondPosition(node.MapPostion);
        node.OnMapPositionChanged += OnConnectedNodeMapPositionChanged;
    }

    private void OnConnectedNodeMapPositionChanged(Vector3 positon)
    {
        ConnectionVisualization.SetPositions(FirstNodeReference.MapPostion, SecondNodeReference.MapPostion);
    }

    #endregion

    #region Enums



    #endregion
}
