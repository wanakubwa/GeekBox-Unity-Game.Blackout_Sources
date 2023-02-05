using UnityEngine;
using System.Collections;

public class LoadScenarioEditorPopUpModel : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public string[] GetAvaibleEditorScenarios()
    {
        return ScenariosManager.Instance.GetScenariosEditorNames();
    }

    public void LoadEditorScenario(string scenarioName)
    {
        ScenariosManager.Instance.LoadEditorScenario(scenarioName);
    }

    #endregion

    #region Enums



    #endregion
}
