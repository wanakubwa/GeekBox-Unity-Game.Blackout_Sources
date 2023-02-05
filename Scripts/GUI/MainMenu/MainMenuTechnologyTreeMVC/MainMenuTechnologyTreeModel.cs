using GeekBox.Ads;
using UnityEngine;

public class MainMenuTechnologyTreeModel : UIModel
{
    #region Fields

    [SerializeField]
    private int pointsToAddAfterWathAd = 500;

    #endregion

    #region Propeties



    #endregion

    #region Methods

    public int GetPlayerUpgradesPoints()
    {
        return PlayerManager.Instance.Wallet.UpgradePointsAmmount;
    }

    public int GetNextUpgradePointPrice()
    {
        return TechnologyTreeManager.Instance.GetNextUPCost();
    }

    public void TryBuyUpgradePoint()
    {
        int cost = GetNextUpgradePointPrice();

        PlayerData.PlayerWallet wallet = PlayerManager.Instance.Wallet;
        if (wallet.TrySubstractKPoints(cost) == true)
        {
            wallet.AddUpgradePoints(1);
        }
    }

    public void AddKPointsAfterADS()
    {
        EasyMobileManager.Instance.ShowRewardedAD(() =>
        {
            PlayerData.PlayerWallet wallet = PlayerManager.Instance.Wallet;
            wallet.AddKPoints(pointsToAddAfterWathAd);
        });
    }

    public bool HasPlayerEnoughKPoints(int reqPoints)
    {
        return PlayerManager.Instance.Wallet.KPointsAmmount >= reqPoints;
    }

    #endregion

    #region Enums



    #endregion
}
