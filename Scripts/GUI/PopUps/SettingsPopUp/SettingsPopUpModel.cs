using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SettingsPopUpModel : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties


    #endregion

    #region Methods

    public void ResetGamePlayerProgress()
    {
        PopUpManager.Instance.ShowOkCancelPopUp(Constants.LOC_RESET_INFO, 
            () => {
            GameManager.Instance.ResetGame();
        });
    }

    public bool IsSoundsOnForPlayer()
    {
        return true;
    }

    public void SetSoundSettings(bool isOn)
    {
        //todo;
    }

    public void ChangeLanguage(int selectedIndex)
    {
        string[] laguagesEnum = Enum.GetNames(typeof(Language));
        string selectedLanguage = laguagesEnum[selectedIndex];
        LanguageManager.Instance.SetLanguage((Language)Enum.Parse(typeof(Language), selectedLanguage));
    }

    #endregion

    #region Enums



    #endregion
}
