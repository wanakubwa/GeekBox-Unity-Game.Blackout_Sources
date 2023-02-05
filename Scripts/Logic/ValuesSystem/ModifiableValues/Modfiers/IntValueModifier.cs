using System;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public class IntValueModifier : ValueModifier<int>
    {
        #region Fields

        public const int DEFAULT_VALUE = 0;

        #endregion

        #region Propeties



        #endregion

        #region Methods

        public IntValueModifier(int modifierValue, int sourceId, ModifierType modType) : base(modifierValue, sourceId, modType)
        {

        }

        public override int GetModifierValue(int baseValue)
        {
            if (ModType == ModifierType.NORMAL)
            {
                return ModifierValue;
            }
            else if (ModType == ModifierType.PERCENT)
            {
                float percentValue = ModifierValue * 0.01f;
                return (int)(baseValue * percentValue);
            }

            return DEFAULT_VALUE;
        }

        #endregion

        #region Enums



        #endregion
    }
}
