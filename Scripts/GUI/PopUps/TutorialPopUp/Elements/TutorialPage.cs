using TutorialSystem;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPage : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Image infoImage;
    [SerializeField]
    private LocalizedTMPro dscLocalizedText;

    #endregion

    #region Propeties

    public Image InfoImage { get => infoImage; }
    public LocalizedTMPro DscLocalizedText { get => dscLocalizedText; }

    #endregion

    #region Methods

    public void SetInfo(TutorialElement element)
    {
        InfoImage.sprite = Sprite.Create(element.ImgTexture, new Rect(0.0f, 0.0f, element.ImgTexture.width, element.ImgTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        DscLocalizedText.SetKey(element.DscKey);
    }

    #endregion

    #region Enums



    #endregion
}
