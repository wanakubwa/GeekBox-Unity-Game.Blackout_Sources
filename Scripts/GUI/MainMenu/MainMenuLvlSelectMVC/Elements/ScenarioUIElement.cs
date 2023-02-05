using UnityEngine;
using System.Collections;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Blackout.GUI.LvlSelect
{
    public class ScenarioUIElement : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private LocalizedTMPro scenarioName;
        [SerializeField]
        private TextMeshProUGUI lvlIndexText;

        [Title("States Settings")]
        [SerializeField]
        private GameObject lockerIconObj;
        [SerializeField]
        private StarsDisplayElement starsDisplayer;
        [SerializeField]
        private Image outlineImg;
        [SerializeField]
        private Image mainImg;
        [SerializeField]
        private Color outlineAvailableColor;
        [SerializeField]
        private Color outlineLockedColor;
        [SerializeField]
        private Color outlineSavedColor;
        [SerializeField]
        private Color scenarioNameColor;

        [Title("States Settings")]
        [SerializeField]
        private Sprite availableLvlSprite;
        [SerializeField]
        private Sprite savedLvlSprite;
        [SerializeField]
        private Sprite lockedLvlSprite;

        #endregion

        #region Propeties

        public LocalizedTMPro ScenarioName { 
            get => scenarioName; 
        }
        public TextMeshProUGUI LvlIndexText { 
            get => lvlIndexText; 
        }

        public ScenarioInfo CachedInfo {
            get;
            private set;
        }

        public IScenarioSelectListener Listener {
            get;
            set;
        }

        private ScenarioUIState CurrentState
        {
            get;
            set;
        }

        public GameObject LockerIconObj { 
            get => lockerIconObj; 
        }
        public Image OutlineImg { 
            get => outlineImg; 
        }
        public Color OutlineAvailableColor { 
            get => outlineAvailableColor; 
        }
        public Color OutlineLockedColor { 
            get => outlineLockedColor; 
        }
        public Color ScenarioNameColor { 
            get => scenarioNameColor; 
        }
        public StarsDisplayElement StarsDisplayer { 
            get => starsDisplayer; 
        }
        public Color OutlineSavedColor { 
            get => outlineSavedColor;
        }
        public Sprite AvailableLvlSprite { get => availableLvlSprite; }
        public Sprite SavedLvlSprite { get => savedLvlSprite; }
        public Sprite LockedLvlSprite { get => lockedLvlSprite; }
        public Image MainImg { get => mainImg; }

        #endregion

        #region Methods

        public void SetInfo(ScenarioInfo scenarioInfo, IScenarioSelectListener listener)
        {
            CachedInfo = scenarioInfo;
            LvlIndexText.SetText(scenarioInfo.OrderNo.ToString());
            ScenarioName.SetKey(scenarioInfo.ScenarioNameKey);
            Listener = listener;

            // Podstawowy stan to lock.
            SetState(new ScenarioUILockedState());
        }

        public void SetState(ScenarioUIState state)
        {
            CurrentState = state;
            CurrentState.SetContext(this);
        }

        public void SelectedScenario()
        {
            CurrentState.OnSelectedScenario();
        }

        #endregion

        #region Enums



        #endregion
    }
}
