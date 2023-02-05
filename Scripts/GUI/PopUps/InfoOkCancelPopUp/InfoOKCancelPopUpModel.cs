using System;

public class InfoOKCancelPopUpModel : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties

    public InfoOkCancelPopUpView PopUpView
    {
        get;
        private set;
    }

    public Action OnAcceptCallback
    {
        get;
        private set;
    } = delegate { };

    public Action OnCancelCallback
    {
        get;
        private set;
    } = delegate { };

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        PopUpView = GetView<InfoOkCancelPopUpView>();
    }

    public void RefreshPopUp(string text, Action onAcceptCallback, Action onCancelCallback)
    {
        OnAcceptCallback = onAcceptCallback;
        OnCancelCallback = onCancelCallback;
        PopUpView.SetMainText(text);
    }

    public void OnAccept()
    {
        OnAcceptCallback();
    }

    public void OnCancel()
    {
        OnCancelCallback();
    }

    #endregion

    #region Handlers



    #endregion
}
