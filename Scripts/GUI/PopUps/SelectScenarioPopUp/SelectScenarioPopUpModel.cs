using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectScenarioPopUpModel : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public List<ScenarioInfo> GetAllScenarios()
    {
        return ScenariosContentSettings.Instance.ScenariosCollection;
    }

    public void SelectOfficialScenario(string name)
    {
        GameManager.Instance.LoadGameScene(name);
    }

    #endregion

    #region Enums



    #endregion
}
