using System;
using UnityEngine;

[RequireComponent(typeof(InfoOkCancelPopUpView), typeof(InfoOKCancelPopUpModel))]
public class InfoOKCancelPopUpController : PopUpController
{

    #region Fields



    #endregion

    #region Propeties

    public InfoOKCancelPopUpModel PopUpModel
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        PopUpModel = GetModel<InfoOKCancelPopUpModel>();
    }

    public void RefreshPopUp(string contentText, Action onAcceptHandler, Action onCancelHandler)
    {
        PopUpModel.RefreshPopUp(contentText, onAcceptHandler, onCancelHandler);
    }

    public void OnAccept()
    {
        PopUpModel.OnAccept();
        ClosePopUp();
    }

    public void OnCancel()
    {
        PopUpModel.OnCancel();
        ClosePopUp();
    }

    #endregion

    #region Handlers



    #endregion
}
