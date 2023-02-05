using System;
using UnityEngine;

namespace PlayerData
{
    [Serializable]
    public class PlayerLvlInfo : IIDEquatable, IEquatable<PlayerLvlInfo>
    {
        #region Fields

        [SerializeField]
        private int scenarioId;
        [SerializeField]
        private float scenarioTimeMs;
        [SerializeField]
        private ScenarioDataManager.RewardType rewardStars;

        #endregion

        #region Propeties

        public int ScenarioId { 
            get => scenarioId; 
            private set => scenarioId = value; 
        }
        public float ScenarioTimeMs { 
            get => scenarioTimeMs; 
            private set => scenarioTimeMs = value; 
        }
        public ScenarioDataManager.RewardType RewardStars { 
            get => rewardStars; 
            private set => rewardStars = value; 
        }

        public int ID => ScenarioId;

        #endregion

        #region Methods

        public PlayerLvlInfo()
        {
        }

        public PlayerLvlInfo(int scenarioId, float scenarioTimeMs, ScenarioDataManager.RewardType rewardStars)
        {
            this.scenarioId = scenarioId;
            this.scenarioTimeMs = scenarioTimeMs;
            this.rewardStars = rewardStars;
        }

        public void SetStarsReward(ScenarioDataManager.RewardType rewardStars)
        {
            RewardStars = rewardStars;
        }

        public bool IDEqual(int otherId)
        {
            return ScenarioId == otherId;
        }

        public bool Equals(PlayerLvlInfo other)
        {
            return IDEqual(other.ID);
        }

        #endregion

        #region Enums



        #endregion
    }
}
