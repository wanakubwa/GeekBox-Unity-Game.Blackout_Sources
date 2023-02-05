using System;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public class SplittedFloatValue : SplittedValue<float>
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public override void AddNormalValue(float valueToAdd)
        {
            SetNormalValue(NormalValue + valueToAdd);
        }

        public override void AddPercentValue(float valueToAdd)
        {
            SetPercentValue(PercentValue + valueToAdd);
        }

        #endregion

        #region Enums



        #endregion
    }
}
