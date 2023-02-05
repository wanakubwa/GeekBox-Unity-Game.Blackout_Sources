using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CreditsPopUpModel), typeof(CreditsPopUpView))]
public class CreditsPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void SliderValueChanged(float value)
    {
        if(value <= 0f)
        {
            ClosePopUp();
        }
    }

    #endregion

    #region Enums



    #endregion
}
