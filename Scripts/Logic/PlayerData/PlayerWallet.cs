using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerData
{
    [Serializable]
    public class PlayerWallet
    {
        #region Fields
        
        [SerializeField]
        private List<int> unlockedUpgradesIds = new List<int>();
        [SerializeField]
        private int upgradePointsAmmount = Constants.DEFAULT_VALUE;
        [SerializeField]
        private int kPointsAmmount = Constants.DEFAULT_VALUE;
        [SerializeField]
        private float kPointsAmmountAllTheTime = Constants.DEFAULT_VALUE;
        [SerializeField]
        private List<NodeModeType> unlockedModes = new List<NodeModeType>();

        [SerializeField]
        private int currentSavedLvlId = Constants.DEFAULT_ID;
        [SerializeField]
        private HashSet<PlayerLvlInfo> finishedLvls = new HashSet<PlayerLvlInfo>();

        #endregion

        #region Propeties

        /// <summary>
        /// int - aktualna ilosc UP.
        /// </summary>
        public event Action<int> OnUpgradePointsChanged = delegate { };

        /// <summary>
        /// int - aktualna ilosc KP.
        /// </summary>
        public event Action<int> OnKPointsChanged = delegate { };

        /// <summary>
        /// int - ID odblokowanego upgradu.
        /// </summary>
        public event Action<int> OnUnlockedUpgrade = delegate { };
        public event Action OnFinishedLvlsUpdate = delegate { };
        public event Action OnUnlockMode = delegate { };

        public List<int> UnlockedUpgradesIds { 
            get => unlockedUpgradesIds; 
        }

        public int UpgradePointsAmmount {
            get => upgradePointsAmmount;
            private set => upgradePointsAmmount = value;
        }

        public int KPointsAmmount { 
            get => kPointsAmmount; 
            private set => kPointsAmmount = value; 
        }

        public List<NodeModeType> UnlockedModes { 
            get => unlockedModes; 
            set => unlockedModes = value; 
        }

        // PROGRESS VARIABLES
        public int CurrentSavedLvlId { 
            get => currentSavedLvlId; 
            private set => currentSavedLvlId = value; 
        }

        public HashSet<PlayerLvlInfo> FinishedLvls { 
            get => finishedLvls; 
            set => finishedLvls = value; 
        }
        public float KPointsAmmountAllTheTime {
            get => kPointsAmmountAllTheTime;
            private set => kPointsAmmountAllTheTime = value; 
        }

        #endregion

        #region Methods

        public PlayerWallet() { }

        public void SetSavedLvlId(int id)
        {
            CurrentSavedLvlId = id;
        }

        public void SetUpgradePoints(int value)
        {
            UpgradePointsAmmount = value;
            if (UpgradePointsAmmount < Constants.DEFAULT_VALUE)
            {
                UpgradePointsAmmount = Constants.DEFAULT_VALUE;
            }

            OnUpgradePointsChanged?.Invoke(UpgradePointsAmmount);
        }

        public void SetKPoints(int value)
        {
            KPointsAmmount = value;
            if (KPointsAmmount < Constants.DEFAULT_VALUE)
            {
                KPointsAmmount = Constants.DEFAULT_VALUE;
            }

            OnKPointsChanged?.Invoke(KPointsAmmount);
        }

        public int GetPlayerTotalStars()
        {
            int output = 0;
            foreach (PlayerLvlInfo lvlInfo in FinishedLvls)
            {
                output += (int)lvlInfo.RewardStars;
            }

            return output;
        }

        public void AddKPoints(int value)
        {
            SetKPoints(KPointsAmmount + value);
            KPointsAmmountAllTheTime += value;
        }

        public void SubstractKPoints(int value)
        {
            SetKPoints(KPointsAmmount - value);
        }

        public bool TrySubstractKPoints(int value)
        {
            if(KPointsAmmount - value >= Constants.DEFAULT_VALUE)
            {
                SubstractKPoints(value);
                return true;
            }

            return false;
        }

        public void AddUpgradePoints(int value)
        {
            SetUpgradePoints(UpgradePointsAmmount + value);
        }

        public void SubstractUpgradePoints(int value)
        {
            SetUpgradePoints(UpgradePointsAmmount - value);
        }

        public void AddUnlockedUpgrade(int id)
        {
            UnlockedUpgradesIds.AddAscending(id);
            OnUnlockedUpgrade.Invoke(id);
        }

        public bool HasPlayerAnyUpgrade(List<int> ids)
        {
            foreach (int id in ids)
            {
                if (HasPlayerUpgrade(id) == true)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasPlayerUpgrade(int id)
        {
            return UnlockedUpgradesIds.BinarySearch(id) >= 0;
        }

        public void AddUnlockedMode(NodeModeType unlockedMode)
        {
            // Hotfix.
            if (UnlockedModes == null)
            {
                UnlockedModes = new List<NodeModeType>();
            }

            UnlockedModes.Add(unlockedMode);
            OnUnlockMode();
        }

        public void AddFinishedLvl(int lvlId, float timeMs, ScenarioDataManager.RewardType stars)
        {
            // Hotfix.
            if(FinishedLvls == null)
            {
                FinishedLvls = new HashSet<PlayerLvlInfo>();
            }

            PlayerLvlInfo lvlInfo = FinishedLvls.GetElementByID(lvlId);
            if(lvlInfo != null)
            {
                // Update istniejacego zapisu jezeli lepszy wynik.
                if(timeMs < lvlInfo.ScenarioTimeMs)
                {
                    lvlInfo.SetStarsReward(stars);
                }
            }

            FinishedLvls.Add(new PlayerLvlInfo(lvlId, timeMs, stars));
            OnFinishedLvlsUpdate();
        }

        public bool IsPlayerFinishLvl(int id)
        {
            return FinishedLvls.ContainsElementByID(id);
        }

        #endregion

        #region Enums



        #endregion
    }
}
