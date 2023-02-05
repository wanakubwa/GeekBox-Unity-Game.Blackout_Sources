using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpgradeUIElement : MonoBehaviour
{
    #region Fields

    private const float STATUS_SWITCH_ANIMATION_TIME = 0.1f;

    [SerializeField]
    private Image upgradeImage;
    [SerializeField]
    private TextMeshProUGUI priceText;

    [Header("Status settings")]
    [SerializeField]
    private Color noAvaibleStatusIconColor = Color.white;
    [SerializeField]
    private Color noAvaibleStatusPriceColor = Color.red;
    [SerializeField]
    private Color currentUpgradeStatusIconColor = Color.yellow;
    [SerializeField]
    private Sprite lockedIcon;

    #endregion

    #region Propeties

    public Image UpgradeImage { get => upgradeImage; }
    public TextMeshProUGUI PriceText { get => priceText; }

    public NodeProfileBase CachedProfile
    {
        get;
        set;
    }

    private  Color NoAvaibleStatusIconColor { 
        get => noAvaibleStatusIconColor; 
    }

    private Sprite LockedIcon { 
        get => lockedIcon;  
    }

    private Color NoAvaibleStatusPriceColor { 
        get => noAvaibleStatusPriceColor; 
    }

    private Color CurrentUpgradeStatusIconColor { 
        get => currentUpgradeStatusIconColor;
    }

    #endregion

    #region Methods

    public void SetInfo(NodeProfileBase profileBase, BottomPanelModel.ItemStatus itemStatus)
    {
        CachedProfile = profileBase;

        UpgradeImage.sprite = profileBase.Icon;
        PriceText.text = profileBase.ProfileCost.ChargeCost.ToStringKiloFormat();

        RefreshInfo(itemStatus);
    }

    public void RefreshInfo(BottomPanelModel.ItemStatus itemStatus)
    {
        switch (itemStatus)
        {
            case BottomPanelModel.ItemStatus.AVAIBLE:
                SetAvaibleStatus();
                break;
            case BottomPanelModel.ItemStatus.NO_AVAIBLE:
                SetNotAvaibleStatus();
                break;
            case BottomPanelModel.ItemStatus.LOCKED:
                SetLockedStatus();
                break;
            case BottomPanelModel.ItemStatus.CURRENT_UPGRADE:
                SetCurrentUpgradeStatus();
                break;
            default:
                break;
        }
    }

    private void SetNotAvaibleStatus()
    {
        UpgradeImage.DOColor(NoAvaibleStatusIconColor, STATUS_SWITCH_ANIMATION_TIME);
        PriceText.DOColor(NoAvaibleStatusPriceColor, STATUS_SWITCH_ANIMATION_TIME);
    }

    private void SetLockedStatus()
    {
        //todo; ustawienie ikony kludki.
    }

    private void SetAvaibleStatus()
    {
        UpgradeImage.DOColor(Color.white, STATUS_SWITCH_ANIMATION_TIME);
        PriceText.DOColor(Color.white, STATUS_SWITCH_ANIMATION_TIME);
    }

    private void SetCurrentUpgradeStatus()
    {
        UpgradeImage.DOColor(CurrentUpgradeStatusIconColor, STATUS_SWITCH_ANIMATION_TIME);
        PriceText.DOColor(CurrentUpgradeStatusIconColor, STATUS_SWITCH_ANIMATION_TIME);
    }

    #endregion

    #region Enums



    #endregion
}
