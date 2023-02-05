using PointsSystem.RewardKPoints.Functions;
using UnityEngine;

namespace PointsSystem.RewardKPoints
{
    public class RewardController
    {
        #region Fields

        public const float ZERO_STARS_MULTIPLIER = 0.25f;
        public const float ONE_STARS_MULTIPLIER = 0.5f;
        public const float TWO_STARS_MULTIPLIER = 0.75f;
        public const float THREE_STARS_MULTIPLIER = 1f;

        public const int BASE_MAX_REWARD = 100;
        private const int ROUND_FACTOR = 5;

        #endregion

        #region Propeties

        private IRewardFunction RewardFunction {
            get;
            set;
        }

        #endregion

        #region Methods

        public RewardController()
        {
            RewardFunction = new SqrtRewardFunction();
        }

        public int GetRewardForCurrentLvl(ScenarioDataManager.RewardType reward)
        {
            float multiplier = GetMultiplierForReward(reward);
            int lvlNo = ScenariosManager.Instance.CurrentScenarioInfo.OrderNo;
            return RoundReward(RewardFunction.EvaluateForLvl(lvlNo, BASE_MAX_REWARD) * multiplier);
        }

        private int RoundReward(float value)
        {
            int fixedValue = Mathf.RoundToInt(value);

            int rest = fixedValue % ROUND_FACTOR;
            int withoutRestValue = fixedValue - rest;

            return rest >= ROUND_FACTOR * 0.5f ? withoutRestValue + ROUND_FACTOR : withoutRestValue;
        }

        private float GetMultiplierForReward(ScenarioDataManager.RewardType reward)
        {
            float multiplier = Constants.DEFAULT_VALUE;
            switch (reward)
            {
                case ScenarioDataManager.RewardType.ZERO_STARS:
                    multiplier = ZERO_STARS_MULTIPLIER;
                    break;
                case ScenarioDataManager.RewardType.ONE_STAR:
                    multiplier = ONE_STARS_MULTIPLIER;
                    break;
                case ScenarioDataManager.RewardType.TWO_STARS:
                    multiplier = TWO_STARS_MULTIPLIER;
                    break;
                case ScenarioDataManager.RewardType.THREE_STARS:
                    multiplier = THREE_STARS_MULTIPLIER;
                    break;
            }

            return multiplier;
        }

        #endregion

        #region Enums



        #endregion
    }
}
