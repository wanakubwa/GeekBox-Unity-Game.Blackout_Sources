
using System;

namespace TechnologyTree
{
    public class UPCostLogFunction : IUPCostFunction
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public int GetNextUPCost(int currentUP, float baseValue)
        {
            // 10log(2)*14.5 + 10log(1)*14.5
            return (int)(10 * Math.Log(currentUP + 1) * baseValue + 10 * Math.Log(currentUP) * baseValue);
        }

        #endregion

        #region Enums



        #endregion
    }
}
