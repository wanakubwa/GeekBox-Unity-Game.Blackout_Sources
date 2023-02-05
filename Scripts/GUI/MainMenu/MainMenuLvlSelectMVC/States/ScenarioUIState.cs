using PlayerData;
using UnityEngine;

namespace Blackout.GUI.LvlSelect
{
    public abstract class ScenarioUIState
    {
        #region Fields



        #endregion

        #region Propeties

        protected ScenarioUIElement Context
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public abstract bool TrySwicthState();
        public abstract void SetupIcon();

        public virtual void OnSelectedScenario() { }

        public void SetContext(ScenarioUIElement context)
        {
            Context = context;
            if (TrySwicthState() == false)
            {
                RefreshIcon();
            }
        }

        public PlayerWallet GetWallet()
        {
            return PlayerManager.Instance.Wallet;
        }

        private void RefreshIcon()
        {
            SetDefaultIcon();
            SetupIcon();
        }

        private void SetDefaultIcon()
        {
            // Podstawowe ustawinie ikony jezeli jest taka potrzeba.
            Context.LockerIconObj.SetActive(false);
            Context.LvlIndexText.gameObject.SetActive(true);
            Context.StarsDisplayer.gameObject.SetActive(false);
            Context.ScenarioName.SetText(Context.CachedInfo.ScenarioNameKey.Localize());
            Context.ScenarioName.color = Context.ScenarioNameColor;
        }

        #endregion

        #region Enums



        #endregion
    }
}
