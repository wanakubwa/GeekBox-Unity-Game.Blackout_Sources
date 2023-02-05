using UnityEngine;
using System.Collections.Generic;
using Blackout.GUI.LvlSelect;

public class SelectScenarioPopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private RectTransform scenariosContent;
    [SerializeField]
    private MainMenuScenarioElement scenarioElementPrefab;

    #endregion

    #region Propeties

    public RectTransform ScenariosContent { get => scenariosContent; }
    public MainMenuScenarioElement ScenarioElementPrefab { get => scenarioElementPrefab; }

    private SelectScenarioPopUpController Controller
    {
        get;
        set;
    }

    private SelectScenarioPopUpModel Model
    {
        get;
        set;
    }


    private List<MainMenuScenarioElement> SpawnedScenariosCollection { get; set; } = new List<MainMenuScenarioElement>();
    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        Controller = GetController<SelectScenarioPopUpController>();
        Model = GetModel<SelectScenarioPopUpModel>();

        RefreshScenariosList();
    }

    public void RefreshScenariosList()
    {
        SpawnedScenariosCollection.ClearDestroy();

        List<ScenarioInfo> scenarios = Model.GetAllScenarios();
        for (int i = 0; i < scenarios.Count; i++)
        {
            MainMenuScenarioElement newScenarioElemenet = Instantiate(ScenarioElementPrefab);
            newScenarioElemenet.transform.ResetParent(ScenariosContent);
            newScenarioElemenet.SetInfo(scenarios[i], Controller);

            SpawnedScenariosCollection.Add(newScenarioElemenet);
        }
    }

    #endregion

    #region Enums



    #endregion
}
