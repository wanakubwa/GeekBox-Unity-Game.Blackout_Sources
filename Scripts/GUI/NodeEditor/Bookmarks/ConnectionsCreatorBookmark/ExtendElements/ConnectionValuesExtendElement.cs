using System;
using TMPro;
using UnityEngine;

public class ConnectionValuesExtendElement : ConnectionExtendElementBase
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI infoText;

    #endregion

    #region Propeties

    public TextMeshProUGUI InfoText { 
        get => infoText; 
        set => infoText = value;
    }

    #endregion

    #region Methods

    public override void Initialize(MapConnection connection, Action<MapConnection> onButtonClickCallback)
    {
        base.Initialize(connection, onButtonClickCallback);

        InfoText.text = string.Format("SPEED \n {0})", connection.Values.Speed.ToString());
    }

    #endregion

    #region Enums



    #endregion
}
