using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(LoadScenarioEditorPopUpModel), typeof(LoadScenarioEditorPopUpView))]
public class LoadScenarioEditorPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void LoadScenario(TextMeshProUGUI textMesh)
    {
        GetModel<LoadScenarioEditorPopUpModel>().LoadEditorScenario(textMesh.text);
    }

    #endregion

    #region Enums



    #endregion
}
