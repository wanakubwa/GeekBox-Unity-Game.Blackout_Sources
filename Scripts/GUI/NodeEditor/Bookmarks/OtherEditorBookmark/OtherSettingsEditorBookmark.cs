using UnityEngine;
using System.Collections;

public class OtherSettingsEditorBookmark : ScenarioEditorBookmarkBase
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void SetCameraCenter()
    {
        CameraManager.Instance.SetCenterCamera();
        CameraManager.Instance.SetCameraZoom(30f);
    }

    #endregion

    #region Enums



    #endregion
}
