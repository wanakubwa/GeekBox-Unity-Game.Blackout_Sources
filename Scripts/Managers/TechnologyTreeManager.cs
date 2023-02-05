using System;
using System.Collections.Generic;
using TechnologyTree;

public class TechnologyTreeManager : ManagerSingletonBase<TechnologyTreeManager>
{
    #region Fields

    public const int ONE_UPGRADE_UP_COST = 1;

    #endregion

    #region Propeties

    public event Action<UpgradeNode> OnBuyUpgrade = delegate { };

    public UpgradePointsController PointsController {
        get;
        set;
    } = new UpgradePointsController();

    private NodeContentSettings NodeSettings {
        get;
        set;
    }

    private TechnologyTreeSettings TechnologySettings { get; set; }

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();

        NodeSettings = NodeContentSettings.Instance;
        TechnologySettings = TechnologyTreeSettings.Instance;
    }

    public List<UpgradeNode> GetAllUpgrades()
    {
        return TechnologySettings.TechnlogyUpgradesDefinitions;
    }

    public UpgradeNode GetUpgradeById(int id)
    {
        return TechnologySettings.GetUpgradeNodeById(id);
    }

    public List<NodeProfileBase> GetAllUpgradesProfile()
    {
        return NodeSettings.NodeModeProfiles;
    }

    public List<NodeProfileBase> GetAvailableUpgradesProfile()
    {
        List<NodeProfileBase> availableModes = new List<NodeProfileBase>();

        List<NodeModeType> unlockedModes = PlayerManager.Instance.Wallet.UnlockedModes;
        for(int i =0; i < unlockedModes.Count; i++)
        {
            availableModes.Add(NodeSettings.GetNodeProfileByModeType(unlockedModes[i]));
        }

        return availableModes;
    }

    public void TryBuyUpgrade(UpgradeNode upgradeDefiniton)
    {
        PlayerManager playerManager = PlayerManager.Instance;
        if (playerManager.Wallet.UpgradePointsAmmount > Constants.DEFAULT_VALUE)
        {
            playerManager.Wallet.AddUnlockedUpgrade(upgradeDefiniton.ID);
            upgradeDefiniton.ApplyEffects(playerManager.PlayerParentValues);
            playerManager.Wallet.SubstractUpgradePoints(ONE_UPGRADE_UP_COST);

            OnBuyUpgrade(upgradeDefiniton);
        }
    }

    public int GetNextUPCost()
    {
        return PointsController.GetNextUpCost(); 
    }

    #endregion

    #region Enums



    #endregion
}
