using UnityEngine;
using System.Collections;
using TMPro;
using Sirenix.OdinInspector;

public class ConnectionChargeVisualization : MonoBehaviour, IChargeCollidable
{
    #region Fields

    private const string OVERFLOW_CHARGE_INFO = "99+";
    private const int OVERFLOW_CHARGE_VALUE = 99;

    [SerializeField]
    private TextMeshPro chargeValueText;
    [SerializeField]
    private SpriteRenderer mainSpriteRenderer;

    [Title("Modes Visualization")]
    [SerializeField]
    private GameObject attackModeIcon;

    #endregion

    #region Propeties

    public TextMeshPro ChargeValueText { 
        get => chargeValueText; 
    }

    public ConnectionCharge CachedCharge { get; set; }

    public SpriteRenderer MainSpriteRenderer { 
        get => mainSpriteRenderer; 
    }

    public GameObject AttackModeIcon { 
        get => attackModeIcon; 
    }

    #endregion

    #region Methods

    public void SetSettings(ParentSettings infoSettings)
    {
        SetMainColor(infoSettings.MainColor);
        SetFontColor(infoSettings.FontColor);
    }

    public void SetInfo(ConnectionCharge charge) 
    {
        CachedCharge = charge;
        SetChargeValueText(charge.ChargeValue);
        AttackModeIcon.SetActive(charge.IsAttackMode);
    }

    public void SetChargeValueText(int value)
    {
        ChargeValueText.text = FormatChargeValueText(value);
    }

    public void RefreshVisualization(Vector2 newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    public void OnHitNode(int nodeId)
    {
        CachedCharge.OnChargeHitTargetNodeHandler(nodeId);
    }

    public void OnHitCharge(ConnectionCharge other)
    {
        CachedCharge.OnChargeHitCharge(other);
    }

    private void SetMainColor(Color color)
    {
        MainSpriteRenderer.color = color;
    }

    private void SetFontColor(Color color)
    {
        ChargeValueText.color = color;
    }

    private string FormatChargeValueText(int chargeValue)
    {
        return chargeValue > OVERFLOW_CHARGE_VALUE ? OVERFLOW_CHARGE_INFO : chargeValue.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // God forgive me for this.
        if(collision.gameObject != null)
        {
            IChargeCollidable collidable = collision.gameObject.GetComponent<IChargeCollidable>();
            if(collidable != null && collidable.GetType() == typeof(MapNodeVisualization) && collidable.GetID() != CachedCharge.SenderNodeId && collidable.GetID() == CachedCharge.TargetNodeId)
            {
                collidable?.Collide(CachedCharge.ChargeValue, CachedCharge.SenderParentId);
                OnHitNode(collidable.GetID());
            }
            else if (collidable != null && collidable.GetType() == typeof(ConnectionChargeVisualization) && collidable.GetID() != CachedCharge.SenderParentId)
            {
                ConnectionChargeVisualization visualization = collidable as ConnectionChargeVisualization;
                if(visualization != null)
                {
                    OnHitCharge(visualization.CachedCharge);
                }
            }
        }
    }

    public void Collide(int charge, int senderParentId)
    {
        //dummy.
    }

    public int GetID()
    {
        return CachedCharge.SenderParentId;
    }

    #endregion

    #region Enums



    #endregion
}
