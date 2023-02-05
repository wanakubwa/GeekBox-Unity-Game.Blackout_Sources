using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GeekBox.UI;
using System.Data;

public class LeftVerticalPanelView : UIView
{
    #region Fields

    [SerializeField]
    private List<ValueToggle> togglesCollection = new List<ValueToggle>();

    #endregion

    #region Propeties

    public List<ValueToggle> TogglesCollection { 
        get => togglesCollection; 
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        SelectToggleWithValue(GetDefaultToggleValue());
    }

    private void SelectToggleWithValue(float value)
    {
        foreach (ValueToggle toggle in TogglesCollection)
        {
            if(toggle.Value == value)
            {
                toggle.isOn = true;
            }
        }
    }

    private float GetDefaultToggleValue()
    {
        foreach (ValueToggle toggle in TogglesCollection)
        {
            if (toggle.isOn == true)
            {
                return toggle.Value;
            }
        }

        return 0.0f;
    }

    #endregion

    #region Enums



    #endregion
}
