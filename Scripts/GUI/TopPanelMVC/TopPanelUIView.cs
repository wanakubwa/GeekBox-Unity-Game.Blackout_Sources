using UnityEngine;
using System.Collections;
using TMPro;

public class TopPanelUIView : UIView
{

    #region Fields

    [SerializeField]
    private TextMeshProUGUI timeCounterText;

    #endregion

    #region Propeties

    public TextMeshProUGUI TimeCounterText { 
        get => timeCounterText;
    }

    private TopPanelUIModel CurrentModel
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<TopPanelUIModel>();
    }

    private void SetTimeCounterText(string text)
    {
        TimeCounterText.text = text;
    }

    private void Update()
    {
        SetTimeCounterText(CurrentModel.GetFormattedTime());
    }

    #endregion

    #region Enums



    #endregion
}
