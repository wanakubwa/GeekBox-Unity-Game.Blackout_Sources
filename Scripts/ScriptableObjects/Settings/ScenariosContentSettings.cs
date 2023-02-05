using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;

[Serializable]
[CreateAssetMenu(fileName = "ScenariosContentSettings.asset", menuName = "Settings/ScenariosContentSettings")]
public class ScenariosContentSettings : ScriptableObject
{
    #region Fields

    private static ScenariosContentSettings instance;

    [SerializeField]
    private string scenariosEditorRootFolderName;
    [SerializeField]
    private string scenariosOfficialRootFolderName;
    [SerializeField]
    private string scenariosPackageName;

    [Header("File Info Settings")]
    [SerializeField]
    private FileInfoSettings scenarioFileInfoSettings = new FileInfoSettings();

    [Header("Scenarios Settings")]
    [SerializeField]
    private List<ScenarioInfo> scenariosCollection = new List<ScenarioInfo>();

    #endregion

    #region Propeties

    public static ScenariosContentSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<ScenariosContentSettings>("Settings/ScenariosContentSettings");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public string ScenariosEditorRootFolderName { 
        get => scenariosEditorRootFolderName;
    }

    public string ScenariosOfficialRootFolderName { 
        get => scenariosOfficialRootFolderName; 
    }

    public string ScenariosPackageName { 
        get => scenariosPackageName; 
    }

    public FileInfoSettings ScenarioFileInfoSettings { 
        get => scenarioFileInfoSettings; 
    }

    public List<ScenarioInfo> ScenariosCollection { 
        get => scenariosCollection; 
    }

    #endregion

    #region Methods

    public ScenarioInfo GetScenarioInfoByDirectory(string scenarioDirectoryName)
    {
        ScenarioInfo scenarioInfo = ScenariosCollection.Find(x => x.ScenarioDirectoryName == scenarioDirectoryName);
        if(scenarioInfo == null)
        {
            scenarioInfo = ScenarioInfo.GetDefault();
        }

        return new ScenarioInfo(scenarioInfo);
    }

    public ScenarioInfo GetScenarioInfoById(int id)
    {
        ScenarioInfo scenarioInfo = ScenariosCollection.GetElementByID(id);
        if (scenarioInfo == null)
        {
            scenarioInfo = ScenarioInfo.GetDefault();
        }

        return new ScenarioInfo(scenarioInfo);
    }

    public int GetScenarioIdByNo(int no)
    {
        ScenarioInfo info = ScenariosCollection.GetElementAtIndexSafe(no - 1);
        return info != null ? info.ScenarioId: Constants.DEFAULT_ID;
    }

#if UNITY_EDITOR

    [Button]
    public void RefreshScenarios()
    {
        string nameKey = string.Empty;
        string id = string.Empty;

        // to powinno po id lecieciec zeby mialo wiekszy sens. :(
        string[] scenariosNames = ScenarioFilePath.GetScenariosNames();
        for (int i = 0; i < scenariosNames.Length; i++)
        {
            ScenarioInfo info = ScenariosCollection.Find(x => x.ScenarioDirectoryName == scenariosNames[i]);

            if (info == null)
            {
                ScenarioFilePath.GetScenarioInfoFromFile(scenariosNames[i], out nameKey, out id);
                ScenariosCollection.Add(new ScenarioInfo(scenariosNames[i], nameKey, id.ParseToInt()));
            }
            else
            {
                ScenarioFilePath.GetScenarioInfoFromFile(scenariosNames[i], out nameKey, out id);
                info.SetId(id.ParseToInt());
                info.SetNameKey(nameKey);
            }
        }

        // Kasowanie scenariuszy, ktore nie istnieja.
        ScenariosCollection.RemoveAll(x => scenariosNames.Contains(x.ScenarioDirectoryName) == false);

        RefreshScenariodOrderNo();
    }

    [Button]
    public void SetEmptyScenariosId()
    {
        for (int i = 0; i < ScenariosCollection.Count; i++)
        {
            if(ScenariosCollection[i].IDEqual(Constants.DEFAULT_ID) == true)
            {
                int id = Guid.NewGuid().GetHashCode();
                ScenarioFilePath.CreateScenarioInfoFile(ScenariosCollection[i].ScenarioDirectoryName, ScenariosCollection[i].ScenarioNameKey, id.ToString());
                ScenariosCollection[i].SetId(id);

                Debug.LogFormat("ID scenariusza o katalogu: {0} ustawiono na: {1}", ScenariosCollection[i].ScenarioDirectoryName, id);
            }
        }

        RefreshScenarios();
    }

    [Button]
    public void SetAllScenariosId()
    {
        for (int i = 0; i < ScenariosCollection.Count; i++)
        {
            int id = Guid.NewGuid().GetHashCode();
            ScenarioFilePath.CreateScenarioInfoFile(ScenariosCollection[i].ScenarioDirectoryName, ScenariosCollection[i].ScenarioNameKey, id.ToString());
            ScenariosCollection[i].SetId(id);

            Debug.LogFormat("ID scenariusza o katalogu: {0} ustawiono na: {1}", ScenariosCollection[i].ScenarioDirectoryName, id);
        }

        RefreshScenarios();
    }

    [Button]
    public void ResetScenariosInfo()
    {
        ScenariosCollection.Clear();
        RefreshScenarios();
    }

    public void OnEnable()
    {
        RefreshScenarios();
    }

    public void OnValidate()
    {
        RefreshScenariodOrderNo();
    }

    private void RefreshScenariodOrderNo()
    {
        for (int i = 0; i < ScenariosCollection.Count; i++)
        {
            ScenariosCollection[i].SetOrderNo(i + 1);
        }
    }

#endif

    #endregion

    #region Enums

    [Serializable]
    public class FileInfoSettings
    {
        #region Fields

        [SerializeField]
        private string infoFileName;
        [SerializeField]
        private string infoFileExtension;
        //[SerializeField]
        //private string idFieldName;
        //[SerializeField]
        //private string nameKeyFieldName;

        #endregion

        #region Propeties

        public string InfoFileName { get => infoFileName; }
        public string InfoFileExtension { get => infoFileExtension; }
        //public string IdFieldName { get => idFieldName; }
        //public string NameKeyFieldName { get => nameKeyFieldName; }

        #endregion

        #region Methods



        #endregion

        #region Enums



        #endregion
    }

    #endregion
}
