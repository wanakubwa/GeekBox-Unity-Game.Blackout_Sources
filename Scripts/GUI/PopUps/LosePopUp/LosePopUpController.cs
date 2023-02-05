using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LosePopUpModel), typeof(LosePopUpView))]
public class LosePopUpController : PopUpController
{
    #region Fields

    [SerializeField]
    private string infoPopBodyText = "Skip lvl for watch ADS?";

    #endregion

    #region Propeties

    private LosePopUpModel CurrentModel {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<LosePopUpModel>();

        PopUpManager.Instance.ShowOkCancelPopUp(infoPopBodyText, () => 
        {
            GeekBox.Ads.EasyMobileManager.Instance.ShowRewardedAD(() => 
            {
                GameEventsManager.Instance.SkipLvl();

                ScenarioInfo nextScenarioInfo = ScenariosManager.Instance.GetNextScenarioInfo();
                if (nextScenarioInfo != null)
                {
                    GameManager.Instance.LoadGameScene(nextScenarioInfo.ScenarioDirectoryName);
                }
                else
                {
                    GameManager.Instance.LoadMenuScene();
                }
            });
        });
    }

    public void RefreshPopUp(float lvlTimeMs)
    {
        CurrentModel.RefreshModel(lvlTimeMs);
        GetView<LosePopUpView>().RefreshView();
    }

    public void ExitButtonClick()
    {
        CurrentModel.LoadMainMenuScene();
        ClosePopUp();
    }

    public void RetryButtonClick()
    {
        CurrentModel.RestartCurrentLvl();
        ClosePopUp();
    }

    public void ShowScenarioSelectButtonClick()
    {
        //todo;
    }

    #endregion

    #region Enums



    #endregion
}
