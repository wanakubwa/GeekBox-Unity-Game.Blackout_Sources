using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MainMenuCenterModel : UIModel
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void SwitchScreenToLvlSelect()
    {
        MainMenuCarouselController.Instance.MakeTransitionToScreen(MainMenuCarouselController.ScreenType.LVL_SELECT_SCREEN);
    }

    public void SwitchScreenToUpgrades()
    {
        MainMenuCarouselController.Instance.MakeTransitionToScreen(MainMenuCarouselController.ScreenType.UPGRADE_SCREEN);
    }

    public void ShowSettingsPopUp()
    {
        PopUpManager.Instance.ShowSettingsPopUp();
    }

    public string GetCurrentAppVersionInfo()
    {
        return string.Format("v. {0}", BuildVersionSettings.Instance.BuildVersion);
    }

    #endregion

    #region Enums



    #endregion
}
