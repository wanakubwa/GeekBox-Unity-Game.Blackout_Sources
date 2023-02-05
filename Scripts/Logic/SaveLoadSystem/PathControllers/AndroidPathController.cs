using System.IO;
using UnityEngine;

public class AndroidPathController : PathControllerBase
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    protected override string GetDataPath()
    {
        return Path.Combine(Application.persistentDataPath, SAVE_FOLDER);
    }

    #endregion

    #region Enums



    #endregion
}
