using GeekBox.UI;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyTreeEditorView : UIView
{
    #region Fields

    [SerializeField]
    private RectTransform upgradesContent;
    [SerializeField]
    private RectTransform connectionsContent;
    [SerializeField]
    private RectTransform mainContent;

    [SerializeField]
    private UpgradeUIEditorElement upgradePrefab;
    [SerializeField]
    private UpgradeUIEditorConnection connectionPrefab;
    [SerializeField]
    private UILineRenderer connectionVisualizationPrefab;

    #endregion

    #region Propeties

    private TechnologyTreeEditorModel CurretModel { 
        get; 
        set; 
    }

    private TechnologyTreeEditorController CurrentController
    {
        get;
        set;
    }

    private UpgradeUIEditorElement UpgradePrefab { get => upgradePrefab; }
    private UpgradeUIEditorConnection ConnectionPrefab { get => connectionPrefab; }

    private List<UpgradeUIEditorElement> UpgradesVisualizationCollection {
        get;
        set;
    } = new List<UpgradeUIEditorElement>();

    private List<UpgradeUIEditorConnection> ConnectionsVisualizationCollection {
        get;
        set;
    } = new List<UpgradeUIEditorConnection>();

    public RectTransform UpgradesContent
    {
        get => upgradesContent;
    }

    public RectTransform ConnectionsContent
    {
        get => connectionsContent;
    }

    public RectTransform MainContent
    {
        get => mainContent;
    }

    private UILineRenderer ConnectionVisualizationPrefab { get => connectionVisualizationPrefab; }
    public UILineRenderer ConnectionVisualization
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurretModel = GetModel<TechnologyTreeEditorModel>();
        CurrentController = GetController<TechnologyTreeEditorController>();

        CreateUpgradesVisualizations(CurretModel.GetAllUpgrades());
        CreateConnectionsVisualizations(UpgradesVisualizationCollection);
    }

    public void DeleteUpgradeVisualization(UpgradeUIEditorElement target)
    {
        UpgradesVisualizationCollection.Remove(target);
        target.CleanData();

        ConnectionsVisualizationCollection.RemoveAll(x => x == null);
    }

    public void CreateSelectedConnectionVisualization(UpgradeUIEditorElement upgradeUi)
    {
        if(ConnectionVisualization != null)
        {
            Destroy(ConnectionVisualization);
        }

        ConnectionVisualization = Instantiate(ConnectionVisualizationPrefab);
        ConnectionVisualization.transform.SetParent(MainContent);
        ConnectionVisualization.SetPoint(0, upgradeUi.CachedUpgrade.EditorPosition);
    }

    public void DestroySelectedConnectionVisualization()
    {
        Destroy(ConnectionVisualization.gameObject);
    }

    public void HandleCreatedConnectionForUpgrades(UpgradeNode requiredUpgrade, UpgradeUIEditorElement dependentUpgrade)
    {
        UpgradeUIEditorElement requiredUpgradeVisualization = UpgradesVisualizationCollection.GetElementByID(requiredUpgrade.ID);
        CreateConnectionBetweenUpgrades(requiredUpgradeVisualization, dependentUpgrade);
    }

    public void AddUpgradeVisualization(UpgradeNode upgradeDefinition)
    {
        UpgradeUIEditorElement upgradeVisualization = GetUpgradeElement(upgradeDefinition);
        UpgradesVisualizationCollection.Add(upgradeVisualization);
    }

    public void ChangeMainContentZoom(float scrollDelta)
    {
        Vector3 calculatedScale = MainContent.localScale + MainContent.localScale * scrollDelta * 0.1f;

        if(calculatedScale.x > 1)
        {
            calculatedScale = Vector3.one;
        }

        MainContent.localScale = calculatedScale;
    }

    private void CreateUpgradesVisualizations(List<UpgradeNode> avaibleUpgrades)
    {
        for(int i = 0; i < avaibleUpgrades.Count; i++)
        {
            AddUpgradeVisualization(avaibleUpgrades[i]);
        }
    }

    private void CreateConnectionsVisualizations(List<UpgradeUIEditorElement> upgradesElements)
    {
        for (int i =0; i < upgradesElements.Count; i++)
        {
            CreateRequiredConnectionsForUpgrade(upgradesElements[i]);
        }
    }

    private void CreateRequiredConnectionsForUpgrade(UpgradeUIEditorElement uiUpgrade)
    {
        for (int i = 0; i < uiUpgrade.CachedUpgrade.RequiredUpgradesIds.Count; i++)
        {
            UpgradeUIEditorElement requiredUpgrade = UpgradesVisualizationCollection.GetElementByID(uiUpgrade.CachedUpgrade.RequiredUpgradesIds[i]);
            CreateConnectionBetweenUpgrades(requiredUpgrade, uiUpgrade);
        }
    }

    /// <summary>
    /// Argument 1: wizualizacja wymaganego upgrejdu, 
    /// Argument 2: wizualizacja upgrejdu, ktory zalezny jest od wymaganego.
    /// Kolejnosc ma znaczenie poniewaz oba konce polaczenia dopasowywane sa na wizyalizacji do roznych przyciskow aby ulatiwc setup.
    /// </summary>
    private void CreateConnectionBetweenUpgrades(UpgradeUIEditorElement requiredUpgrade, UpgradeUIEditorElement dependentUpgrade)
    {
        UpgradeUIEditorConnection connection = GetUpgradeConnection(requiredUpgrade, dependentUpgrade);
        requiredUpgrade.AddConnection(connection);
        dependentUpgrade.AddConnection(connection);

        ConnectionsVisualizationCollection.Add(connection);
    }

    private UpgradeUIEditorElement GetUpgradeElement(UpgradeNode upgradeDefinition)
    {
        UpgradeUIEditorElement upgrade = Instantiate(UpgradePrefab);
        upgrade.transform.ResetParent(UpgradesContent);
        upgrade.Initialize(upgradeDefinition, CurrentController);

        return upgrade;
    }

    private UpgradeUIEditorConnection GetUpgradeConnection(UpgradeUIEditorElement firstDefinition, UpgradeUIEditorElement secondDefinition)
    {
        UpgradeUIEditorConnection connection = Instantiate(ConnectionPrefab);
        connection.transform.SetParent(ConnectionsContent);
        connection.Initialize(firstDefinition, secondDefinition);

        return connection;
    }

    private void Update()
    {
        if(ConnectionVisualization != null)
        {
            ConnectionVisualization.SetPoint(1, CurretModel.CalculateUpgradeEditorPosition(Input.mousePosition));
        }
    }

    #endregion

    #region Enums



    #endregion
}
