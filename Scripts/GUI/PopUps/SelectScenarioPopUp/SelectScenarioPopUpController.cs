using UnityEngine;
using System.Collections;
using Blackout.GUI.LvlSelect;

[RequireComponent(typeof(SelectScenarioPopUpView), typeof(SelectScenarioPopUpModel))]
public class SelectScenarioPopUpController : PopUpController, IScenarioSelectListener
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void OnScenarioSelected(ScenarioInfo scenario)
    {
        GetModel<SelectScenarioPopUpModel>().SelectOfficialScenario(scenario.ScenarioDirectoryName);
        ClosePopUp();
    }

    #endregion

    #region Enums



    #endregion
}
