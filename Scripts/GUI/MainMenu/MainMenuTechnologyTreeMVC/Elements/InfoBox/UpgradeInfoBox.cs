using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class UpgradeInfoBox : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI detailedDescriptionText;
    [SerializeField]
    private TextMeshProUGUI bottomInfoText;
    [SerializeField]
    private AutoTMPScroll descriptionScroller;

    [Title("Images")]
    [SerializeField]
    private Image icon;

    [Title("Sliders")]
    [SerializeField]
    private Image buyProgressSlider;

    #endregion

    #region Propeties

    private TextMeshProUGUI TitleText { get => titleText; }
    private TextMeshProUGUI DetailedDescriptionText { get => detailedDescriptionText; }
    private TextMeshProUGUI BottomInfoText { get => bottomInfoText; }
    private Image Icon { get => icon; }
    public Image BuyProgressSlider { get => buyProgressSlider; }
    public AutoTMPScroll DescriptionScroller { get => descriptionScroller; }

    #endregion

    #region Methods

    public void SetInfo(Sprite icon, Color iconColor, string titleText, string detailedDescriptionText, string bottomInfoText)
    {
        Icon.sprite = icon;
        Icon.color = iconColor;
        TitleText.SetText(titleText);
        DetailedDescriptionText.SetText(detailedDescriptionText);
        BottomInfoText.SetText(bottomInfoText);
        BottomInfoText.gameObject.SetActive(true);
        BuyProgressSlider.gameObject.SetActive(false);
        DescriptionScroller.RefreshTextScroll();
    }

    public void SetProgress(float normalizedValue)
    {
        BuyProgressSlider.fillAmount = normalizedValue;
    }

    public void SetBuyStatus(bool isBuy)
    {
        BottomInfoText.gameObject.SetActive(!isBuy);
        BuyProgressSlider.gameObject.SetActive(isBuy);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Enums



    #endregion
}
