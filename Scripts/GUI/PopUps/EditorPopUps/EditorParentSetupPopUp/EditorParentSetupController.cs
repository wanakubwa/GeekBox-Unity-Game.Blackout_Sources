using UnityEngine;

[RequireComponent(typeof(EditorParentSetupModel), typeof(EditorParentSetupView))]
public class EditorParentSetupController : ScenarioSetupBaseController<NodeParent>
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void RemoveMode()
    {
        GetModel<EditorParentSetupModel>().RemoveMode();
        GetView<EditorParentSetupView>().RefreshAvailableNodeModes();
    }

    public void AddMode()
    {
        GetModel<EditorParentSetupModel>().AddMode();
        GetView<EditorParentSetupView>().RefreshAvailableNodeModes();
    }

    #endregion

    #region Enums



    #endregion
}
