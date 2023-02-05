using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ParentSettings
{
    #region Fields

    [SerializeField]
    private Color mainColor = Color.cyan;
    [SerializeField]
    private Color fontColor = Color.black;
    [SerializeField]
    private Color shieldColor = Color.blue;

    #endregion

    #region Propeties

    public Color MainColor { get => mainColor; private set => mainColor = value; }
    public Color FontColor { get => fontColor; private set => fontColor = value; }
    public Color ShieldColor { get => shieldColor; private set => shieldColor = value; }

    #endregion

    #region Methods

    public void SetMainColor(Color color)
    {
        MainColor = color;
    }

    public void SetFontColor(Color color)
    {
        FontColor = color;
    }

    public void SetShieldsColor(Color color)
    {
        ShieldColor = color;
    }

    #endregion

    #region Enums



    #endregion
}
