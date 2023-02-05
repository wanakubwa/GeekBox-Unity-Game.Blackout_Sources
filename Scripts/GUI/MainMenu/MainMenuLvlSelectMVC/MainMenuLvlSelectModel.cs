using UnityEngine;
using System.Collections.Generic;

public class MainMenuLvlSelectModel : UIModel
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

    public void SelectOfficialScenario(string directoryName)
    {
        GameManager.Instance.LoadGameScene(directoryName);
    }

    #endregion

    #region Enums



    #endregion
}
