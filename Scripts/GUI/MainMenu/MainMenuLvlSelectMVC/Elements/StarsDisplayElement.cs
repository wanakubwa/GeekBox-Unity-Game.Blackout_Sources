using UnityEngine;
using UnityEngine.UI;

public class StarsDisplayElement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Image firstStar;
    [SerializeField]
    private Image secondStar;
    [SerializeField]
    private Image thirdStar;

    [Space]
    [SerializeField]
    private Sprite availableStarSprite;
    [SerializeField]
    private Sprite lockedStarSprite;

    #endregion

    #region Propeties

    public Image FirstStar { get => firstStar; }
    public Image SecondStar { get => secondStar; }
    public Image ThirdStar { get => thirdStar; }
    public Sprite AvailableStarSprite { get => availableStarSprite; }
    public Sprite LockedStarSprite { get => lockedStarSprite; }

    #endregion

    #region Methods

    public void SetInfo(ScenarioDataManager.RewardType rewardType)
    {
        gameObject.SetActive(true);

        switch (rewardType)
        {
            case ScenarioDataManager.RewardType.ONE_STAR:
                FirstStar.sprite = AvailableStarSprite;
                break;
            case ScenarioDataManager.RewardType.TWO_STARS:
                FirstStar.sprite = AvailableStarSprite;
                SecondStar.sprite = AvailableStarSprite;
                break;
            case ScenarioDataManager.RewardType.THREE_STARS:
                FirstStar.sprite = AvailableStarSprite;
                SecondStar.sprite = AvailableStarSprite;
                ThirdStar.sprite = AvailableStarSprite;
                break;
        }
    }

    #endregion

    #region Enums



    #endregion
}
