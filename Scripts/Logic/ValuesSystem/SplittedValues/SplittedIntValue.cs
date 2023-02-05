using System;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public class SplittedIntValue : SplittedValue<int>
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public override void AddNormalValue(int valueToAdd)
        {
            SetNormalValue(NormalValue + valueToAdd);
        }

        public override void AddPercentValue(int valueToAdd)
        {
            SetPercentValue(PercentValue + valueToAdd);
        }

        #endregion

        #region Enums



        #endregion
    }
}
