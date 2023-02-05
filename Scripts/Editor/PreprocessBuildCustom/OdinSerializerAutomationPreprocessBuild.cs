using GeekBox.OdinSerializer.Editor;
using System;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

#if UNITY_EDITOR
public class OdinSerializerAutomationPreprocessBuild : IPreprocessBuildWithReport
{
    #region Fields

    public int callbackOrder => 3;

    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void OnPreprocessBuild(BuildReport report)
    {
        if (AOTSupportUtilities.ScanProjectForSerializedTypes(out List<Type> supportedTypes))
        {
            supportedTypes.Add(typeof(Color));
            supportedTypes.Add(typeof(Vector3));
            supportedTypes.Add(typeof(UnityEngine.Vector2));
            supportedTypes.Add(typeof(UnityEngine.Vector2Int));
            supportedTypes.Add(typeof(NodeModeType));
            supportedTypes.Add(typeof(ScenarioDataManager.RewardType));
            supportedTypes.Add(typeof(System.Collections.Generic.List<NodeModeType>));
            supportedTypes.Add(typeof(GeekBox.TechnologyTree.UpgradeType));


            AOTSupportUtilities.GenerateDLL(Application.dataPath + "/Plugins/", "GeekBox.OdinSerializer.GeneratedAOT", supportedTypes);
        }

        Debug.LogFormat("OdinSerializerAutomationPreprocessBuild... {0}".SetColor(Color.white), "[DONE]".SetColor(Color.green));
    }

    #endregion

    #region Enums



    #endregion
}
#endif