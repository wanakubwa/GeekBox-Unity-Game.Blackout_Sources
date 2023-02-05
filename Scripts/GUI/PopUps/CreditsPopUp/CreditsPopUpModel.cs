using UnityEngine;
using System.Collections;

public class CreditsPopUpModel : PopUpModel
{
    #region Fields

    [Space]
    [SerializeField]
    private float durationTimeS;

    #endregion

    #region Propeties

    public float DurationTimeS { 
        get => durationTimeS; 
        private set => durationTimeS = value; 
    }

    private CreditsPopUpView View
    {
        get;
        set;
    }

    private float StepValue
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        View = GetView<CreditsPopUpView>();
        StepValue = CalculateStepValue(1);
    }

    private float CalculateStepValue(float sliderMaxValue)
    {
       return sliderMaxValue / DurationTimeS;
    }

    private void Update()
    {
        View.SetScrollBarValue(View.CreditsScrollbar.value - (Time.deltaTime * StepValue));
    }

    #endregion

    #region Handlers



    #endregion
}
