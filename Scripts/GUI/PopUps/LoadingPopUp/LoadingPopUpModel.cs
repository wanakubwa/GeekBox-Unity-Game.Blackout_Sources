using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LoadingPopUpModel : PopUpModel
{

    #region Fields



    #endregion

    #region Propeties

    public LoadingPopUpVIew PopUpVIew
    {
        get;
        private set;
    }

    public float MaxValue
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        PopUpVIew = GetView<LoadingPopUpVIew>();
        SetStartParameters();
    }

    public void InitializePopUp(float maxValue)
    {
        MaxValue = maxValue;
    }

    public void RefreshPopUp(float progresValue)
    {
        float fillValue = progresValue / MaxValue;
        PopUpVIew.SetFillNormalized(fillValue);
    }

    private void SetStartParameters()
    {
        PopUpVIew.SetFillNormalized(0f);
    }

    #endregion

    #region Handlers



    #endregion
}
