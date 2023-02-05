using UnityEngine;
using System.Collections;

public class LeftVerticalPanelModel : UIModel
{
    #region Fields

    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void SelectToggle(float value)
    {
        MapManager.Instance.SetChargePercentFactor(value);
    }

    public float GetCurrentChargeFactor()
    {
        return MapManager.Instance.ChargeSendPercentFactor;
    }

    #endregion

    #region Enums



    #endregion
}
