using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class RefreshScenariosSettingsPreprocessBuild : IPreprocessBuildWithReport
{
    #region Fields



    #endregion

    #region Propeties

    public int callbackOrder { get { return 2; } }

    #endregion

    #region Methods

    public void OnPreprocessBuild(BuildReport report)
    {
        ScenariosContentSettings.Instance.RefreshScenarios();
        Debug.LogFormat("{0}... {1}".SetColor(Color.white), this.GetType(), "[DONE]".SetColor(Color.green));
    }

    #endregion

    #region Enums



    #endregion
}
