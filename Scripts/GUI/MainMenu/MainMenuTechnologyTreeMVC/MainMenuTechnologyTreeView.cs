using TMPro;
using UnityEngine;

public class MainMenuTechnologyTreeView : UIView
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI availableUpgradesInfoText;
    [SerializeField]
    private TextMeshProUGUI upgradePointsPriceText;

    #endregion

    #region Propeties

    public TextMeshProUGUI AvailableUpgradesInfoText { 
        get => availableUpgradesInfoText; 
    }
    public TextMeshProUGUI UpgradePointsPriceText { 
        get => upgradePointsPriceText;
    }

    private MainMenuTechnologyTreeModel CurrentModel {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        RefreshUpgradesPointsInfo(CurrentModel.GetPlayerUpgradesPoints());
        RefreshUpgradePointPriceInfo();
    }

    public void RefreshPanel()
    {
        RefreshUpgradesPointsInfo(CurrentModel.GetPlayerUpgradesPoints());
        RefreshUpgradePointPriceInfo();
    }

    public void RefreshUpgradesPointsInfo(int ammount)
    {
        AvailableUpgradesInfoText.SetText(string.Format(Constants.LOC_AVAILABLE_UPGRADES.Localize(), ammount));
        RefreshUpgradePointPriceInfo();
    }

    public void RefreshUpgradePointPriceInfo()
    {
        int nextUpgradePointPrice = CurrentModel.GetNextUpgradePointPrice();
        Color infoColor = CurrentModel.HasPlayerEnoughKPoints(nextUpgradePointPrice) == true ? Color.white : Constants.RED_COLOR;
        UpgradePointsPriceText.SetText(nextUpgradePointPrice.ToString().SetColor(infoColor));
    }

    private void Awake()
    {
        CurrentModel = GetModel<MainMenuTechnologyTreeModel>();
    }

    #endregion

    #region Enums



    #endregion
}
