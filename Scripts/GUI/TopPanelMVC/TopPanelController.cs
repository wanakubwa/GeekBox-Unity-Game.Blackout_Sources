using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TopPanelUIModel), typeof(TopPanelUIView))]
public class TopPanelController : UIController
{

    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void ShowGameMenuPopUp()
    {
        GetModel<TopPanelUIModel>().ShowGameMenuPopUp();
    }

    #endregion

    #region Enums



    #endregion
}
