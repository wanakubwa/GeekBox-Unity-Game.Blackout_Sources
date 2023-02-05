using UnityEngine;
using System.Collections.Generic;
using Blackout.GUI.LvlSelect;
using GeekBox.UI;
using System.Collections;

public class MainMenuLvlSelectView : UIView
{
    #region Fields

    [SerializeField]
    private RectTransform scenariosContent;
    [SerializeField]
    private ScenarioUIRow scenarioRowPrefab;
    [SerializeField]
    private UILineRenderer connectionRendererPrefab;
    [SerializeField]
    private RectTransform connectionsParent;
    [SerializeField]
    private GameObject topPanelRef;

    #endregion

    #region Propeties

    public RectTransform ScenariosContent { 
        get => scenariosContent; 
    }
    public ScenarioUIRow ScenarioRowPrefab {
        get => scenarioRowPrefab;
    }
    public UILineRenderer ConnectionRendererPrefab { 
        get => connectionRendererPrefab; 
    }
    public RectTransform ConnectionsParent { 
        get => connectionsParent; 
    }

    private MainMenuLvlSelectController Controller {
        get;
        set;
    }
    private MainMenuLvlSelectModel Model {
        get;
        set;
    }

    private List<ScenarioUIRow> SpawnedRowsCollection { 
        get; 
        set; 
    } = new List<ScenarioUIRow>();
    private List<UILineRenderer> SpawnedConnectionsCollection {
        get;
        set;
    } = new List<UILineRenderer>();
    private MEC.CoroutineHandle MainCoroutineHandle
    {
        get;
        set;
    }

    public GameObject TopPanelRef { get => topPanelRef; }

    #endregion

    #region Methods

    public void RefreshPanel()
    {
        RefreshScenariosList();
        
    }

    public void ClearPanel()
    {
        MEC.Timing.KillCoroutines(MainCoroutineHandle);
        SpawnedRowsCollection.ClearDestroy();
        SpawnedConnectionsCollection.ClearDestroy();
        StopAllCoroutines();
    }

    private void RefreshTopPanel()
    {
        StartCoroutine(_WaitAndRefreshObject());
    }

    private void RefreshScenariosList()
    {
        MainCoroutineHandle = MEC.Timing.RunCoroutine(_SpawnScenariosElements());
    }

    private IEnumerator<float> _SpawnScenariosElements()
    {
        List<ScenarioInfo> scenarios = Model.GetAllScenarios();
        for (int i = 0; i < scenarios.Count; i++)
        {
            ScenarioUIRow newRowElemenet = Instantiate(ScenarioRowPrefab);
            newRowElemenet.transform.ResetParent(ScenariosContent);
            newRowElemenet.SetInfo(scenarios[i], Controller);

            SpawnedRowsCollection.Add(newRowElemenet);

            yield return MEC.Timing.WaitForOneFrame;

            // Od 2 elementu.
            if (i > 0)
            {
                UILineRenderer newConnection = Instantiate(ConnectionRendererPrefab);
                newConnection.transform.ResetParent(ConnectionsParent);
                newConnection.SetPoint(0, SpawnedRowsCollection[i - 1].GetScenarioElementPosition());
                newConnection.SetPoint(1, SpawnedRowsCollection[i].GetScenarioElementPosition());
                newConnection.transform.localPosition = new Vector3(-newConnection.LineThickness * 0.5f, 0f);

                SpawnedConnectionsCollection.Add(newConnection);
            }
        }

        // Hack zeby top panel byl nad nazwami pozimow
        // Poniewaz poziomy sa spawnowane z opoznieniem canvas nie ogarnia na jakiej jest wartstwie.
        // Mozna to bylo rozwiazac maska....
        RefreshTopPanel();
    }

    private IEnumerator _WaitAndRefreshObject()
    {
        yield return new WaitForEndOfFrame();

        TopPanelRef.SetActive(false);
        TopPanelRef.SetActive(true);
    }

    private void Awake()
    {
        Controller = GetController<MainMenuLvlSelectController>();
        Model = GetModel<MainMenuLvlSelectModel>();
    }

    #endregion

    #region Enums



    #endregion
}
