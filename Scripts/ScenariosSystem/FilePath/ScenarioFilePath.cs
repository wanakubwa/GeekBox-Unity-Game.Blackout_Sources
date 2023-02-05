using GeekBox.OdinSerializer.Utilities;
using System.IO;
using UnityEngine;

public class ScenarioFilePath
{
    #region Fields

    public const string PLAYER_SCENARIO_SAVE_FOLDER = "ScenarioSave";

    #endregion

    #region Propeties

    public static string ScenarioSaveFolderPath {
        get;
        private set;
    }

    #endregion

    #region Methods

    public static string GetTargetScenarioPath(string directoryName)
    {
        return GetScenariosRootPath() + Path.AltDirectorySeparatorChar + directoryName;
    }

    public static string GetScenarioPlayerSavePath()
    {
        if(ScenarioSaveFolderPath.IsNullOrWhitespace() == true)
        {
            ScenarioSaveFolderPath = SaveLoadManager.Instance.GetSavePathSubfolder(PLAYER_SCENARIO_SAVE_FOLDER);
        }
        
        return ScenarioSaveFolderPath;
    }

    public static string GetScenariosRootPath()
    {
        string path = string.Empty;

#if UNITY_ANDROID && !UNITY_EDITOR

        path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + ScenariosContentSettings.Instance.ScenariosOfficialRootFolderName;
#else
        string dataPath = Directory.GetParent(Application.dataPath).FullName;
        path = Path.Combine(dataPath, ScenariosContentSettings.Instance.ScenariosEditorRootFolderName);
#endif
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        return path;
    }

    public static string[] GetScenariosNames()
    {
        string scenariosAbsolutePath = GetScenariosRootPath();
        string[] directoriesNames = Directory.GetDirectories(scenariosAbsolutePath);

        string[] scenariosNames = new string[directoriesNames.Length];
        for (int i = 0; i < directoriesNames.Length; i++)
        {
            scenariosNames[i] = Path.GetFileName(directoriesNames[i]);
        }

        return scenariosNames;
    }

    public string GetScenarioPackagePath()
    {
        return Application.streamingAssetsPath;
    }

    public static void ClearScenarioPlayerSave()
    {
        string path = GetScenarioPlayerSavePath();
        Directory.Delete(path, true);
        Directory.CreateDirectory(path);
    }

#if UNITY_EDITOR

    /// <summary>
    /// Do wykorzystania jedynie w edytorze!
    /// </summary>
    public static void CreateScenarioInfoFile(string scenarioDirectoryName, string scenarioNameKey, string scenarioId)
    {
        ScenariosContentSettings settings = ScenariosContentSettings.Instance;

        string filePath = Path.Combine(GetTargetScenarioPath(scenarioDirectoryName), settings.ScenarioFileInfoSettings.InfoFileName + settings.ScenarioFileInfoSettings.InfoFileExtension);

        // Create a file to write to.
        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.WriteLine(scenarioNameKey);
            sw.WriteLine(scenarioId);
        }
    }

    /// <summary>
    /// Do wykorzystania jedynie w edytorze!
    /// </summary>
    public static void GetScenarioInfoFromFile(string scenarioDirectoryName, out string scenarioNameKey, out string scenarioId)
    {
        ScenariosContentSettings settings = ScenariosContentSettings.Instance;

        string filePath = Path.Combine(GetTargetScenarioPath(scenarioDirectoryName), settings.ScenarioFileInfoSettings.InfoFileName + settings.ScenarioFileInfoSettings.InfoFileExtension);

        if(File.Exists(filePath) == false)
        {
            scenarioNameKey = Constants.LOC_DEFAULT_KEY;
            scenarioId = Constants.DEFAULT_ID.ToString();
            Debug.LogErrorFormat("No scenario info file for scenario directory: '{0}' \n Set default data!", scenarioDirectoryName);
            return;
        }

        string[] data = File.ReadAllLines(filePath);
        scenarioNameKey = data[0];
        scenarioId = data[1];
    }

#endif

    #endregion

    #region Enums



    #endregion
}
