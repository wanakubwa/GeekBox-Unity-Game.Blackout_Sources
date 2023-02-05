using UnityEngine;

public class CarouselScreen : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private RectTransform rectTransform;

    [Header("Screen Controllers")]
    [SerializeField]
    private CarouselScreenController[] screenControllers;

    #endregion

    #region Propeties

    public RectTransform RectTransform { get => rectTransform; }
    public CarouselScreenController[] ScreenControllers { get => screenControllers; }

    #endregion

    #region Methods

    public void Initialize(Vector3 newCenterPosition, bool isActive)
    {
        transform.position = newCenterPosition;
        gameObject.SetActive(isActive);
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
        for(int i =0; i < ScreenControllers.Length; i++)
        {
            ScreenControllers[i].FocusScreen();
        }
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
        for (int i = 0; i < ScreenControllers.Length; i++)
        {
            ScreenControllers[i].UnFocusScreen();
        }
    }

    #endregion

    #region Enums



    #endregion
}
