using UnityEngine;
using TMPro;
using System;

namespace Blackout.GUI.LvlSelect
{
    public class MainMenuScenarioElement : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private LocalizedTMPro scenarioName;

        #endregion

        #region Propeties

        public LocalizedTMPro ScenarioName { get => scenarioName; }

        private ScenarioInfo CachedInfo
        {
            get;
            set;
        }

        private IScenarioSelectListener Listener {
            get;
            set;
        }

        #endregion

        #region Methods

        public void SetInfo(ScenarioInfo scenarioInfo, IScenarioSelectListener listener)
        {
            CachedInfo = scenarioInfo;
            ScenarioName.SetKey(scenarioInfo.ScenarioNameKey);
            Listener = listener;
        }

        public void SelectedScenario()
        {
            Listener.OnScenarioSelected(CachedInfo);
        }

        #endregion

        #region Enums



        #endregion
    }
}
