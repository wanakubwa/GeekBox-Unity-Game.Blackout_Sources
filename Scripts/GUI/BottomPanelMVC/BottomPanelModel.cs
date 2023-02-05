using System.Collections.Generic;

public class BottomPanelModel : UIModel
{
    #region Fields



    #endregion

    #region Propeties

    public MapNode CurrentSelectedNode
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public void SetSelectedNode(MapNode node)
    {
        CurrentSelectedNode = node;
    }

    public ItemStatus GetItemStatusForSelectedNode(NodeProfileBase profile)
    {
        // todo: dodanie sprawdzenia czy upgrade zostal odblokowany na drzewie rozwoju.
        if(CheckPlayerCanBuyUpgrade(profile.ProfileCost.ChargeCost) == true)
        {
            if(CheckPlayerHasUpgrade(profile.ModeType) == true)
            {
                return ItemStatus.CURRENT_UPGRADE;
            }

            return ItemStatus.AVAIBLE;
        }

        return ItemStatus.NO_AVAIBLE;
    }

    public List<NodeProfileBase> GetAvaibleToBuyUpgrades()
    {
        List<NodeProfileBase> avaibleUpgrades = new List<NodeProfileBase>();

        List<NodeProfileBase> profileBases = TechnologyTreeManager.Instance.GetAvailableUpgradesProfile();
        for(int i =0; i < profileBases.Count; i++)
        {
            if(profileBases[i].Icon != null)
            {
                avaibleUpgrades.Add(profileBases[i]);
            }
        }

        return avaibleUpgrades;
    }

    public void TryBuyUpgrade(NodeProfileBase selectedProfile)
    {
        if(CheckPlayerCanBuyUpgrade(selectedProfile.ProfileCost.ChargeCost) == true && CheckPlayerHasUpgrade(selectedProfile.ModeType) == false)
        {
            CurrentSelectedNode.SetNodeModeProfile(selectedProfile);
            CurrentSelectedNode.Values.SubstractCharge(selectedProfile.ProfileCost.ChargeCost);
        }

        // todo; jakies info dlczego nie moze kupic.
    }

    private bool CheckPlayerCanBuyUpgrade(float chargeCost)
    {
        return CurrentSelectedNode != null && CurrentSelectedNode.ParentId == Constants.NODE_PLAYER_PARENT_ID 
            && CurrentSelectedNode.Values.ChargeValue >= chargeCost;
    }

    private bool CheckPlayerHasUpgrade(NodeModeType modeType)
    {
        return CurrentSelectedNode.Values.ProfileModeType == modeType;
    }

    #endregion

    #region Enums

    public enum ItemStatus
    {
        AVAIBLE,
        NO_AVAIBLE,
        LOCKED,
        CURRENT_UPGRADE
    }

    #endregion
}
