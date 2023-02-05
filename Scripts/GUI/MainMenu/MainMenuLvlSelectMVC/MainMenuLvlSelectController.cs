using UnityEngine;
using System.Collections;
using Blackout.GUI.LvlSelect;

[RequireComponent(typeof(MainMenuLvlSelectModel), typeof(MainMenuLvlSelectView))]
public class MainMenuLvlSelectController : CarouselScreenController, IScenarioSelectListener
{
    #region Fields



    #endregion

    #region Propeties

    private MainMenuLvlSelectModel CurrentModel {
        get;
        set;
    }

    private MainMenuLvlSelectView CurrentView {
        get;
        set;
    }

    #endregion

    #region Methods

    public void OnScenarioSelected(ScenarioInfo scenario)
    {
        CurrentModel.SelectOfficialScenario(scenario.ScenarioDirectoryName);
    }

    public override void FocusScreen()
    {
        base.FocusScreen();

        CurrentView.RefreshPanel();
    }

    public override void UnFocusScreen()
    {
        base.UnFocusScreen();

        CurrentView.ClearPanel();
    }

    private void Awake()
    {
        CurrentModel = GetModel<MainMenuLvlSelectModel>();
        CurrentView = GetView<MainMenuLvlSelectView>();
    }

    #endregion

    #region Enums



    #endregion
}
