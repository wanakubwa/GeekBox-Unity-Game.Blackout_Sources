using UnityEngine;

namespace PointsSystem.RewardKPoints.Functions
{
    public class SqrtRewardFunction : IRewardFunction
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public float EvaluateForLvl(int currentLvl, float baseRewardValue)
        {
            return Mathf.Sqrt(currentLvl) * baseRewardValue;
        }

        #endregion

        #region Enums



        #endregion

    }
}
