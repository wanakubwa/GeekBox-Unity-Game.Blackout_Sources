using UnityEngine;

namespace UpgradesGUI
{
    [RequireComponent(typeof(UpgradesTreePanelView), typeof(UpgradeTreePanelModel))]
    public class UpgradesTreePanelController : UIController
    {
        #region Fields



        #endregion

        #region Propeties

        private UpgradesTreePanelView CurrentView { 
            get;
            set;
        }

        private UpgradeTreePanelModel CurrentModel {
            get;
            set;
        }

        #endregion

        #region Methods

        public void BuySelectedUpgrade(UpgradeNode upgradeDefinition)
        {
            CurrentModel.TryBuyUpgrade(upgradeDefinition);
        }

        public void RefreshPanel()
        {
            CurrentView.RefreshPanel();
        }

        public void ClearPanel()
        {
            CurrentView.CleanData();
        }

        public void Awake()
        {
            CurrentModel = GetModel<UpgradeTreePanelModel>();
            CurrentView = GetView<UpgradesTreePanelView>();

            CurrentModel.Initialize();
            CurrentView.Initialize();
        }

        public override void AttachEvents()
        {
            base.AttachEvents();

            if(PlayerManager.Instance != null)
            {
                PlayerManager.Instance.Wallet.OnUnlockedUpgrade += HandleUnlockedUpgrade;
                PlayerManager.Instance.Wallet.OnUpgradePointsChanged += HandleUpgradePointsChanged;
            }
        }

        public override void DettachEvents()
        {
            base.DettachEvents();

            if (PlayerManager.Instance != null)
            {
                PlayerManager.Instance.Wallet.OnUnlockedUpgrade -= HandleUnlockedUpgrade;
                PlayerManager.Instance.Wallet.OnUpgradePointsChanged -= HandleUpgradePointsChanged;
            }
        }

        private void HandleUnlockedUpgrade(int unlockedUpgradeId)
        {
            CurrentView.RefreshUpgradeWithNeighbors(unlockedUpgradeId);
        }

        private void HandleUpgradePointsChanged(int currentPoints)
        {
            CurrentView.RefreshAllUpgrades();
        }

        #endregion

        #region Enums



        #endregion
    }
}

