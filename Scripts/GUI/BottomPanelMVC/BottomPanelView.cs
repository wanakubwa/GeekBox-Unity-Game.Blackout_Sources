using System.Collections.Generic;
using UnityEngine;

class BottomPanelView : UIView
{
    #region Fields

    [SerializeField]
    private RectTransform upgradesContent;
    [SerializeField]
    private UpgradeUIElement upgradeElementPrefab;
    [SerializeField]
    private GameObject emptyInfo;

    #endregion

    #region Propeties

    public RectTransform UpgradesContent { 
        get => upgradesContent;
    }

    public UpgradeUIElement UpgradeElementPrefab { 
        get => upgradeElementPrefab;
    }
    public GameObject EmptyInfo {
        get => emptyInfo;
    }

    private List<UpgradeUIElement> SpawnedUpgrades { get; set; } = new List<UpgradeUIElement>();
    private BottomPanelModel CurrentModel { get; set; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<BottomPanelModel>();

        RebuildView();
    }

    /// <summary>
    /// Odswiezenie informacji z wykorzystaniem zespawnowanych elementow UI.
    /// </summary>
    public void RefreshView()
    {
        RefreshUpgradesInfo();
    }

    private void RefreshUpgradesInfo()
    {
        for(int i = 0; i < SpawnedUpgrades.Count; i++)
        {
            SpawnedUpgrades[i].RefreshInfo(CurrentModel.GetItemStatusForSelectedNode(SpawnedUpgrades[i].CachedProfile));
        }
    }

    /// <summary>
    /// Budowa UI na nowo wywolana w momencie otwiercia sceny gry.
    /// </summary>
    private void RebuildView()
    {
        List<NodeProfileBase> profiles = GetModel<BottomPanelModel>().GetAvaibleToBuyUpgrades();
        EmptyInfo.SetActive(profiles.Count < 1);
        SpawnProfiles(profiles);
    }

    private void SpawnProfiles(List<NodeProfileBase> avaibleProfiles)
    {
        SpawnedUpgrades.ClearDestroy();

        for(int i = 0; i < avaibleProfiles.Count; i++)
        {
            UpgradeUIElement spawnedUpgrade = Instantiate(UpgradeElementPrefab);
            spawnedUpgrade.transform.ResetParent(UpgradesContent);
            spawnedUpgrade.gameObject.SetActive(true);
            spawnedUpgrade.SetInfo(avaibleProfiles[i], CurrentModel.GetItemStatusForSelectedNode(avaibleProfiles[i]));

            SpawnedUpgrades.Add(spawnedUpgrade);
        }
    }

    #endregion

    #region Enums



    #endregion
}
