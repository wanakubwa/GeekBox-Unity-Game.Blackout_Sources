using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShieldsVisualization : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI shieldsAmmountText;

    [Space]
    [SerializeField]
    private Image shieldImage;
    [SerializeField]
    private SpriteRenderer shieldOutline;

    #endregion

    #region Propeties

    public TextMeshProUGUI ShieldsAmmountText { 
        get => shieldsAmmountText; 
    }

    public Image ShieldImage { 
        get => shieldImage;
    }

    public SpriteRenderer ShieldOutline { 
        get => shieldOutline;
    }

    #endregion

    #region Methods

    public void SetShieldsAmmount(int value)
    {
        ShieldsAmmountText.text = value.ToString();
    }

    public void SetVisualizationColor(Color shieldColor)
    {
        ShieldImage.color = shieldColor;
        ShieldOutline.color = shieldColor;
    }

    #endregion

    #region Enums



    #endregion
}
