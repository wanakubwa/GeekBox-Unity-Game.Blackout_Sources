using UnityEngine;
using System.Collections;
using GeekBox.UI;

[RequireComponent(typeof(LeftVerticalPanelModel), typeof(LeftVerticalPanelView))]
public class LeftVerticalPanelController : UIController
{
    #region Fields



    #endregion

    #region Propeties

    private LeftVerticalPanelModel CurrentModel
    {
        get; 
        set;
    }

    private LeftVerticalPanelView CurrentView
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<LeftVerticalPanelModel>();
    }

    public void OnToggleSelected(ValueToggle sender)
    {
        if(CurrentModel != null)
        {
            CurrentModel.SelectToggle(sender.Value);
        }
    }

    #endregion

    #region Enums



    #endregion
}
