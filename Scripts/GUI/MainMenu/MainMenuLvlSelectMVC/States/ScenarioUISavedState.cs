

using PlayerData;

namespace Blackout.GUI.LvlSelect
{
    public class ScenarioUISavedState : ScenarioUIState
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public override void SetupIcon()
        {
            Context.OutlineImg.color = Context.OutlineSavedColor;
            Context.MainImg.sprite = Context.SavedLvlSprite;
        }

        public override bool TrySwicthState()
        {
            // Z tego stanu nie ma wyjscia.

            return false;
        }

        public override void OnSelectedScenario()
        {
            PopUpManager.Instance.ShowOkCancelPopUp(Constants.LOC_GUI_LOAD_PROGRESS_INFO.Localize(), () => {
                GameManager.Instance.LoadGame();
            },
                OnRejectLoadProgress
            );
        }

        private void OnRejectLoadProgress()
        {
            Context.Listener.OnScenarioSelected(Context.CachedInfo);
        }

        #endregion

        #region Enums



        #endregion
    }
}
