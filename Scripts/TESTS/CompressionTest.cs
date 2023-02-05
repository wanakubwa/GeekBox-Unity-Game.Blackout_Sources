using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class CompressionTest : MonoBehaviour
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    [Button]
    public void ExtractTGZArchive()
    {
        string targetCompressScenariosPath = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + ScenariosContentSettings.Instance.ScenariosPackageName + ".gz";
        ExtractScenariosArchive(targetCompressScenariosPath, ScenarioFilePath.GetScenariosRootPath() + "/TestUnzip");
    }

    [Button]
    public void CompressTGZArchive()
    {
        string dataDirectory = ScenarioFilePath.GetScenariosRootPath();
        string fileToCreate = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + ScenariosContentSettings.Instance.ScenariosPackageName + ".gz";

        if (File.Exists(fileToCreate) == true)
        {
            Debug.Log("Usuwanie starego pliku: " + fileToCreate);
            File.Delete(fileToCreate);
        }

        SharpZipFacade.CreateTarGZFromDirectory(fileToCreate, dataDirectory);
    }


    [Button]
    public void ExtractZIPArchive()
    {
        string targetCompressScenariosPath = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + ScenariosContentSettings.Instance.ScenariosPackageName + ".zip";
        ExtractScenariosArchive(targetCompressScenariosPath, ScenarioFilePath.GetScenariosRootPath() + "/TestUnzip");
    }

    [Button]
    public void CompressZIPArchive()
    {
        string dataDirectory = ScenarioFilePath.GetScenariosRootPath();
        string fileToCreate = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + ScenariosContentSettings.Instance.ScenariosPackageName + ".zip";

        if (File.Exists(fileToCreate) == true)
        {
            Debug.Log("Usuwanie starego pliku: " + fileToCreate);
            File.Delete(fileToCreate);
        }

        SharpZipFacade.CreateZipArchiveFromDirectory(fileToCreate, dataDirectory);
    }

    private static void ExtractScenariosArchive(string compressedDataPath, string extractPath)
    {
        //copy tgz to directory where we can extract it
        string copiedFilePath = Path.Combine(extractPath, "tmp.zip");

        UnityWebRequest loadingRequest = UnityWebRequest.Get(compressedDataPath);
        loadingRequest.SendWebRequest();
        while (!loadingRequest.isDone)
        {
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
            {
                break;
            }
        }
        if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
        {
            Debug.Log("Loading request error for: " + compressedDataPath);
        }
        else
        {
            File.WriteAllBytes(copiedFilePath, loadingRequest.downloadHandler.data);
            Debug.LogFormat("Przekopiowanie danych pod sciezke: {0}", copiedFilePath);
        }

        //extract it
        SharpZipFacade.ExtractZipFile(copiedFilePath, extractPath);
        //delete tgz
        File.Delete(copiedFilePath);
    }

    #endregion

    #region Enums



    #endregion
}
