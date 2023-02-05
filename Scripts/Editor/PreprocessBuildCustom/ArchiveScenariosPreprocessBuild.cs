using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class ArchiveScenariosPreprocessBuild : IPreprocessBuildWithReport
{
    #region Fields

    private const string FILE_EXTENSION = ".zip";

    #endregion

    #region Propeties

    public int callbackOrder { get { return 0; } }

    #endregion

    #region Methods

    public void OnPreprocessBuild(BuildReport report)
    {
        PackScenariosToStreamingAssets();
        Debug.LogFormat("ArchiveScenariosPreprocessBuild... {0}".SetColor(Color.white), "[DONE]".SetColor(Color.green));
    }

    private static void PackScenariosToStreamingAssets()
    {
        if (Directory.Exists(Application.streamingAssetsPath) == false)
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        string dataDirectory = ScenarioFilePath.GetScenariosRootPath();
        string fileToCreate = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + ScenariosContentSettings.Instance.ScenariosPackageName + FILE_EXTENSION;

        if (File.Exists(fileToCreate) == true)
        {
            Debug.Log("Usuwanie starego pliku: " + fileToCreate);
            File.Delete(fileToCreate);
        }

        SharpZipFacade.CreateZipArchiveFromDirectory(fileToCreate, dataDirectory);
    }

    #endregion

    #region Enums



    #endregion

}
