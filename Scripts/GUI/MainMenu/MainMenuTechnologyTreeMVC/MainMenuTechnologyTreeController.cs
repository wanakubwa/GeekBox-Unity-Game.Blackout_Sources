using UnityEngine;
using UpgradesGUI;

[RequireComponent(typeof(MainMenuTechnologyTreeModel), typeof(MainMenuTechnologyTreeView))]
public class MainMenuTechnologyTreeController : CarouselScreenController
{
    #region Fields

    [SerializeField]
    private UpgradesTreePanelController upgradesTreePanel;

    #endregion

    #region Propeties

    public UpgradesTreePanelController UpgradesTreePanel { get => upgradesTreePanel; }

    private MainMenuTechnologyTreeModel CurrentModel
    {
        get;
        set;
    }

    private MainMenuTechnologyTreeView CurrentView
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void FocusScreen()
    {
        base.FocusScreen();

        CurrentModel = GetModel<MainMenuTechnologyTreeModel>();
        CurrentView = GetView<MainMenuTechnologyTreeView>();

        UpgradesTreePanel.RefreshPanel();
        CurrentView.RefreshPanel();
    }

    public override void UnFocusScreen()
    {
        base.UnFocusScreen();

        UpgradesTreePanel.ClearPanel();
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnUpgradePointsChanged += HandleUpgradePointsChanged;
            PlayerManager.Instance.Wallet.OnKPointsChanged += HandleKPointsChanged;
        }
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnUpgradePointsChanged -= HandleUpgradePointsChanged;
            PlayerManager.Instance.Wallet.OnKPointsChanged -= HandleKPointsChanged;
        }
    }

    public void BuyUpgradePoint()
    {
        CurrentModel.TryBuyUpgradePoint();
    }

    public void OnAdsButtonClick()
    {
        CurrentModel.AddKPointsAfterADS();
    }

    private void HandleUpgradePointsChanged(int currentPoints)
    {
        CurrentView.RefreshUpgradesPointsInfo(currentPoints);
    }

    private void HandleKPointsChanged(int currentPoints)
    {
        CurrentView.RefreshUpgradePointPriceInfo();
    }

    #endregion

    #region Enums



    #endregion
}
