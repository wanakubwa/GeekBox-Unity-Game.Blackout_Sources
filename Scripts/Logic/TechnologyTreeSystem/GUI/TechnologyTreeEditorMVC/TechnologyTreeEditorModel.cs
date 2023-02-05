using System.Collections.Generic;
using UnityEngine;

public class TechnologyTreeEditorModel : UIModel
{
    #region Fields



    #endregion

    #region Propeties

    private TechnologyTreeEditorView CurrentView
    {
        get;
        set;
    }

    public UpgradeNode FirstSelectedNode {
        get;
        set;
    }

    #endregion

    #region Methods

    public void SetFirstSelectedNode(UpgradeNode node)
    {
        FirstSelectedNode = node;
    }

    public void AddConnectionToSelectedNode(UpgradeNode target)
    {
        FirstSelectedNode.AddUnlockedUpgradeId(target.ID);
        target.AddRequiredUpgradeId(FirstSelectedNode.ID);
        NotifyTechnologyTreeSettingsRefresh();
    }

    public void DeleteUpgradeNode(UpgradeNode target)
    {
        TechnologyTreeSettings.Instance.RemoveTechnologyUpgrade(target);
    }

    public void HandleEndCreatingConnection()
    {
        FirstSelectedNode = null;
    }

    public override void Initialize()
    {
        base.Initialize();

        CurrentView = GetView<TechnologyTreeEditorView>();
    }

    public List<UpgradeNode> GetAllUpgrades()
    {
        return TechnologyTreeSettings.Instance.TechnlogyUpgradesDefinitions;
    }

    public List<UpgradeNode> GetRequiredUpgradesForUpgrade(UpgradeNode upgrade)
    {
        return TechnologyTreeSettings.Instance.GetUpgradesNodesByIds(upgrade.RequiredUpgradesIds);
    }

    public UpgradeNode CreateNewUpgrade(Vector2 screenPosition)
    {
        UpgradeNode upgrade = new UpgradeNode();
        upgrade.SetEditorPosition(CalculateUpgradeEditorPosition(screenPosition));
        TechnologyTreeSettings.Instance.AddTechnologyUpgrade(upgrade);

        return upgrade;
    }

    public Vector3 CalculateUpgradeEditorPosition(Vector2 screenPosition)
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CurrentView.MainContent, screenPosition, null, out localPosition);
        return localPosition;
    }

    private void NotifyTechnologyTreeSettingsRefresh()
    {
        TechnologyTreeSettings.Instance.HandleUpgradesCollectionChanged();
    }

    #endregion

    #region Enums



    #endregion
}
