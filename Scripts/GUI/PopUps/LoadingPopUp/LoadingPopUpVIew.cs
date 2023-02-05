using UnityEngine;
using UnityEngine.UI;

public class LoadingPopUpVIew : PopUpView
{

    #region Fields

    [Space]
    [SerializeField]
    private Image fillBar;

    #endregion

    #region Propeties

    public Image FillBar {
        get => fillBar; 
    }

    #endregion

    #region Methods

    public override void ClosePopUp()
    {
        SetFillNormalized(1f);
        base.ClosePopUp();
    }

    public void SetFillNormalized(float fillValue)
    {
        if(FillBar != null)
        {
            FillBar.fillAmount = fillValue;
        }
    }

    #endregion

    #region Handlers



    #endregion
}
