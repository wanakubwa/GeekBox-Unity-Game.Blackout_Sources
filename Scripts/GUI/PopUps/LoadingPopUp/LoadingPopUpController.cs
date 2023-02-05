using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(LoadingPopUpModel), typeof(LoadingPopUpVIew))]
public class LoadingPopUpController : PopUpController
{

    #region Fields



    #endregion

    #region Propeties

    public LoadingPopUpModel PopUpModel
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        PopUpModel = GetModel<LoadingPopUpModel>();
    }

    public void InitializePopUp(int maxValue)
    {
        PopUpModel.InitializePopUp(maxValue);
    }

    public void RefreshPopUp(int progressValue)
    {
        PopUpModel.RefreshPopUp(progressValue);
    }

    #endregion

    #region Handlers



    #endregion
}
