using UnityEngine;

public class InfoOkCancelPopUpView : PopUpView
{

    #region Fields

    [SerializeField]
    private LocalizedTMPro mainText;

    #endregion

    #region Propeties

    public LocalizedTMPro MainText { 
        get => mainText; 
    }

    #endregion

    #region Methods

    public void SetMainText(string keyText)
    {
        MainText.SetKey(keyText);
    }

    #endregion

    #region Handlers



    #endregion
}
