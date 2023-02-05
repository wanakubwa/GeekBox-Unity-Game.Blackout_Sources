using PlayerData;

namespace Blackout.GUI.LvlSelect
{
    public class ScenarioUIAvailableState : ScenarioUIState
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public override void SetupIcon()
        {
            Context.OutlineImg.color = Context.OutlineAvailableColor;
        }

        public override bool TrySwicthState()
        {
            PlayerWallet wallet = GetWallet();
            if(wallet.IsPlayerFinishLvl(Context.CachedInfo.ScenarioId) == true)
            {
                // Gracz przeszedl juz wczesniej dany poziom.
                Context.SetState(new ScenarioUIPassedState());
                return true;
            }
            else if(wallet.CurrentSavedLvlId == Context.CachedInfo.ScenarioId)
            {
                // Gracz ma zapisany ten poziom.
                Context.SetState(new ScenarioUISavedState());
                return true;
            }

            return false;
        }

        public override void OnSelectedScenario()
        {
            base.OnSelectedScenario();

            Context.Listener.OnScenarioSelected(Context.CachedInfo);
        }

        #endregion

        #region Enums



        #endregion
    }
}
