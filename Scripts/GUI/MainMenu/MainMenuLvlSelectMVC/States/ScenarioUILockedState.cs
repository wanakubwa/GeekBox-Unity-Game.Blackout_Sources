

using PlayerData;

namespace Blackout.GUI.LvlSelect
{
    public class ScenarioUILockedState : ScenarioUIState
    {
        #region Fields

        private const int FIRST_SCENARIO_NO = 1;
        private const string LOCKED_LVL_NAME = "???";

        #endregion

        #region Propeties



        #endregion

        #region Methods

        public override void SetupIcon()
        {
            Context.LockerIconObj.SetActive(true);
            Context.LvlIndexText.gameObject.SetActive(false);
            Context.OutlineImg.color = Context.OutlineLockedColor;
            Context.ScenarioName.SetText(LOCKED_LVL_NAME);
            Context.ScenarioName.color = Context.OutlineLockedColor;
            Context.MainImg.sprite = Context.LockedLvlSprite;
        }

        public override bool TrySwicthState()
        {
            ScenariosContentSettings settings = ScenariosContentSettings.Instance;
            PlayerWallet wallet = GetWallet();
            
            if(Context.CachedInfo.OrderNo == FIRST_SCENARIO_NO || wallet.IsPlayerFinishLvl(settings.GetScenarioIdByNo(Context.CachedInfo.OrderNo - 1)) == true)
            {
                Context.SetState(new ScenarioUIAvailableState());
                return true;
            }

            return false;
        }

        #endregion

        #region Enums



        #endregion
    }
}
