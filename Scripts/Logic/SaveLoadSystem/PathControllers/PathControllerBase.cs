using System.IO;
using UnityEngine;

public class PathControllerBase
{
    #region Fields

    public const string SAVE_FOLDER = "GameSave";

    #endregion

    #region Propeties

    public string DataSavePath {
        get;
        set;
    }

    #endregion

    #region Methods

    protected virtual string GetDataPath()
    {
        return Path.Combine(Directory.GetParent(Application.dataPath).FullName, SAVE_FOLDER);
    }

    public void Init()
    {
        DataSavePath = GetDataPath();
        TryCreateDataPath();
    }

    public void ResetDataPathContent()
    {
        if (Directory.Exists(DataSavePath) == false)
        {
            return;
        }

        Directory.Delete(DataSavePath, true);
        TryCreateDataPath();
    }

    private void TryCreateDataPath()
    {
        if (Directory.Exists(DataSavePath) == false)
        {
            Directory.CreateDirectory(DataSavePath);
        }
    }

    #endregion

    #region Enums



    #endregion
}
