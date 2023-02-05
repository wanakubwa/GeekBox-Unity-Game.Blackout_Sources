using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsPopUpView : PopUpView
{
    #region Fields

    [Space]
    [SerializeField]
    private Scrollbar creditsScrollbar;
    [SerializeField]
    private float startValue;

    #endregion

    #region Propeties

    public Scrollbar CreditsScrollbar { get => creditsScrollbar; }
    public float StartValue { get => startValue; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        SetScrollBarValue(StartValue);
    }

    public void SetScrollBarValue(float value)
    {
        CreditsScrollbar.value = value;
    }

    #endregion

    #region Enums



    #endregion
}
