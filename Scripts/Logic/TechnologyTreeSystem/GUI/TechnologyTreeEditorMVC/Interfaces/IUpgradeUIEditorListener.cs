using UnityEngine;

public interface IUpgradeUIEditorListener
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    void OnDeleteUpgrade(UpgradeUIEditorElement sender);
    void OnClickCreateNextConnection(UpgradeUIEditorElement sender);
    void OnClickPrevConnection(UpgradeUIEditorElement sender);
    void OnUpgradeElementDrag(UpgradeUIEditorElement sender, Vector2 screenPoint);

    #endregion

    #region Enums



    #endregion
}
