using System;

namespace TechnologyTree
{
    [Serializable]
    public class UpgradePointsController
    {
        #region Fields

        private const float BASE_UP_VALUE = 14.5f;

        #endregion

        #region Propeties

        private IUPCostFunction CostFunction {
            get;
            set;
        }

        #endregion

        #region Methods

        public UpgradePointsController()
        {
            CostFunction = new UPCostLogFunction();
        }

        public int GetNextUpCost()
        {
            PlayerData.PlayerWallet wallet = PlayerManager.Instance.Wallet;
            return CostFunction.GetNextUPCost(wallet.UpgradePointsAmmount + wallet.UnlockedUpgradesIds.Count + 1, BASE_UP_VALUE);
        }

        #endregion

        #region Enums



        #endregion
    }
}
