using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ConnectionCharge : IIDEquatable, IObjectCloneable<ConnectionCharge>, ICleanable
{
    #region Fields

    [SerializeField]
    private int chargeValue;
    [SerializeField]
    private int senderNodeId;
    [SerializeField]
    private int targetNodeId;
    [SerializeField]
    private int senderParentId;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int connectionId;
    [SerializeField]
    private int id;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private Vector2 mapPosition;

    [SerializeField]
    private bool isAttackMode = false;

    #endregion

    #region Propeties

    // Parameters: int - chargeID.
    private Action<int> OnHitTargetNodeHandler { get; set; } = delegate { };
    private Action<int> OnDestroyCharge { get; set; } = delegate { };

    public int ChargeValue { 
        get => chargeValue; 
        private set => chargeValue = value; 
    }

    public int SenderNodeId { 
        get => senderNodeId; 
        private set => senderNodeId = value; 
    }

    public float MoveSpeed { 
        get => moveSpeed; 
        private set => moveSpeed = value; 
    }

    private ConnectionChargeVisualization CurrentVisualization
    {
        get;
        set;
    }

    public int ID { 
        get => id; 
        private set => id = value; 
    }

    public Vector2 Direction { 
        get => direction; 
        private set => direction = value; 
    }

    public Vector2 MapPosition { 
        get => mapPosition; 
        private set => mapPosition = value; 
    }

    public int SenderParentId { 
        get => senderParentId; 
        private set => senderParentId = value;
    }
    public bool IsAttackMode {
        get => isAttackMode;
        private set => isAttackMode = value; 
    }
    public int TargetNodeId {
        get => targetNodeId; 
        private set => targetNodeId = value;
    }
    public int ConnectionId {
        get => connectionId; 
        private set => connectionId = value; 
    }

    #endregion

    #region Methods

    public ConnectionCharge()
    {
        // Serialization.
    }

    public ConnectionCharge(ConnectionCharge source)
    {
        ChargeValue = source.ChargeValue;
        SenderNodeId = source.SenderNodeId;
        MoveSpeed = source.MoveSpeed;
        ID = source.ID;
        Direction = source.Direction;
        MapPosition = source.MapPosition;
        SenderParentId = source.SenderParentId;
        IsAttackMode = source.IsAttackMode;
        TargetNodeId = source.TargetNodeId;
        ConnectionId = source.ConnectionId;
    }

    public ConnectionCharge(int id, int charge, float moveSpeed, int senderNodeId, int senderParentId, int connectionId, Vector2 moveDirection, Vector2 position, bool isAttackMode, int targetNodeId)
    {
        ID = id;
        ChargeValue = charge;
        MoveSpeed = moveSpeed;
        SenderNodeId = senderNodeId;
        Direction = moveDirection;
        MapPosition = position;
        SenderParentId = senderParentId;
        IsAttackMode = isAttackMode;
        TargetNodeId = targetNodeId;
        ConnectionId = connectionId;
    }

    public void initialize(NodeParent parent)
    {
        parent.IncreaseActiveCharges();
    }

    public void SetListeners(Action<int> onHitTargetNodeCallback, Action<int> onDestroyChargeCallback)
    {
        OnHitTargetNodeHandler = onHitTargetNodeCallback;
        OnDestroyCharge = onDestroyChargeCallback;
    }

    public void SetCharge(int value)
    {
        ChargeValue = value;
        CurrentVisualization.SetChargeValueText(ChargeValue);
    }

    public void OnChargeHitTargetNodeHandler(int nodeId)
    {
        if(nodeId != SenderNodeId)
        {
            OnHitTargetNodeHandler(ID);
        }
    }

    public void OnChargeHitCharge(ConnectionCharge otherCharge)
    {
        // Wyzsze id ladunku obsluguje kolizje.
        // Jezeli naleza do tego samego polaczenia.
        if(otherCharge.ID < ID && otherCharge.ConnectionId == ConnectionId)
        {
            // 20 - 10 = 10
            int chargeDelta = ChargeValue - otherCharge.ChargeValue;
            if(chargeDelta > 0)
            {
                otherCharge.NotifyChargeDestroy();
                SetCharge(chargeDelta);
            }
            else if (chargeDelta < 0)
            {
                // 10 - 20 = -10
                otherCharge.SetCharge(-chargeDelta);
                NotifyChargeDestroy();
            }
            else
            {
                otherCharge.NotifyChargeDestroy();
                NotifyChargeDestroy();
            }
        }
    }

    public void NotifyChargeDestroy()
    {
        OnDestroyCharge(ID);
    }

    public void SetVisualization(ConnectionChargeVisualization visualization)
    {
        CurrentVisualization = visualization;
        CurrentVisualization.RefreshVisualization(MapPosition);
        CurrentVisualization.SetInfo(this);
    }

    public void CustomUpdate(float deltaTimeS)
    {
        Vector2 currentStep = (Direction * MoveSpeed) * deltaTimeS;
        MapPosition = MapPosition + currentStep;

        CurrentVisualization.RefreshVisualization(MapPosition);
    }

    public bool IDEqual(int otherId)
    {
        return ID == otherId;
    }

    public ConnectionCharge Clone()
    {
        return new ConnectionCharge(this);
    }

    public void CleanData()
    {
        if(CurrentVisualization != null)
        {
            GameObject.Destroy(CurrentVisualization.gameObject);
        }
    }

    #endregion

    #region Enums



    #endregion
}
