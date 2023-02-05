

namespace Blackout.GUI.LvlSelect
{
    public class ScenarioUIPassedState : ScenarioUIState
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public override void SetupIcon()
        {
            Context.OutlineImg.color = Context.OutlineAvailableColor;
            Context.StarsDisplayer.SetInfo(GetWallet().FinishedLvls.GetElementByID(Context.CachedInfo.ScenarioId).RewardStars);
            Context.MainImg.sprite = Context.AvailableLvlSprite;
        }

        public override bool TrySwicthState()
        {
            // Z tego stanu nie ma wyjscia.

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
