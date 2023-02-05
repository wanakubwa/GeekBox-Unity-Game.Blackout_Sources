using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsSystem.RewardKPoints.Functions
{
    public interface IRewardFunction
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        float EvaluateForLvl(int currentLvl, float baseRewardValue);

        #endregion

        #region Enums



        #endregion
    }
}
