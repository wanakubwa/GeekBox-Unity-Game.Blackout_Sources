using System;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public class TargetModifiableFloatValue : TargetValue<int, ModifiableFloatValue>
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public void AddTarget(int id, float baseValue)
        {
            ModifiableFloatValue floatValue = base.AddTarget(id);
            floatValue.SetBaseValue(baseValue);
        }

        #endregion

        #region Enums



        #endregion
    }
}
