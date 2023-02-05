using UnityEngine;

public class MainMenuTopPanelBase : MonoBehaviour
{
    #region Fields

    

    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void ReturnToCenterScreen()
    {
        MainMenuCarouselController.Instance.MakeTransitionToScreen(MainMenuCarouselController.ScreenType.MAIN_SCREEN);
    }

    protected virtual void RefreshPanel() { }
    protected virtual void AttachEvents() { }
    protected virtual void DetachEvents() { }

    private void OnEnable()
    {
        RefreshPanel();
        AttachEvents();
    }

    private void OnDisable()
    {
        DetachEvents();
    }

    #endregion

    #region Enums



    #endregion
}
